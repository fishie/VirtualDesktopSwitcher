using System;
using System.Windows.Forms;

namespace VirtualDesktopSwitcher.Code.CustomFormControls
{
    class TitlePanel : Panel
    {
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WinApi.WM_NCHITTEST)
            {
                message.Result = (IntPtr)WinApi.HTTRANSPARENT;
                return;
            }

            base.WndProc(ref message);
        }
    }
}
