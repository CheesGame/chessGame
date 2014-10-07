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
using algorith_moveInChess;
using Microsoft.VisualBasic.PowerPacks;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        Board board = new Board();

        TreeNode qipu = new TreeNode("棋谱");
        Step step = new Step();
        Boolean flag = true;
        private Step step0 = new Step();
        ShapeContainer sh = new ShapeContainer();//画棋盘的容器
        int[] xLocation = { 60, 86, 112, 137, 163, 189, 214, 240, 266, 291, 317, 343, 367, 393, 419, 443, 470, 496, 520 };
        int[] yLocation = { 108, 133, 158, 182, 208, 233, 258, 282, 307, 332, 357, 382, 407, 431, 456, 481, 506, 532, 556 };
        public Form3()
        {
            InitializeComponent();
            Controls.Add(sh);
            pictureBox1.Visible = false;
            this.treeView1.Nodes.Add(qipu);
            ImageList imageList = new ImageList();
            imageList.Images.Add(Image.FromFile("E://black.jpg"));
            imageList.Images.Add(Image.FromFile("E://white.jpg"));
            this.treeView1.ImageList = imageList;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        private string OpenFile()
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "sgf files (*.sgf)|*.sgf|All Files (*.*)|*.*";
            openDlg.FileName = "";
            openDlg.DefaultExt = ".sgf";
            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;
            string filename = "";

            DialogResult res = openDlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (!(openDlg.FileName).EndsWith(".sgf") && !(openDlg.FileName).EndsWith(".SGF"))
                    MessageBox.Show("Unexpected file format", "Super Go Format", MessageBoxButtons.OK);
                else
                    filename = openDlg.FileName;
            }
            return filename;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //表示当前鼠标的横纵坐标
            int xCoordinate = MousePosition.X - this.Location.X;
            int yCoordinate = MousePosition.Y - this.Location.Y;
            //label1可以显示当前鼠标所在位置的横纵坐标
            label1.Text = "x:" + xCoordinate + "y:" + yCoordinate;
            //判断鼠标是否在格子里面（上下左右各有10个像素点的判断：位于表格外的10个像素点也可以判断进行显示）
            if (xCoordinate > 50 && xCoordinate < 530 && yCoordinate > 98 && yCoordinate < 566)
            {
                //方格需要显示的横纵坐标（据我判断，应该是中心坐标）
                int pbX, pbY;
                //计算距离左边近还是距离右边近
                pbX = 0;
                pbY = 0;
                if (xCoordinate - 10 < xLocation[0])
                {
                    pbX = xLocation[0] - 10;
                }
                else
                {
                    if (xCoordinate - 10 > xLocation[18])
                    {
                        pbX = xLocation[18] - 10;
                    }
                    else
                    {
                        for (int i = 0; i < 19; i++)
                        {
                            if (xCoordinate - 10 > xLocation[i])
                                continue;
                            else
                            {
                                pbX = xLocation[i] - 10;
                                break;
                            }
                        }
                    }
                }
                if (yCoordinate - 10 < yLocation[0])
                {
                    pbY = yLocation[0] - 10;
                }
                else
                {
                    if (yCoordinate - 10 > yLocation[18])
                    {
                        pbY = xLocation[18] - 10;
                    }
                    else
                    {
                        for (int i = 0; i < 19; i++)
                        {
                            if (yCoordinate - 10 > yLocation[i])
                                continue;
                            else
                            {
                                pbY = yLocation[i] - 10;
                                break;
                            }
                        }
                    }
                }
                pictureBox1.Location = new Point(pbX, pbY);
                pictureBox1.Visible = true;
                if (flag)
                {
                    pictureBox1.BackColor = Color.Black;
                }
                else
                {
                    pictureBox1.BackColor = Color.White;
                }
            }
            else
            {  //不在棋盘内，隐藏方格
                pictureBox1.Visible = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Piece piece = new Piece();
            PictureBox picturebox2 = new PictureBox();

            if (flag)
            {
                picturebox2.Image = Image.FromFile("E://black.jpg");
                flag = (!flag);
                piece.setColor(1);
            }
            else
            {
                picturebox2.Image = Image.FromFile("E://white.jpg");
                flag = (!flag);
                piece.setColor(-1);
            }
            picturebox2.Height = 20;
            picturebox2.Width = 20;
            picturebox2.SizeMode = PictureBoxSizeMode.StretchImage;

            int xCoordinate = MousePosition.X - this.Location.X;
            int yCoordinate = MousePosition.Y - this.Location.Y;
            //方格需要显示的横纵坐标（据我判断，应该是中心坐标）
            int pbX, pbY;
            //计算距离左边近还是距离右边近
            pbX = 0;
            pbY = 0;
            if (xCoordinate > 50 && xCoordinate < 530 && yCoordinate > 98 && yCoordinate < 566)
            {

                if (xCoordinate - 10 < xLocation[0])
                {
                    pbX = xLocation[0] - 10;
                    piece.setXCoordinate(1);
                }
                else
                {
                    if (xCoordinate - 10 > xLocation[18])
                    {
                        pbX = xLocation[18] - 10;
                        piece.setXCoordinate(19);
                    }
                    else
                    {
                        for (int i = 0; i < 19; i++)
                        {
                            if (xCoordinate - 10 > xLocation[i])
                                continue;
                            else
                            {
                                pbX = xLocation[i] - 10;
                                piece.setXCoordinate(i + 1);
                                break;
                            }
                        }
                    }
                }
                if (yCoordinate - 10 < yLocation[0])
                {
                    pbY = yLocation[0] - 10;
                    piece.setYCoordinate(1);
                }
                else
                {
                    if (yCoordinate - 10 > yLocation[18])
                    {
                        pbY = xLocation[18] - 10;
                        piece.setYCoordinate(19);
                    }
                    else
                    {
                        for (int i = 0; i < 19; i++)
                        {
                            if (yCoordinate - 10 > yLocation[i])
                                continue;
                            else
                            {
                                pbY = yLocation[i] - 10;
                                piece.setYCoordinate(i);
                                break;
                            }
                        }
                    }
                }
                picturebox2.Location = new Point(pbX, pbY);

            }
            OvalShape a = new OvalShape();
            a.Size = new Size(20, 20);
            a.Location = new Point(pbX, pbY);
            a.BackStyle = BackStyle.Opaque;
            a.Tag = step.getNumberOfStep();
            if (piece.getColor() < 0)
            {
                a.BackColor = Color.White;
                //    panduan(1);
            }
            else
                a.BackColor = Color.Black;
            sh.Shapes.Add(a);
            //     this.Controls.Add(picturebox2);
            //      picturebox2.Show();
            step.addStep(piece);


            Piece [] hehe=new Piece[400];
            piece.setBelong(board);
            hehe=piece.playChess(piece.getXCoordinate(),piece.getYCoordinate(),piece.getColor());

            TreeNode node = new TreeNode(piece.getXCoordinate() + "," + piece.getYCoordinate());
            if (piece.getColor() < 0) node.SelectedImageIndex = node.ImageIndex = 1;
            else node.SelectedImageIndex = node.ImageIndex = 0;
            qipu.Nodes.Add(node);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageList imageList = new ImageList();
            imageList.Images.Add(Image.FromFile("E://black.jpg"));
            imageList.Images.Add(Image.FromFile("E://white.jpg"));
            string filename = OpenFile();
            step0.fromFile(filename);
            this.treeView1.ImageList = imageList;
            this.treeView1.Nodes.Add(qipu);
            for (int i = 0; i < step0.getNumberOfStep(); i++)
            {
                int j = 0;
                if (step0.getStep()[i].getColor() < 0) j = 1;
                TreeNode stone = new TreeNode(step0.getStep()[i].getXCoordinate() + "," + step0.getStep()[i].getYCoordinate());
                stone.ImageIndex = j;
                stone.SelectedImageIndex = j;
                qipu.Nodes.Add(stone);
            }
        }
        private void SaveFile()
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.AddExtension = true;
            saveDlg.RestoreDirectory = true;
            saveDlg.CheckPathExists = true;
            saveDlg.FileName = "";
            saveDlg.Filter = "sgf files (*.sgf)|*.sgf|All Files (*.*)|*.*";

            DialogResult res = saveDlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (!(saveDlg.FileName).EndsWith(".sgf") && !(saveDlg.FileName).EndsWith(".SGF"))
                {
                    MessageBox.Show("Unexpected file format", "Super Go Format", MessageBoxButtons.OK);

                }
                else
                {
                    string file = saveDlg.FileName.ToString();
                    string content = step0.toString();
                    if (File.Exists(file) == true)
                    {
                        MessageBox.Show("存在此文件!");
                    }
                    else
                    {
                        FileStream myFs = new FileStream(file, FileMode.Create);
                        StreamWriter mySw = new StreamWriter(myFs);
                        mySw.Write(content);
                        mySw.Close();
                        myFs.Close();
                        MessageBox.Show("写入成功");
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        int times = 0;
        private void treeView1_Click(object sender, EventArgs e)
        {
            times = times + 1;
            if (times<=2) return;
            String[] coordinate=new String[3];
            coordinate = treeView1.SelectedNode.Text.Split(',');
            if (coordinate[0] == "棋谱") return;
            int xChi = int.Parse(coordinate[0]);
            int yChi = int.Parse(coordinate[1]);
            int number = 0;
            for (number = 0; number < step.getNumberOfStep(); number++)
            {
                if (xChi == step.getStep()[number].getXCoordinate() && yChi == step.getStep()[number].getYCoordinate()) break;
            }
            foreach (Shape i in sh.Shapes)
                if (number.ToString() == i.Tag.ToString())
                {
                    sh.Shapes.Remove(i);
                    break;//遍历中删除元素必须退出
                }
 
        }



    }
}
