using System.Windows.Forms;

namespace VirtualDesktopSwitcher.Code.CustomFormControls
{
    class UnselectableButton : Button
    {
        public UnselectableButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
