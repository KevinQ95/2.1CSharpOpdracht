using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class CustomButton : Button
    {
        public static Size size = new Size(67, 100);
        public static Color _activeBorder = System.Drawing.Color.Green;
        public int index;

        public CustomButton():base()
        {
            base.Size = size;
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);

                base.FlatAppearance.BorderColor = _activeBorder;
        }

    }


}
