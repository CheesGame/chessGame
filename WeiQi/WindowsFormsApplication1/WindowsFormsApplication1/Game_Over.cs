using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Game_Over : Form
    {
        public bool win=true;
        public Game_Over()
        {
            InitializeComponent();
        }
        public Game_Over(int blackStone,int whiteStone) {
            InitializeComponent();
            label1.Text = blackStone.ToString();
            label2.Text = whiteStone.ToString();
            if (blackStone > (whiteStone + 4))
            { 
                //黑棋赢了
                label3.Text = "黑棋";
                label4.Text = "白棋";
            }
            else if (blackStone < (whiteStone + 4))
            {
                //白棋赢了
                label3.Text = "白棋";
                label4.Text = "黑棋";
            }
            else { 
                //平了
                label3.Text = "黑棋,白棋";
                label4.Text = "";
            }
        }
        private void Game_Over_Load(object sender, EventArgs e)
        {

        }
    }
}
