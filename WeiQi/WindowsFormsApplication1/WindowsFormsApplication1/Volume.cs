

using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Text;

using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Volume : Form
    {
        [DllImport("Winmm.dll")]

        private static extern int waveOutSetVolume(int hwo, System.UInt32 pdwVolume);//设置音量

        [DllImport("Winmm.dll")]

        private static extern uint waveOutGetVolume(int hwo, out System.UInt32 pdwVolume); //获取音量
        public Volume()
        {
            InitializeComponent();

            uint v;

            uint i = waveOutGetVolume(0, out v);

            uint vleft = v & 0xFFFF;

            uint vright = (v & 0xFFFF0000) >> 16;

            trackBar1.Value = (int.Parse(vleft.ToString()) | int.Parse(vright.ToString())) * (this.trackBar1.Maximum - this.trackBar1.Minimum) / 0xFFFF;
       
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.UInt32 Value = (System.UInt32)((double)0xffff * (double)trackBar1.Value / (double)(trackBar1.Maximum - trackBar1.Minimum));//先把trackbar的value值映射到0x0000～0xFFFF范围

            //限制value的取值范围

            if (Value < 0) Value = 0;

            if (Value > 0xffff) Value = 0xffff;

            System.UInt32 left = (System.UInt32)Value;//左声道音量

            System.UInt32 right = (System.UInt32)Value;//右

            waveOutSetVolume(0, left << 16 | right); //"<<"左移，“|”逻辑或运算
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
