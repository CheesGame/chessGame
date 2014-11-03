using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using clientSocket;
namespace WindowsFormsApplication1
{
    public partial class Battle_Entry : Form
    {
        public string user;
        public string password;
        public Client player=new Client();
        public bool firstFlag = false;
        public Battle_Entry()
        {
            InitializeComponent();
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
        private bool vertify()
        {
            if (player.getReceiveMessage() != "false")
            {
                //string[] recieveMessageSplit = player.getReceiveMessage().Split(',');
                //if (recieveMessageSplit[1] == "first") {
                //    firstFlag = true;
                //}
                //return true;
                return true;
            }
            else
                return false;
        }
        public string infor;
        private void label2_Click(object sender, EventArgs e)
        {

            player.setSendMessage(textBox1.Text + "," + textBox2.Text+",0");
            player.send();
            player.receive();
            if (vertify())
            {
                user = textBox1.Text;
                password = textBox2.Text;
                string[] split = player.getReceiveMessage().Split(':');
                infor = split[1];
                DaTing form3 = new DaTing(player,split[1], textBox1.Text);
                form3.Show();
                this.Hide();
                form3.FormClosing += Form_Closing;
            }
            else
            {
                AllError error = new AllError("登陆失败！");
                error.Show();
                return;
            }
        }
        private bool isExist() {
            FileStream f = new FileStream(Application.StartupPath + "//users//users.txt", FileMode.Open);
            StreamReader r = new StreamReader(f);
            string fileString = r.ReadToEnd();
            r.Close();
            f.Close();
            String[] fileSplit = new String[10000];
            fileSplit = fileString.Split(';');
            for (int i = 1; i < fileSplit.Length; i++)
            {
                string[] user = new string[5];
                user = fileSplit[i].Split(':');
                if (user[1] == textBox1.Text)
                {
                    return true;
                }
            }
            return false;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            player.setSendMessage(textBox1.Text + "," + textBox2.Text + ",1");
            player.send();
            player.receive();
            if (vertify())
            {
                user = textBox1.Text;
                password = textBox2.Text;
                string[] split = player.getReceiveMessage().Split(':');
                infor = split[1];
                AllSuccess success = new AllSuccess("注册成功！");
                success.ShowDialog();
                DaTing form3 = new DaTing(player, split[1], textBox1.Text);
                form3.Show();
                this.Hide();
                form3.FormClosing += Form_Closing;
            }
            else
            {
                AllError error = new AllError("注册失败！");
                error.Show();
                return;
            }
            
        }   //注册的问题没有调！！！！！！！

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Battle_Entry_Load(object sender, EventArgs e)
        {

        }
    }
}
