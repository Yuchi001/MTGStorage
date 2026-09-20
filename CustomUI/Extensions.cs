using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MTGStorage.CustomUI
{
    public static class Extensions
    {
        public static void RoundElement(this Control control, int radius)
        {
            var path = new GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
        }
    }
}