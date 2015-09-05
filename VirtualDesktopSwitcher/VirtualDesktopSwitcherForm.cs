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

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static VirtualDesktopSwitcherForm formInstance;
        private static IKeyboardSimulator keyboardSimulator;
        private static List<Rectangle> rectangles;
        private static bool desktopScroll;
        private static int hHook = 0;
        private HookProc mouseHookProcedure; // Need to keep a reference to hookproc or otherwise it will be GC:ed.
        
        public VirtualDesktopSwitcherForm()
        {
            InitializeComponent();
            formInstance = this;
            keyboardSimulator = (new InputSimulator()).Keyboard;
            AttachHook();
            rectangles = new List<Rectangle>();

            using (var streamReader = new StreamReader("config.json"))
            {
                string json = streamReader.ReadToEnd();
                dynamic jsonConfig = JsonConvert.DeserializeObject(json);

                if (jsonConfig.rectangles != null)
                {
                    foreach (var jsonRectangle in jsonConfig.rectangles)
                    {
                        int x = jsonRectangle.x;
                        int y = jsonRectangle.y;
                        int width = jsonRectangle.width;
                        int height = jsonRectangle.height;

                        rectangles.Add(new Rectangle(x, y, width, height));
                    }
                }

                desktopScroll = jsonConfig.desktopScroll ?? false;
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
                if (formInstance.Visible) formInstance.BackColor = Color.Green;

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
                if (formInstance.Visible) formInstance.BackColor = Color.LightGray;
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

        private void HideFormEventHandler(object sender, EventArgs e)
        {
            Hide();
        }

        private void helloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
