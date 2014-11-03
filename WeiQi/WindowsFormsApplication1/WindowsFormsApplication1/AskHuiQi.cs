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
    public partial class AskHuiQi : Form
    {
        public bool ok = true;
        public AskHuiQi()
        {
            InitializeComponent();
        }
        //同意
        private void label2_Click(object sender, EventArgs e)
        {
            this.ok = true;
            this.Close();
        }
        //不同意
        private void label1_Click(object sender, EventArgs e)
        {
            this.ok = false;
            this.Close();
        }
    }
}
