namespace WindowsFormsApplication1
{
    partial class OL_Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OL_Game));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返回主菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.投降ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.悔棋ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.声音ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.音量选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.棋子声音ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.背景声音ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.游戏名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.反馈ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示我们的GitHub网址ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示我们的邮箱ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("幼圆", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.游戏ToolStripMenuItem,
            this.声音ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.反馈ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.menuStrip1.Size = new System.Drawing.Size(820, 80);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存游戏ToolStripMenuItem,
            this.退出游戏ToolStripMenuItem});
            this.文件ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文件ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4);
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 保存游戏ToolStripMenuItem
            // 
            this.保存游戏ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.保存游戏ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.保存游戏ToolStripMenuItem.Name = "保存游戏ToolStripMenuItem";
            this.保存游戏ToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.保存游戏ToolStripMenuItem.Text = "保存游戏";
            this.保存游戏ToolStripMenuItem.Click += new System.EventHandler(this.保存游戏ToolStripMenuItem_Click_1);
            // 
            // 退出游戏ToolStripMenuItem
            // 
            this.退出游戏ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.退出游戏ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.退出游戏ToolStripMenuItem.Name = "退出游戏ToolStripMenuItem";
            this.退出游戏ToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.退出游戏ToolStripMenuItem.Text = "退出游戏";
            this.退出游戏ToolStripMenuItem.Click += new System.EventHandler(this.退出游戏ToolStripMenuItem_Click_1);
            // 
            // 游戏ToolStripMenuItem
            // 
            this.游戏ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.返回主菜单ToolStripMenuItem,
            this.投降ToolStripMenuItem,
            this.悔棋ToolStripMenuItem});
            this.游戏ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.游戏ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.游戏ToolStripMenuItem.Name = "游戏ToolStripMenuItem";
            this.游戏ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4);
            this.游戏ToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.游戏ToolStripMenuItem.Text = "游戏";
            // 
            // 返回主菜单ToolStripMenuItem
            // 
            this.返回主菜单ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回主菜单ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.返回主菜单ToolStripMenuItem.Name = "返回主菜单ToolStripMenuItem";
            this.返回主菜单ToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.返回主菜单ToolStripMenuItem.Text = "返回菜单";
            this.返回主菜单ToolStripMenuItem.Click += new System.EventHandler(this.返回主菜单ToolStripMenuItem_Click_1);
            // 
            // 投降ToolStripMenuItem
            // 
            this.投降ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.投降ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.投降ToolStripMenuItem.Name = "投降ToolStripMenuItem";
            this.投降ToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.投降ToolStripMenuItem.Text = "弃步";
            this.投降ToolStripMenuItem.Click += new System.EventHandler(this.投降ToolStripMenuItem_Click);
            // 
            // 悔棋ToolStripMenuItem
            // 
            this.悔棋ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F);
            this.悔棋ToolStripMenuItem.Name = "悔棋ToolStripMenuItem";
            this.悔棋ToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.悔棋ToolStripMenuItem.Text = "悔棋";
            this.悔棋ToolStripMenuItem.Click += new System.EventHandler(this.悔棋ToolStripMenuItem_Click);
            // 
            // 声音ToolStripMenuItem
            // 
            this.声音ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.音量选择ToolStripMenuItem,
            this.棋子声音ToolStripMenuItem,
            this.背景声音ToolStripMenuItem});
            this.声音ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.声音ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.声音ToolStripMenuItem.Name = "声音ToolStripMenuItem";
            this.声音ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4);
            this.声音ToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.声音ToolStripMenuItem.Text = "声音";
            // 
            // 音量选择ToolStripMenuItem
            // 
            this.音量选择ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.音量选择ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.音量选择ToolStripMenuItem.Name = "音量选择ToolStripMenuItem";
            this.音量选择ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.音量选择ToolStripMenuItem.Text = "音量选择";
            this.音量选择ToolStripMenuItem.Click += new System.EventHandler(this.音量选择ToolStripMenuItem_Click_1);
            // 
            // 棋子声音ToolStripMenuItem
            // 
            this.棋子声音ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.棋子声音ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.棋子声音ToolStripMenuItem.Name = "棋子声音ToolStripMenuItem";
            this.棋子声音ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.棋子声音ToolStripMenuItem.Text = "开/关棋子声音";
            this.棋子声音ToolStripMenuItem.Click += new System.EventHandler(this.棋子声音ToolStripMenuItem_Click_1);
            // 
            // 背景声音ToolStripMenuItem
            // 
            this.背景声音ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.背景声音ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.背景声音ToolStripMenuItem.Name = "背景声音ToolStripMenuItem";
            this.背景声音ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.背景声音ToolStripMenuItem.Text = "开/关背景声音";
            this.背景声音ToolStripMenuItem.Click += new System.EventHandler(this.背景声音ToolStripMenuItem_Click_1);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.游戏名称ToolStripMenuItem,
            this.版本号ToolStripMenuItem,
            this.关于ToolStripMenuItem1});
            this.关于ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.关于ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4);
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 游戏名称ToolStripMenuItem
            // 
            this.游戏名称ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.游戏名称ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.游戏名称ToolStripMenuItem.Name = "游戏名称ToolStripMenuItem";
            this.游戏名称ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.游戏名称ToolStripMenuItem.Text = "游戏名称";
            this.游戏名称ToolStripMenuItem.Click += new System.EventHandler(this.游戏名称ToolStripMenuItem_Click);
            // 
            // 版本号ToolStripMenuItem
            // 
            this.版本号ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.版本号ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.版本号ToolStripMenuItem.Name = "版本号ToolStripMenuItem";
            this.版本号ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.版本号ToolStripMenuItem.Text = "版本号";
            this.版本号ToolStripMenuItem.Click += new System.EventHandler(this.版本号ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem1
            // 
            this.关于ToolStripMenuItem1.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.关于ToolStripMenuItem1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.关于ToolStripMenuItem1.Name = "关于ToolStripMenuItem1";
            this.关于ToolStripMenuItem1.Size = new System.Drawing.Size(182, 26);
            this.关于ToolStripMenuItem1.Text = "开发小组名";
            this.关于ToolStripMenuItem1.Click += new System.EventHandler(this.关于ToolStripMenuItem1_Click);
            // 
            // 反馈ToolStripMenuItem
            // 
            this.反馈ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示我们的GitHub网址ToolStripMenuItem,
            this.显示我们的邮箱ToolStripMenuItem});
            this.反馈ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.反馈ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.反馈ToolStripMenuItem.Name = "反馈ToolStripMenuItem";
            this.反馈ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4);
            this.反馈ToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.反馈ToolStripMenuItem.Text = "反馈";
            // 
            // 显示我们的GitHub网址ToolStripMenuItem
            // 
            this.显示我们的GitHub网址ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.显示我们的GitHub网址ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.显示我们的GitHub网址ToolStripMenuItem.Name = "显示我们的GitHub网址ToolStripMenuItem";
            this.显示我们的GitHub网址ToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.显示我们的GitHub网址ToolStripMenuItem.Text = "我们的GitHub网址";
            this.显示我们的GitHub网址ToolStripMenuItem.Click += new System.EventHandler(this.显示我们的GitHub网址ToolStripMenuItem_Click);
            // 
            // 显示我们的邮箱ToolStripMenuItem
            // 
            this.显示我们的邮箱ToolStripMenuItem.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.显示我们的邮箱ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.显示我们的邮箱ToolStripMenuItem.Name = "显示我们的邮箱ToolStripMenuItem";
            this.显示我们的邮箱ToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.显示我们的邮箱ToolStripMenuItem.Text = "我们的邮箱";
            this.显示我们的邮箱ToolStripMenuItem.Click += new System.EventHandler(this.显示我们的邮箱ToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.treeView1.Font = new System.Drawing.Font("方正喵呜体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(565, 332);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(235, 251);
            this.treeView1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("方正喵呜体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(637, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 21);
            this.label1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("方正喵呜体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(637, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 21);
            this.label2.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(662, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 38);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(662, 228);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(86, 42);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("方正喵呜体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(675, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 28);
            this.label3.TabIndex = 15;
            this.label3.Text = "提子结束";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.TabIndex = 16;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // OL_Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.游戏背景图_new;
            this.ClientSize = new System.Drawing.Size(820, 600);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OL_Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "某科学的勇者围棋";
            this.Load += new System.EventHandler(this.OL_Game_Load);
            this.Click += new System.EventHandler(this.OL_Game_Click);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存游戏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出游戏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 游戏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 返回主菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 投降ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 声音ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 音量选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 棋子声音ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 背景声音ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 游戏名称ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 反馈ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示我们的GitHub网址ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示我们的邮箱ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripMenuItem 悔棋ToolStripMenuItem;
    }
}