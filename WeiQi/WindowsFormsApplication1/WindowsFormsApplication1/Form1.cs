using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Video MyVideo = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyVideo = new Video(Application.StartupPath + "\\musicRes\\片头6.wmv", false);
            int width = panel1.Width;
            int height = panel1.Height;
            MyVideo.Owner = panel1;
            panel1.Width = width;
            panel1.Height = height;
            MyVideo.Play();
            timer1.Interval = 6000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
            timer1.Stop();
        }
    }
}
