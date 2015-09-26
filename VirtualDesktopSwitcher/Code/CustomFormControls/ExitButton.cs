using System.Drawing;
using System.Windows.Forms;

namespace VirtualDesktopSwitcher.Code.CustomFormControls
{
    class ExitButton : UnselectableButton
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            const float thickness = 2f;
            const int offset = 6;

            using (var pen = new Pen(Color.White, thickness))
            {
                pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                pevent.Graphics.DrawLine(pen,
                    new Point(offset, offset),
                    new Point(Width - offset - 1, Height - offset - 1));
                pevent.Graphics.DrawLine(pen,
                    new Point(Width - offset - 1, offset),
                    new Point(offset, Height - offset - 1));
            }
        }
    }
}
