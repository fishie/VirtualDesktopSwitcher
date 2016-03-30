using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using IWshRuntimeLibrary;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using File = System.IO.File;
using System.Diagnostics;

namespace VirtualDesktopSwitcher.Code
{
    public partial class VirtualDesktopSwitcherForm : Form
    {
        public bool HideOnStartup { get; private set; }
        
        private static VirtualDesktopSwitcherForm _formInstance;
        private static IKeyboardSimulator _keyboardSimulator;
        private static List<Rectangle> _rectangles;
        private static bool _desktopScroll;
        private static bool _taskViewScroll;
        private static bool _virtualBoxFix;
        private static int _hHook;
        private static IntPtr _desktopWindow;
        private static List<IntPtr> _taskViewButtons;
        private static uint _wmTaskbarCreated;

        private WinApi.HookProc _mouseHookProcedure; // Need to keep a reference to hookproc or otherwise it will be GC:ed.
        private readonly List<Form> _forms;
        private dynamic _jsonConfig;
        private TreeNode _clickedNode;

        private const string CONFIG_FILENAME = "config.json";
        private const string SHORTCUT_FILENAME = "\\VirtualDesktopSwitcher.lnk";

        public VirtualDesktopSwitcherForm()
        {
            InitializeComponent();

            _formInstance = this;
            _keyboardSimulator = (new InputSimulator()).Keyboard;
            _rectangles = new List<Rectangle>();
            _forms = new List<Form>();

            ReadConfig();
            CheckForStartupShortcut();
            AttachHook();
            FindWindows();
        }

        private static bool EnumWindow(IntPtr hwnd, IntPtr lParam)
        {
            var title = GetWindowString(hwnd);
            var className = GetWindowClass(hwnd);
            if (title == "FolderView" && className == "SysListView32" && IsDesktopWindowLineage(hwnd))
            {
                _desktopWindow = hwnd;
            }
            if (title == "Task View" && className == "TrayButton")
            {
                _taskViewButtons.Add(hwnd);
            }
            return true;
        }

        private void FindWindows()
        {
            _wmTaskbarCreated = WinApi.RegisterWindowMessage("TaskbarCreated");
            _taskViewButtons = new List<IntPtr>();
            WinApi.EnumChildWindows(WinApi.GetDesktopWindow(), EnumWindow, IntPtr.Zero);
        }

