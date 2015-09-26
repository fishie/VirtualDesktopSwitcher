namespace VirtualDesktopSwitcher.Code
{
    class WinApi
    {
        #region MessageConstants
        // For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.
        public const int WH_MOUSE_LL = 14;
        public const int WM_MOUSEWHEEL = 522;
        public const int WM_SYSCOMMAND = 274;
        public const int WM_NCHITTEST = 0x84;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_MINIMIZE = 0xF020;
        public const int HT_CAPTION = 0x2;
        public const int HTTRANSPARENT = -1;
        #endregion
    }
}
