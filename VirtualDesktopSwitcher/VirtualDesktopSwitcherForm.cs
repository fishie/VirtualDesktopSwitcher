using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace VirtualDesktopSwitcher
{
    public partial class VirtualDesktopSwitcherForm : Form
    {
        #region WinAPIFunctions
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder title, int size);
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x; // LONG
            public int y; // LONG

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData; // DWORD
            public int flags; // DWORD
            public int time; // DWORD
            public IntPtr dwExtraInfo; // ULONG_PTR
        }
        #endregion

        #region MessageConstants
        // For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.
        public const int WH_MOUSE_LL = 14;
        public const int WM_MOUSEWHEEL = 522;
        public const int WM_SYSCOMMAND = 274;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_MINIMIZE = 0xF020;
        #endregion

        public bool hideOnStartup { get; private set; }
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static VirtualDesktopSwitcherForm formInstance;
        private static IKeyboardSimulator keyboardSimulator;
        private static List<Rectangle> rectangles;
        private static bool desktopScroll;
        private static int hHook = 0;

        private HookProc mouseHookProcedure; // Need to keep a reference to hookproc or otherwise it will be GC:ed.
        private List<Form> forms;
        private dynamic jsonConfig;

        private const string CONFIG_FILENAME = "config.json";
        private const string SHORTCUT_FILENAME = "\\VirtualDesktopSwitcher.lnk";

        public VirtualDesktopSwitcherForm()
        {
            InitializeComponent();
            formInstance = this;
            keyboardSimulator = (new InputSimulator()).Keyboard;
            AttachHook();
            rectangles = new List<Rectangle>();

            using (var streamReader = new StreamReader(CONFIG_FILENAME))
            {
                string json = streamReader.ReadToEnd();
                jsonConfig = JsonConvert.DeserializeObject(json);

                if (jsonConfig.rectangles != null)
                {
                    foreach (var jsonRectangle in jsonConfig.rectangles)
                    {
                        int x = jsonRectangle.x;
                        int y = jsonRectangle.y;
                        int width = jsonRectangle.width;
                        int height = jsonRectangle.height;

                        rectangles.Add(new Rectangle(x, y, width, height));

                        var node = treeView1.Nodes.Add("rectangle " + (treeView1.Nodes.Count+1));

                        Action<string, int> addSubNode = (label, value) =>
                        {
                            var subnode = node.Nodes.Add(label);
                            var subsubnode = subnode.Nodes.Add(value.ToString());
                            subnode.ExpandAll();
                        };

                        addSubNode("x", x);
                        addSubNode("y", y);
                        addSubNode("width", width);
                        addSubNode("height", height);
                    }
                }

                desktopScroll = jsonConfig.desktopScroll ?? false;
                hideOnStartup = jsonConfig.hideOnStartup ?? false;
            }

            desktopScrollCheckbox.CheckedChanged -= desktopScrollCheckbox_CheckedChanged;
            desktopScrollCheckbox.Checked = desktopScroll;
            desktopScrollCheckbox.CheckedChanged += desktopScrollCheckbox_CheckedChanged;

            hideOnStartupCheckbox.CheckedChanged -= hideOnStartupCheckbox_CheckedChanged;
            hideOnStartupCheckbox.Checked = hideOnStartup;
            hideOnStartupCheckbox.CheckedChanged += hideOnStartupCheckbox_CheckedChanged;

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + SHORTCUT_FILENAME))
            {
                IWshRuntimeLibrary.WshShellClass wsh = new IWshRuntimeLibrary.WshShellClass();
                IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup) + SHORTCUT_FILENAME)
                    as IWshRuntimeLibrary.IWshShortcut;
                if (shortcut.TargetPath.ToLower() == System.Reflection.Assembly.GetEntryAssembly().Location.ToLower())
                {
                    loadOnWindowsStartupCheckbox.Checked = true;
                }
                else
                {
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                    File.Delete(path + SHORTCUT_FILENAME);
                    loadOnWindowsStartupCheckbox.Checked = false;
                }
            }
            else
            {
                loadOnWindowsStartupCheckbox.Checked = false;
            }
        }
        
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                int wParam = m.WParam.ToInt32();
                if (wParam == SC_MAXIMIZE || wParam == SC_MINIMIZE)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void AttachHook()
        {
            if (hHook == 0)
            {
                mouseHookProcedure = new HookProc(LowLevelMouseProc);
                hHook = SetWindowsHookEx(WH_MOUSE_LL, mouseHookProcedure, IntPtr.Zero, 0);

                // If the SetWindowsHookEx function fails.
                if (hHook == 0)
                {
                    int error = Marshal.GetLastWin32Error();
                    MessageBox.Show("SetWindowsHookEx Failed " + error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("SetWindowsHookEx Failed - hHook was not null");
            }
        }

        private void DetachHook()
        {
            bool ret = UnhookWindowsHookEx(hHook);

            // If the UnhookWindowsHookEx function fails.
            if (ret == false)
            {
                MessageBox.Show("UnhookWindowsHookEx Failed");
                return;
            }
            hHook = 0;
        }

        private static void CtrlWinKey(VirtualKeyCode virtualKeyCode)
        {
            keyboardSimulator.KeyDown(VirtualKeyCode.LCONTROL);
            keyboardSimulator.KeyDown(VirtualKeyCode.LWIN);

            keyboardSimulator.KeyPress(virtualKeyCode);

            keyboardSimulator.KeyUp(VirtualKeyCode.LWIN);
            keyboardSimulator.KeyUp(VirtualKeyCode.LCONTROL);
        }

        private static bool CheckPoint(POINT point)
        {
            foreach (var rectangle in rectangles)
            {
                if (point.x > rectangle.Left && point.x < rectangle.Right &&
                    point.y > rectangle.Top && point.y < rectangle.Bottom)
                {
                    return true;
                }
            }

            return false;
        }

        public static int LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }

            var msllHookStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);

            if (formInstance.Visible)
            {
                formInstance.Text = string.Format("{0} {1}", msllHookStruct.pt.x, msllHookStruct.pt.y);
            }

            var windowTitle = new StringBuilder(10);
            GetWindowText(WindowFromPoint(msllHookStruct.pt), windowTitle, 11);

            if (CheckPoint(msllHookStruct.pt) || (desktopScroll && windowTitle.ToString() == "FolderView"))
            {
                if (formInstance.Visible) formInstance.BackColor = Color.Yellow;

                if (wParam.ToInt32() == WM_MOUSEWHEEL)
                {
                    int highOrder = msllHookStruct.mouseData >> 16;

                    if (highOrder > 0)
                    {
                        CtrlWinKey(VirtualKeyCode.LEFT);
                    }
                    else
                    {
                        CtrlWinKey(VirtualKeyCode.RIGHT);
                    }
                }
            }
            else
            {
                if (formInstance.Visible) formInstance.BackColor = SystemColors.Control;
            }

            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        private void ToggleVisibilityWithMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = !Visible;
                TopMost = Visible;
            }
        }

        private void helloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowRectangles()
        {
            forms = new List<Form>();

            foreach (var rectangle in rectangles)
            {
                var form = new Form();
                form.Parent = null;
                form.FormBorderStyle = FormBorderStyle.None;
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(rectangle.X, rectangle.Y);
                form.MinimumSize = new Size(rectangle.Width, rectangle.Height);
                form.Size = form.MinimumSize;
                form.TopMost = true;
                form.BackColor = Color.Yellow;
                form.ShowInTaskbar = false;
                form.Show();
                forms.Add(form);
            }
        }

        private void HideRectangles()
        {
            foreach (var form in forms)
            {
                form.Close();
            }
            forms = null;
        }

        private void VirtualDesktopSwitcherForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) ShowRectangles();
            else HideRectangles();
        }

        private void desktopScrollCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            desktopScroll = desktopScrollCheckbox.Checked;
            jsonConfig.desktopScroll = desktopScroll;
            UpdateConfigJsonFile();
        }

        private void hideOnStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            jsonConfig.hideOnStartup = hideOnStartupCheckbox.Checked;
            UpdateConfigJsonFile();
        }

        private void UpdateConfigJsonFile()
        {
            File.WriteAllText(CONFIG_FILENAME, JsonConvert.SerializeObject(jsonConfig, Formatting.Indented));
        }

        private void loadOnWindowsStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (loadOnWindowsStartupCheckbox.Checked)
            {
                IWshRuntimeLibrary.WshShellClass wsh = new IWshRuntimeLibrary.WshShellClass();
                IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup) + SHORTCUT_FILENAME)
                    as IWshRuntimeLibrary.IWshShortcut;
                shortcut.Arguments = "";
                shortcut.TargetPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                shortcut.Description = "VisualDesktopSwitcher";
                shortcut.WorkingDirectory = Environment.CurrentDirectory;
                shortcut.Save();
            }
            else
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                File.Delete(path + SHORTCUT_FILENAME);
            }
        }

        private void VirtualDesktopSwitcherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                e.Node.TreeView.LabelEdit = true;
                e.Node.BeginEdit();
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            int value;
            if (int.TryParse(e.Label, out value))
            {
                int i = e.Node.Parent.Parent.Index;
                var rectangle = rectangles[i];
                
                switch (e.Node.Parent.Text[0])
                {
                    case 'x':
                        rectangle.X = value;
                        break;
                    case 'y':
                        rectangle.Y = value;
                        break;
                    case 'w':
                        rectangle.Width = value;
                        break;
                    case 'h':
                        rectangle.Height = value;
                        break;
                }

                rectangles[i] = rectangle; // because Rectangle is a struct we only get a copy of it, not the reference

                e.Node.TreeView.LabelEdit = false;
            }

        }
    }
}
