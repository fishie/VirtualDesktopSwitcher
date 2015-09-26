using System.Windows.Forms;

namespace VirtualDesktopSwitcher
{
    class HiddenContext : ApplicationContext
    {
        public HiddenContext()
        {
            var form = new VirtualDesktopSwitcherForm();
            if (form.hideOnStartup == false) form.Show();
        }
    }
}
