using System.Drawing;
using System.Windows.Forms;

namespace VirtualDesktopSwitcher.Code.CustomFormControls
{
    class MinimizeButton : UnselectableButton
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            const float thickness = 2f;
            const int offset = 6;
            var height = Height - offset - 1;

            using (var pen = new Pen(ForeColor, thickness))
            {
                pevent.Graphics.DrawLine(pen,
                    new Point(offset, height),
                    new Point(Width - offset, height));
            }
        }
    }
}
