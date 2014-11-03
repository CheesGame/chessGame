using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clientSocket;
namespace WindowsFormsApplication1
{
    public partial class DaTing : Form
    {
        public bool firstflag = true;
        public string namestring;
        public Client player;
        public string[] name = new string[12];
        public int[] groupNo = new int[12];
        public DaTing(Client client,string content,string namestring) {
            player = client;
            this.namestring = namestring;
            initFunction();
            getName(content);
        }
        public DaTing()
        {
            initFunction();
        }
        //输入标号，返回对手号
        public int getSequence(int number) {
            for (int i = 0; i < 12; i++) {
                if (groupNo[i] == groupNo[number] && i!=number)
                    return i;
            }
            return number;
        }
        //解析从服务器传过来的数据
        public void getName(string content){
            string[] contentSplit = content.Split(';');
            for (int i = 1; i < contentSplit.Count(); i++) {
                if (contentSplit[i] == "") continue;
                else {
                    groupNo[i-1] = int.Parse((contentSplit[i]).Split(',')[0]);
                    name[i-1] = contentSplit[i].Split(',')[1];
                }
            }
            displayInfo();
        }
        //将信息展示到form里面
        public void displayInfo() {
            for (int i = 0; i < 12; i++) {
                if (groupNo[i] <= 6 && groupNo[i] > 0) {
                    switch (groupNo[i]) {
                        case 1: {
                            if (getSequence(i) == i)
                            {
                                pictureBox13.Visible = true;
                                label1.Text = name[i];
                            }
                            else {
                                pictureBox7.Visible = true;
                                label2.Text = name[i];
                            }
                        
                        } break;
                        case 2:
                            {
                                if (getSequence(i) == i)
                                {
                                    pictureBox14.Visible = true;
                                    label3.Text = name[i];
                                }
                                else
                                {
                                    pictureBox8.Visible = true;
                                    label4.Text = name[i];
                                }

                            } break;
                        case 3:
                            {
                                if (getSequence(i) == i)
                                {
                                    pictureBox15.Visible = true;
                                    label5.Text = name[i];
                                }
                                else
                                {
                                    pictureBox9.Visible = true;
                                    label6.Text = name[i];
                                }

                            } break;
                        case 4:
                            {
                                if (getSequence(i) == i)
                                {
                                    pictureBox16.Visible = true;
                                    label7.Text = name[i];
                                }
                                else
                                {
                                    pictureBox10.Visible = true;
                                    label8.Text = name[i];
                                }

                            } break;
                        case 5:
                            {
                                if (getSequence(i) == i)
                                {
                                    pictureBox17.Visible = true;
                                    label9.Text = name[i];
                                }
                                else
                                {
                                    pictureBox11.Visible = true;
                                    label10.Text = name[i];
                                }

                            } break;
                        case 6:
                            {
                                if (getSequence(i) == i)
                                {
                                    pictureBox18.Visible = true;
                                    label11.Text = name[i];
                                }
                                else
                                {
                                    pictureBox12.Visible = true;
                                    label12.Text = name[i];
                                }
                            } break;
                    }
                
                }
            
            }
        
        }
        
        public void initFunction(){
            InitializeComponent();
            
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;
            pictureBox16.Visible = false;
            pictureBox17.Visible = false;
            pictureBox18.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            player.setSendMessage("1");
            player.send();
            OL_Game form;
            if (label1.Text !="")
            {
                this.Hide();
                form = new OL_Game(namestring, player, false);
                form.ShowDialog();
                this.Close();
            }
            else {
                this.Hide();
                form = new OL_Game(namestring, player, true);
                form.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            player.setSendMessage("2");
            player.send();
            OL_Game form;
            if (label3.Text != "")
            {
                this.Hide();
                form = new OL_Game(namestring, player, false);
                form.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                form = new OL_Game(namestring, player, true);
                form.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            player.setSendMessage("3");
            player.send();
            OL_Game form;
            if (label5.Text != "")
            {
                this.Hide();
                form = new OL_Game(namestring, player, false);
                form.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                form = new OL_Game(namestring, player, true);
                form.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            player.setSendMessage("4");
            player.send();
            OL_Game form;
            if (label7.Text != "")
            {
                this.Hide();
                form = new OL_Game(namestring, player, false);
                form.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                form = new OL_Game(namestring, player, true);
                form.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            player.setSendMessage("5");
            player.send();
            OL_Game form;
            if (label9.Text != "")
            {
                this.Hide();
                form = new OL_Game(namestring, player, false);
                form.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                form = new OL_Game(namestring, player, true);
                form.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            player.setSendMessage("6");
            player.send();
            OL_Game form;
            if (label11.Text != "")
            {
                this.Hide();
                form = new OL_Game(namestring, player, false);
                form.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                form = new OL_Game(namestring, player, true);
                form.ShowDialog();
                this.Close();
            }
        }
     
    }
}
