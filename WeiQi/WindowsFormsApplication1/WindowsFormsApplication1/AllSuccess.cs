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
    public partial class AllSuccess : Form
    {
        public AllSuccess()
        {
            InitializeComponent();
        }
        public AllSuccess(string successMessage)
        {
            InitializeComponent();
            label1.Text = successMessage;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
