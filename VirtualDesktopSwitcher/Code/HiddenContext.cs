using System.Windows.Forms;

namespace VirtualDesktopSwitcher.Code
{
    class HiddenContext : ApplicationContext
    {
        public HiddenContext()
        {
            var form = new VirtualDesktopSwitcherForm();
            if (form.HideOnStartup == false) form.Show();
        }
    }
}
