using System.Windows.Forms;

namespace VirtualDesktopSwitcher.Code
{
    class HiddenContext : ApplicationContext
    {
        public HiddenContext()
        {
            var form = new VirtualDesktopSwitcherForm();
            form.Show();
            if (form.HideOnStartup) form.Hide();
        }
    }
}
