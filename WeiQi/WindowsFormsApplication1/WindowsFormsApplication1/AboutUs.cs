using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using algorith_moveInChess;

namespace WindowsFormsApplication1
{
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int xCoordinate=MousePosition.X-this.Location.X;
            int yCoordinate=MousePosition.Y-this.Location.Y;
            if (xCoordinate > 208 && xCoordinate < 438 && yCoordinate > 540 && yCoordinate < 600)
                pictureBox1.Visible = true;
            else pictureBox1.Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
        }

    }
}
