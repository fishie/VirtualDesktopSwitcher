using System.Windows.Forms;

namespace VirtualDesktopSwitcher
{
    class HiddenContext : ApplicationContext
    {
        public HiddenContext()
        {
            var form = new VirtualDesktopSwitcherForm();
            if (form.hideOnStartup == false) form.Show();
            form.FormClosed += new FormClosedEventHandler(FormClosed);
        }

        void FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitThread();
        }
    }
}
