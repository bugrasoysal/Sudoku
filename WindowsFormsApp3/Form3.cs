using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
                Pen mypen = new Pen(Color.Black);

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            for (int i = 70; i <= 560; i = i + 70)
            {
                Point point = new Point(0, i);
                Point point2 = new Point(630, i);
                Point point3 = new Point(i, 0);
                Point point4 = new Point(i, 630);
                if (i == 210 || i == 420)
                {
                    mypen.Width = 3;
                }
                g.DrawLine(mypen, point, point2);
                g.DrawLine(mypen, point3, point4);
                mypen.Width = 1;
            }
        }
    }
}