        #region Event handlers
        private void VirtualDesktopSwitcherForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) ShowRectangles();
            else HideRectangles();
        }

        private void CheckedChanged(out bool b, CheckBox checkBox, string propertyName)
        {
            b = checkBox.Checked;
            _jsonConfig[propertyName] = b;
            UpdateConfigJsonFile();
        }

        private void desktopScrollCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged(out _desktopScroll, desktopScrollCheckbox, "desktopScroll");
        }

        private void taskViewButtonScrollCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged(out _taskViewScroll, taskViewButtonScrollCheckbox, "taskViewScroll");
        }

        private void virtualBoxFixCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged(out _virtualBoxFix, virtualBoxFixCheckbox, "virtualBoxFix");
        }

        private void hideOnStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _jsonConfig.hideOnStartup = hideOnStartupCheckbox.Checked;
            UpdateConfigJsonFile();
        }

        private void ToggleVisibilityWithMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = !Visible;
                TopMost = Visible;
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void loadOnWindowsStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + SHORTCUT_FILENAME;

            if (loadOnWindowsStartupCheckbox.Checked)
            {
                var wshShell = new WshShell();
                var shortcut = wshShell.CreateShortcut(shortcutPath);

                shortcut.Arguments = "";
                shortcut.TargetPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                shortcut.Description = "VisualDesktopSwitcher";
                shortcut.WorkingDirectory = Environment.CurrentDirectory;
                shortcut.Save();
            }
            else
            {
                File.Delete(shortcutPath);
            }
        }

        private void VirtualDesktopSwitcherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void rectanglesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                e.Node.TreeView.LabelEdit = true;
                e.Node.BeginEdit();
            }
        }

        private void rectanglesTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            int rectangleIndex = e.Node.Parent.Parent.Index;
            string propertyName = e.Node.Parent.Text;
            int value;

            if (int.TryParse(e.Label, out value))
            {
                _rectangles[rectangleIndex].Set(propertyName, value);
                _jsonConfig.rectangles[rectangleIndex][propertyName] = value;
                UpdateConfigJsonFile();
                HideRectangles();
                ShowRectangles();
            }
            else
            {
                e.CancelEdit = true;
            }
            e.Node.TreeView.LabelEdit = false;
        }

        private void addRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = rectanglesTreeView.Nodes.Add("rectangle " + (rectanglesTreeView.Nodes.Count + 1));
            rectanglesTreeView.SelectedNode = node;

            Action<string, string> addSubNode = (label, value) =>
            {
                var subnode = node.Nodes.Add(label);
                subnode.Nodes.Add(value);
                subnode.ExpandAll();
            };

            addSubNode("x", "0");
            addSubNode("y", "0");
            addSubNode("width", "50");
            addSubNode("height", "40");

            _rectangles.Add(new Rectangle(0, 0, 50, 40));
            var jObject = JsonConvert.DeserializeObject(@"{""x"": 0, ""y"": 0, ""width"": 50, ""height"": 40}");
            _jsonConfig.rectangles.Add(jObject);
            HideRectangles();
            ShowRectangles();
            UpdateConfigJsonFile();
        }

        private void removeRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = _clickedNode.Index;
            _clickedNode.Remove();
            _rectangles.RemoveAt(index);
            _jsonConfig.rectangles.RemoveAt(index);
            HideRectangles();
            ShowRectangles();
            UpdateConfigJsonFile();
        }

        private void rectanglesTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            var node = rectanglesTreeView.GetNodeAt(e.Location);
            if (node != null && node.Level == 0)
            {
                rectanglesTreeView.SelectedNode = node;
                _clickedNode = node;
                treeViewRightClickMenuRemove.Items[0].Text = $"Remove rectangle {(node.Index + 1)}";
                rectanglesTreeView.ContextMenuStrip = treeViewRightClickMenuRemove;
            }
            else
            {
                rectanglesTreeView.ContextMenuStrip = treeViewRightClickMenuAdd;
            }
        }

        private void ToggleRectangles(object sender = null, EventArgs e = null)
        {
            rectanglesTreeView.Visible ^= true;
            Height += (rectanglesTreeView.Visible ? 1 : -1) * rectanglesTreeView.Height;
            advancedLabel.Text = (rectanglesTreeView.Visible ? "-" : "+") + " Advanced";
        }
        #endregion

        private void ReadConfig()
        {
            if (File.Exists(CONFIG_FILENAME) == false)
            {
                File.WriteAllText(CONFIG_FILENAME, "{}");
                _jsonConfig = new JObject();
                return;
            }

            using (var streamReader = new StreamReader(CONFIG_FILENAME))
            {
                string json = streamReader.ReadToEnd();

                try
                {
                    _jsonConfig = JsonConvert.DeserializeObject(json);
                }
                catch (JsonReaderException)
                {
                    streamReader.Close();
                    File.WriteAllText(CONFIG_FILENAME, "{}");
                    _jsonConfig = new JObject();
                    return;
                }

                if (_jsonConfig.rectangles != null)
                {
                    foreach (var jsonRectangle in _jsonConfig.rectangles)
                    {
                        int x = jsonRectangle.x;
                        int y = jsonRectangle.y;
                        int width = jsonRectangle.width;
                        int height = jsonRectangle.height;

                        _rectangles.Add(new Rectangle(x, y, width, height));

                        var node = rectanglesTreeView.Nodes.Add("rectangle " + (rectanglesTreeView.Nodes.Count + 1));

                        Action<string, int> addSubNode = (label, value) =>
                        {
                            var subnode = node.Nodes.Add(label);
                            subnode.Nodes.Add(value.ToString());
                            subnode.ExpandAll();
                        };

                        addSubNode("x", x);
                        addSubNode("y", y);
                        addSubNode("width", width);
                        addSubNode("height", height);
                    }
                }

                _desktopScroll = _jsonConfig.desktopScroll ?? false;
                _taskViewScroll = _jsonConfig.taskViewScroll ?? false;
                _virtualBoxFix = _jsonConfig.virtualBoxFix ?? false;
                HideOnStartup = _jsonConfig.hideOnStartup ?? false;
            }

            Action<bool, CheckBox, EventHandler> setChecked = (boolValue, checkBox, eventHandler) =>
            {
                checkBox.CheckedChanged -= eventHandler;
                checkBox.Checked = boolValue;
                checkBox.CheckedChanged += eventHandler;
            };

            setChecked(_desktopScroll, desktopScrollCheckbox, desktopScrollCheckbox_CheckedChanged);
            setChecked(_taskViewScroll, taskViewButtonScrollCheckbox, taskViewButtonScrollCheckbox_CheckedChanged);
            setChecked(_virtualBoxFix, virtualBoxFixCheckbox, virtualBoxFixCheckbox_CheckedChanged);
            setChecked(HideOnStartup, hideOnStartupCheckbox, hideOnStartupCheckbox_CheckedChanged);
        }

        private void CheckForStartupShortcut()
        {
            var shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + SHORTCUT_FILENAME;

            if (File.Exists(shortcutPath))
            {
                var wshShell = new WshShell();
                var shortcut = wshShell.CreateShortcut(shortcutPath);
                
                if (shortcut.TargetPath.ToLower() == System.Reflection.Assembly.GetEntryAssembly().Location.ToLower())
                {
                    loadOnWindowsStartupCheckbox.Checked = true;
                }
                else
                {
                    File.Delete(shortcutPath);
                    loadOnWindowsStartupCheckbox.Checked = false;
                }
            }
            else
            {
                loadOnWindowsStartupCheckbox.Checked = false;
            }
        }

        protected override void WndProc(ref Message message)
        {
            // Enable dragging of window by clicking in the form.
            if (message.Msg == WinApi.WM_NCHITTEST)
            {
                base.WndProc(ref message);
                message.Result = (IntPtr)(WinApi.HT_CAPTION);
                return;
            }

            if (message.Msg == _wmTaskbarCreated)
            {
                FindWindows();
            }

            base.WndProc(ref message);
        }

        private void AttachHook()
        {
            if (_hHook == 0)
            {
                _mouseHookProcedure = LowLevelMouseProc;
                _hHook = WinApi.SetWindowsHookEx(WinApi.WH_MOUSE_LL, _mouseHookProcedure, IntPtr.Zero, 0);

                // If the SetWindowsHookEx function fails.
                if (_hHook == 0)
                {
                    int error = Marshal.GetLastWin32Error();
                    MessageBox.Show("SetWindowsHookEx Failed " + error);
                }
            }
            else
            {
                MessageBox.Show("SetWindowsHookEx Failed - hHook was not null");
            }
        }

        [UsedImplicitly]
        private void DetachHook()
        {
            var ret = WinApi.UnhookWindowsHookEx(_hHook);

            // If the UnhookWindowsHookEx function fails.
            if (ret == false)
            {
                MessageBox.Show("UnhookWindowsHookEx Failed");
                return;
            }
            _hHook = 0;
        }

        private static void CtrlWinKey(VirtualKeyCode virtualKeyCode)
        {
            _keyboardSimulator.KeyDown(VirtualKeyCode.LCONTROL);
            _keyboardSimulator.KeyDown(VirtualKeyCode.LWIN);

            _keyboardSimulator.KeyPress(virtualKeyCode);

            _keyboardSimulator.KeyUp(VirtualKeyCode.LWIN);
            _keyboardSimulator.KeyUp(VirtualKeyCode.LCONTROL);
        }

        private static bool CheckPoint(WinApi.POINT point)
        {
            return _rectangles.Any(rectangle =>
                point.x > rectangle.Left &&
                point.x < rectangle.Right &&
                point.y > rectangle.Top &&
                point.y < rectangle.Bottom);
        }

        private static string GetWindowString(IntPtr hwnd)
        {
            int length = WinApi.GetWindowTextLength(hwnd);
            var stringBuilder = new StringBuilder(length + 1);
            WinApi.GetWindowText(hwnd, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }

        private static string GetWindowClass(IntPtr hwnd)
        {
            var stringBuilder = new StringBuilder(256);
            WinApi.GetClassName(hwnd, stringBuilder, 256);
            return stringBuilder.ToString();
        }

        private static bool IsDesktopWindowLineage(IntPtr hwnd)
        {
            var parent = WinApi.GetParent(hwnd);
            if (parent == IntPtr.Zero) return false;

            var parentparent = WinApi.GetParent(parent);
            if (parentparent == IntPtr.Zero) return false;

            var ancestor = WinApi.GetAncestor(hwnd, 2);
            return ancestor == parentparent;
        }

        private static bool IsScrollPoint(WinApi.POINT point)
        {
            var windowUnderCursor = WinApi.WindowFromPoint(point);

            return (_rectangles.Count > 0 && CheckPoint(point)) ||
                   (_desktopScroll && windowUnderCursor == _desktopWindow) ||
                   (_taskViewScroll && _taskViewButtons.Contains(windowUnderCursor));
        }
        
        private static IntPtr GetVirtualBoxInForeground(IntPtr foregroundWindow, string foregroundWindowTitle)
        {
            var className = GetWindowClass(foregroundWindow);

            if (className == "QWidget" && foregroundWindowTitle.EndsWith("VirtualBox"))
            {
                var childWindow = WinApi.FindWindowEx(foregroundWindow, IntPtr.Zero, "QWidget", null);

                if (childWindow == IntPtr.Zero) return IntPtr.Zero;

                var childChildWindow = WinApi.FindWindowEx(childWindow, IntPtr.Zero, "QWidget", null);
                if (childChildWindow != IntPtr.Zero)
                {
                    return childChildWindow;
                }
            }
            return IntPtr.Zero;
        }

        public static int LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return WinApi.CallNextHookEx(_hHook, nCode, wParam, lParam);
            }

            if (wParam.ToInt32() == WinApi.WM_MOUSEWHEEL)
            {
                var msllHookStruct = Marshal.PtrToStructure<WinApi.MSLLHOOKSTRUCT>(lParam);

                var foregroundWindow = WinApi.GetForegroundWindow();
                var foregroundWindowTitle = GetWindowString(foregroundWindow);
                bool isVolumeControlOpen = foregroundWindowTitle == "Volume Control";

                if (!isVolumeControlOpen && IsScrollPoint(msllHookStruct.pt))
                {
                    if (_virtualBoxFix)
                    {
                        var virtualBoxWindow = GetVirtualBoxInForeground(foregroundWindow, foregroundWindowTitle);
                        if (virtualBoxWindow != IntPtr.Zero) // Send VK_RCONTROL first if VirtualBox.
                        {
                            WinApi.KeyPress(virtualBoxWindow, WinApi.VK_RCONTROL);
                        }
                    }

                    int highOrder = msllHookStruct.mouseData >> 16;
                    CtrlWinKey(highOrder > 0 ? VirtualKeyCode.LEFT : VirtualKeyCode.RIGHT);
                }
            }

            if (_formInstance.Visible)
            {
                var point = Marshal.PtrToStructure<WinApi.MSLLHOOKSTRUCT>(lParam).pt;
                if (_formInstance.rectanglesTreeView.Visible)
                {
                    _formInstance.advancedLabel.Text = $"- Advanced [{point.x} {point.y}]";
                }
                
                _formInstance.BackColor = IsScrollPoint(point) ? Color.Yellow : SystemColors.Control;
            }

            return WinApi.CallNextHookEx(_hHook, nCode, wParam, lParam);
        }



        private void ShowRectangles()
        {
            foreach (var rectangle in _rectangles)
            {
                var form = new Form
                {
                    Parent = null,
                    FormBorderStyle = FormBorderStyle.None,
                    StartPosition = FormStartPosition.Manual,
                    Location = new Point(rectangle.X, rectangle.Y),
                    MinimumSize = new Size(rectangle.Width, rectangle.Height),
                    Size = new Size(rectangle.Width, rectangle.Height),
                    TopMost = true,
                    BackColor = Color.Yellow,
                    ShowInTaskbar = false
                };
                form.Show();
                _forms.Add(form);
            }
        }

        private void HideRectangles()
        {
            foreach (var form in _forms)
            {
                form.Close();
            }
            _forms.Clear();
        }

        private void UpdateConfigJsonFile()
        {
            var json = JsonConvert.SerializeObject(_jsonConfig, Formatting.Indented);
            File.WriteAllText(CONFIG_FILENAME, json);
        }

        private void VirtualDesktopSwitcherForm_Load(object sender, EventArgs e)
        {

        }

        private void formTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://" + formTitle.Text);
        }
    }
}
