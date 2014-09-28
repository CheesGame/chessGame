using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicGame
{
    public partial class Form1 : Form
    {
        private Gobang gobang1;
        private System.ComponentModel.IContainer components = null;
        public Form1()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.gobang1 = new basicGame.Gobang();
            this.SuspendLayout();
            // 
            // gobang1
            // 
            this.gobang1.BackColor = System.Drawing.Color.Wheat;
            this.gobang1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gobang1.Horizontal = 19;
            this.gobang1.Location = new System.Drawing.Point(10,10);
            this.gobang1.Name = "gobang1";
            this.gobang1.PaintLine = System.Drawing.Color.Black;
            this.gobang1.Size = new System.Drawing.Size(581, 502);
            this.gobang1.TabIndex = 0;
            this.gobang1.Text = "gobang1";
            this.gobang1.Vertical = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 502);
            this.Controls.Add(this.gobang1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }
    }
}
