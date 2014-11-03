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
    public partial class AllError : Form
    {
        public AllError()
        {
            InitializeComponent();
        }
        public AllError(string errorMessage)
        {
            InitializeComponent();
            label1.Text = errorMessage;
        }
        public string errorMessage;
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = errorMessage;
        }
    }
}
