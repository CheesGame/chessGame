using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using algorith_moveInChess;

namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        //System.Media.SoundPlayer Music = new System.Media.SoundPlayer(Application.StartupPath + "//musicRes//pipa.wav");
        
        public Main()
        {
            InitializeComponent();
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
            Form1 form1 = new Form1();
            form1.Show();
            Music music = new Music();
            music.StartMusic();
       }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
        //点击开始“单机游戏”界面
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SA_Game form3 = new SA_Game();
            form3.Show();
            form3.FormClosing += Form_Closing;

        }
        //点击进入双人对战界面
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Battle_Entry form3 = new Battle_Entry();
            form3.Show();
            form3.FormClosing += Form_Closing;
        }
        //点击进入“关于我们”界面
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs form2 = new AboutUs();
            form2.Show();
            form2.FormClosing += Form_Closing;
        }
        //退出游戏
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int xCoordinate = MousePosition.X - this.Location.X;
            int yCoordinate = MousePosition.Y - this.Location.Y;
            if (xCoordinate < 210 && xCoordinate > 30)
            {
                if (yCoordinate > 270 && yCoordinate < 330)
                {
                    pictureBox2.Visible = true;
                    pictureBox6.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox7.Visible = true;
                    pictureBox8.Visible = true;
                    pictureBox9.Visible = true;
                }
                else if (yCoordinate > 330 && yCoordinate < 390)
                {
                    pictureBox3.Visible = true;
                    pictureBox7.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                    pictureBox8.Visible = true;
                    pictureBox9.Visible = true;
                }
                else if (yCoordinate > 390 && yCoordinate < 450)
                {
                    pictureBox4.Visible = true;
                    pictureBox8.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                    pictureBox7.Visible = true;
                    pictureBox9.Visible = true;
                }
                else if (yCoordinate > 450 && yCoordinate < 510)
                {
                    pictureBox5.Visible = true;
                    pictureBox9.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox6.Visible = true;
                    pictureBox7.Visible = true;
                    pictureBox8.Visible = true;
                }
                else
                {
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                    pictureBox7.Visible = true;
                    pictureBox8.Visible = true;
                    pictureBox9.Visible = true;
                }
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
            }
        }

    }
}