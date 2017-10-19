using PacketLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class CustomButton : Button
    {
        //public static Size Size { get; set; }
        public static Color _activeBorder { get; set; }
        //public CardPacket CardPacket { get; set; }
        public int Index { get; set; }

        public CustomButton() : base()
        {
            Size = new Size(67, 100);
            _activeBorder = Color.Green;
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);

            FlatAppearance.BorderColor = _activeBorder;
        }

        protected override void OnClick(EventArgs e)
        {
            ClientForm.Form1.SendCardToServer(Index);
            //Console.WriteLine("send " + CardPacket.ToString());
        }

    }

}
