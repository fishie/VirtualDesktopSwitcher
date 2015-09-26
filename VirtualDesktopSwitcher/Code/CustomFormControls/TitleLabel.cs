using System;
using System.Windows.Forms;

namespace VirtualDesktopSwitcher.Code.CustomFormControls
{
    class TitleLabel : Label
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinApi.WM_NCHITTEST)
            {
                m.Result = (IntPtr)WinApi.HTTRANSPARENT;
                return;
            }

            base.WndProc(ref m);
        }
    }
}
