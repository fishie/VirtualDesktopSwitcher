using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VirtualDesktopSwitcher.Code
{
    class WinApi
    {
        #region MessageConstants
        // For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.
        public const int WH_MOUSE_LL = 14;
        public const int WM_MOUSEWHEEL = 522;
        public const int WM_NCHITTEST = 0x84;
        public const int HT_CAPTION = 0x2;
        public const int HTTRANSPARENT = -1;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const uint VK_RCONTROL = 0xA3;
        #endregion

        #region WinAPIFunctions
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, VirtualDesktopSwitcherForm.HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr WindowFromPoint(VirtualDesktopSwitcherForm.POINT point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder title, int size);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        #endregion

        private static uint CreateKeyLparam(uint key, bool keyUp)
        {
            uint keydownLparam = 0;
            keydownLparam |= 0x00000001; // repeat count
            keydownLparam |= MapVirtualKey(key, 0) << 16; // scan code

            // context code, previous key state, transition state
            if (keyUp) keydownLparam |= 0xc0000000;

            return keydownLparam;
        }

        public static void KeyDown(IntPtr hWnd, uint key)
        {
            SendMessage(hWnd, WM_KEYDOWN, key, CreateKeyLparam(key, false));
        }

        public static void KeyUp(IntPtr hWnd, uint key)
        {
            SendMessage(hWnd, WM_KEYUP, key, CreateKeyLparam(key, true));
        }

        public static void KeyPress(IntPtr hWnd, uint key)
        {
            KeyDown(hWnd, key);
            KeyUp(hWnd, key);
        }
    }
}
