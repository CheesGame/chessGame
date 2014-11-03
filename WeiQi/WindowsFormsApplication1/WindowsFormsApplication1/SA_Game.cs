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
using System.Runtime.InteropServices;
using System.Media;
using Microsoft.DirectX.DirectSound;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class SA_Game : Form
    {
        private Board board = new Board();  //棋盘类
        private Step step = new Step(); //棋谱类，记录每一步

        private ShapeContainer sh = new ShapeContainer();//画棋盘的容器
        private int[] xLocation = { 0, 68, 94, 120, 145, 173, 197, 222, 248, 274, 299, 325, 351, 375, 401, 427, 451, 478, 504, 528 };
        private int[] yLocation = { 0, 138, 163, 188, 212, 238, 263, 288, 312, 337, 362, 387, 412, 437, 461, 486, 511, 536, 562, 586 };
        private int xCoordinate, yCoordinate;    //鼠标相对于窗口位置的x坐标和y坐标
        private int pbX, pbY;   //棋子的显示位置
        private Boolean flag = true;    //true表示应该下黑棋，false表示应该下白棋

        private int selectNumber = -1;  //在树状图中选中的棋子的步数
        private int selectNodeX = 0;    //在树状图中选中的棋子的横坐标
        private int selectNodeY = 0;    //在树状图中选中棋子的纵坐标
        private TreeNode qipu = new TreeNode("棋谱"); //创建树状棋谱展示
        private ImageList imageList = new ImageList();  //树节点图片集

        bool ChessSoundFlag = true; //控制播放哪一个音效
        Music music = new Music();
        SecondaryBuffer whiteSound;
        Device secDev1;
        SecondaryBuffer blackSound;
        Device secDev2;
        private Piece[] fileStep = new Piece[1000]; //保存从sgf文件中取出的棋子
        private bool fromFileFlag = false;  //表示棋谱是否是从文件中获得

        private int correntStep = 0;

        public SA_Game()
        {
            InitializeComponent();
            Controls.Add(sh);   //画棋盘容器添加到form里面
            this.treeView1.Nodes.Add(qipu); //添加棋谱的树节点
            imageList.Images.Add(Image.FromFile(Application.StartupPath + "//imageRes//black.jpg"));
            imageList.Images.Add(Image.FromFile(Application.StartupPath + "//imageRes//white.jpg"));
            this.treeView1.ImageList = imageList;
            //Music.Play();
            secDev1 = new Device();
            secDev1.SetCooperativeLevel(this, CooperativeLevel.Normal);
            whiteSound = new SecondaryBuffer(WindowsFormsApplication1.Properties.Resources.STONE1, secDev1);
            secDev2 = new Device();
            secDev2.SetCooperativeLevel(this, CooperativeLevel.Normal);
            blackSound = new SecondaryBuffer(WindowsFormsApplication1.Properties.Resources.STONE2, secDev2);
        }

        private void startM(object sender, EventArgs e)
        {
            //Music.Play();
        }
        //删除树中的所有节点
        private void deleteTreeNode()
        {
            for (; qipu.Nodes.Count != 0; )
            {   //循环删除树桩图的节点
                qipu.Nodes.RemoveAt(qipu.Nodes.Count - 1);
            }
        }
        //删除之前的记录
        private void deleteRecord()
        {
            int number1 = selectNumber + 1;
            int number = selectNumber + 1;  //从1开始数的
            selectNumber = -1;
            int stepnumber = step.getNumberOfStep();
            //删除树形结构中的节点
            if (number1 == -2)
            {
                deleteTreeNode();
                step.deleteElement(0);  //删除step中的existence
                board.resetExistence(); //删除board中的existence
                return;
            }
            for (; number < stepnumber; stepnumber--)
            {   //循环删除树状图的节点
                qipu.Nodes.RemoveAt(stepnumber - 1);
            }
            step.deleteElement(number); //删除step的existence
            board.setExistence(step.existence[step.getNumberOfStep() - 1]);     //删除board的existence
        }
        //form3加载时的触发事件
        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;  //启动计时器
            timer1.Interval = 1;    //刷新周期为1毫秒
            if (xCoordinate > 58 && xCoordinate < 538 && yCoordinate > 128 && yCoordinate < 596)
            {
                if ((selectNumber >= 0 || selectNumber == -3) && selectNumber != step.getNumberOfStep())
                {
                    deleteRecord();
                }
                xiaqi();
            }
        }
        //timer触发事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            xCoordinate = MousePosition.X - this.Location.X;
            yCoordinate = MousePosition.Y - this.Location.Y;

            pbX = xLocation[getPbX(xCoordinate)] - 10;
            pbY = yLocation[getPbY(yCoordinate)] - 10;

        }
        //获得棋子应该显示的位置
        private int getPbX(int xCoordinate)
        {
            if (xCoordinate - 10 < xLocation[1]) return 1;
            else if (xCoordinate - 10 > xLocation[19]) return 19;
            else
                for (int i = 1; i < 20; i++)
                {
                    if (xCoordinate - 10 > xLocation[i]) continue;
                    else return i;
                }
            return 0;
        }
        //获得棋子应该显示的位置
        private int getPbY(int yCoordinate)
        {
            int pbY = 0;   //棋子应该显示的位置的y坐标
            if (yCoordinate - 10 < yLocation[1]) return 1;
            else if (yCoordinate - 10 > yLocation[19]) return 19;
            else
                for (int i = 1; i < 20; i++)
                {
                    if (yCoordinate - 10 > yLocation[i]) continue;
                    else return i;
                }
            return 0;
        }
        //用ovalshape画棋子
        private void drawStone(Piece piece, int tag)
        {
            OvalShape a = new OvalShape();
            a.Size = new Size(20, 20);
            a.Location = new Point(xLocation[piece.getXCoordinate()] - 18, yLocation[piece.getYCoordinate()] - 40);
            a.BackStyle = BackStyle.Opaque;
            a.Tag = tag;
            if (piece.getColor() == -1)
            {
                a.BackColor = Color.White;
                if (ChessSoundFlag)
                {
                    whiteSound.Play(0, BufferPlayFlags.Default);
                }
            }
            else
            {
                a.BackColor = Color.Black;
                if (ChessSoundFlag)
                {
                    blackSound.Play(0, BufferPlayFlags.Default);
                }
            }
            sh.Shapes.Add(a);   //在container中加入棋子
        }
        //删除一个棋子
        private void deleteStone(int i)
        {
            foreach (Shape j in sh.Shapes)
                if (i.ToString() == j.Tag.ToString())
                {
                    sh.Shapes.Remove(j);
                    return;
                }
        }
        //吃掉piece中的子
        private void chiStone(Piece[] piece)
        {
            for (int i = 0; piece[i] != null; i++)
                for (int j = 0; j < step.getNumberOfStep(); j++)
                    if (piece[i].getXCoordinate() == step.getStep()[j].getXCoordinate() && piece[i].getYCoordinate() == step.getStep()[j].getYCoordinate())
                        deleteStone(j);
        }
        //判断是否同形
        public bool isSame()
        {
            if (step.getNumberOfStep() < 2)
                return false;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (board.existence[i][j] != step.existence[step.getNumberOfStep() - 2][i][j])
                        return false;
                }
            }
            return true;
        }
        //点击下棋
        private void xiaqi()
        {
            Piece piece = new Piece();  //新建一个棋子
            flag = (step.getNumberOfStep() % 2 == 0);
            if (flag) piece.setColor(1);  //设置棋子的颜色
            else piece.setColor(-1);
            //设置棋子的位置坐标
            if (fromFileFlag)
            {
                piece.setXCoordinate(xCoordinate);
                piece.setYCoordinate(yCoordinate);
            }
            else
            {
                piece.setXCoordinate(getPbX(xCoordinate));
                piece.setYCoordinate(getPbY(yCoordinate));
            }
            Piece[] hehe = new Piece[400];
            piece.setBelong(board);
            hehe = piece.playChess(piece.getXCoordinate(), piece.getYCoordinate(), piece.getColor());
            if (hehe == null)
            { //在同一点重复下子或者下的子气为0，则错误提示
                AllError error = new AllError("这里不能下棋！");
                error.Show();
                return;
            }
            if (isSame())
            { //判断是否同形，同形则错误提示
                AllError error = new AllError("这里不能下棋！");
                board.existence[getPbX(xCoordinate)][getPbY(yCoordinate)] = false;
                error.Show();
                return;
            }
            drawStone(piece, step.getNumberOfStep());   //画棋子
            chiStone(hehe); //吃子
            //新建一个树节点显示棋谱
            TreeNode node = new TreeNode((step.getNumberOfStep() + 1) + ":  " + piece.getXCoordinate() + "," + piece.getYCoordinate());
            if (piece.getColor() < 0) node.SelectedImageIndex = node.ImageIndex = 1;
            else node.SelectedImageIndex = node.ImageIndex = 0;
            qipu.Nodes.Add(node);

            step.addStep(piece);    //在棋谱中存入棋子
            step.addExistence(board.existence); //记录保存棋盘中所有棋子的位置
            step.stepIncrease();

            correntStep = step.getNumberOfStep();
        }
        //打开棋谱操作，返回文件路径
        private string OpenFile()
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "sgf files (*.sgf)|*.sgf|All Files (*.*)|*.*";
            openDlg.FileName = "";
            openDlg.DefaultExt = ".sgf";
            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;
            string filename = "";

            DialogResult res = openDlg.ShowDialog();    //打开文件选择框

            if (res == DialogResult.OK)
            {
                if (!(openDlg.FileName).EndsWith(".sgf") && !(openDlg.FileName).EndsWith(".SGF"))
                {
                    AllError error = new AllError("文件格式错误！");
                    error.Show();
                }
                else
                    return openDlg.FileName;
            }
            return null;
        }
        //处理sgf文件
        public void fromFile(string path)
        {
            //读文件
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string fileString = r.ReadToEnd();
            r.Close();
            f.Close();
            String[] fileSplit = new String[10000];
            fileSplit = fileString.Split(';');
            for (int i = 2; i < fileSplit.Length; i++)
            {
                Piece stone = new Piece();
                if (fileSplit[i][0] == 'B') stone.setColor(1);
                else if (fileSplit[i][0] == 'W') stone.setColor(-1);
                else break;
                stone.setXCoordinate(fileSplit[i][2] - 'a' + 1);
                stone.setYCoordinate(fileSplit[i][3] - 'a' + 1);
                fileStep[i - 2] = new Piece(stone.getColor(), stone.getXCoordinate(), stone.getYCoordinate());
            }
        }
        //清空fileStep中的信息
        public void deleteFileStep()
        {
            for (int i = 0; fileStep[i] != null && i < fileStep.Count(); i++)
            {
                fileStep[i] = null;
            }
        }

        //清空棋谱中的棋子
        private void cleanAllStone()
        {
            int count = sh.Shapes.Count;
            for (int i = 0; i <= step.getNumberOfStep(); ++i)
                foreach (Shape j in sh.Shapes)
                    if (i.ToString() == j.Tag.ToString())
                    {
                        sh.Shapes.Remove(j);
                        break;
                    }
        }
        //选中棋谱中某一项的触发事件
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            String[] coordinate = new String[10];
            int selectStep = 0;
            coordinate = treeView1.SelectedNode.Text.Split(new char[] { ',', ':', ' ' });
            try     //点击到棋谱中某一项时
            {
                selectStep = int.Parse(coordinate[0]);
                selectNodeX = int.Parse(coordinate[coordinate.Count() - 2]);    //获取棋子坐标
                selectNodeY = int.Parse(coordinate[coordinate.Count() - 1]);
            }
            catch (Exception exception)
            {
                cleanAllStone();    //点击非具体棋谱中每一步时，清空整个棋盘
                selectNumber = -3;
                return;
            }
            for (selectNumber = 0; selectNumber < step.getNumberOfStep(); selectNumber++)
                if (selectNodeX == step.getStep()[selectNumber].getXCoordinate() && selectNodeY == step.getStep()[selectNumber].getYCoordinate()
                    && selectStep == selectNumber + 1) break;
            cleanAllStone();    //清空整个棋盘重画
            correntStep = selectNumber;
            for (int i = 0; i <= selectNumber; i++)
            {
                if (step.searchExistence(step.getStep()[i].getXCoordinate(), step.getStep()[i].getYCoordinate(), selectNumber))
                    //查询该棋子是否被吃掉  
                    drawStone(step.getStep()[i], i);
            }
            treeView1.SelectedNode = null;
        }

        private void 载入游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = OpenFile();   //获取文件名
            if (filename == null) return;
            fromFile(filename);   //获取文件内容
            cleanAllStone();    //清空整个棋盘重画
            deleteTreeNode();   //删除树节点
            board.resetExistence(); //清空existence信息
            step.resetStep();   //清空step信息
            step.resetExitence();   //清空step的existence信息
            fromFileFlag = true;
            int numberOfStep = step.getNumberOfStep();
            for (int i = 0; fileStep[i] != null; i++)
            {
                xCoordinate = fileStep[i].getXCoordinate();
                yCoordinate = fileStep[i].getYCoordinate();
                xiaqi();
            }
            deleteFileStep();
            fromFileFlag = false;
        }

        private void 保存游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();  //打开文件保存框
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
                    AllError error = new AllError("文件格式错误！");
                    error.Show();
                }
                else
                {
                    string file = saveDlg.FileName.ToString();
                    string content = step.toString();
                    if (File.Exists(file) == true)
                    {
                        AllError error = new AllError("文件已经存在！");
                        error.Show();
                    }
                    else
                    {
                        FileStream myFs = new FileStream(file, FileMode.Create);
                        StreamWriter mySw = new StreamWriter(myFs);
                        mySw.Write(content);
                        mySw.Close();
                        myFs.Close();
                        AllSuccess success = new AllSuccess("文件保存成功！");
                        success.Show();
                    }
                }
            }
        }

        private void 退出游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 返回主菜单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 音量选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Volume form6 = new Volume();
            form6.Show();
        }

        private void 棋子声音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChessSoundFlag = !ChessSoundFlag;
        }

        private void 背景声音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (music.flag)
            {
                music.StopMusic();
                music.flag = !music.flag;
            }
            else music.StartMusic();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void 显示我们的GitHub网址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/CheesGame/chessGame");
        }

        private void 游戏名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs aboutus = new AboutUs();
            aboutus.Show();
            aboutus.FormClosing += Form_Closing;
        }
        private void 显示我们的邮箱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailSend mailsend = new MailSend();
            mailsend.Show();
        }
        //点击后退一步
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (correntStep != 0)
            {
                correntStep--;
                cleanAllStone();    //清空整个棋盘重画
                for (int i = 0; i < correntStep; i++)
                {
                    if (step.searchExistence(step.getStep()[i].getXCoordinate(), step.getStep()[i].getYCoordinate(), correntStep))
                        //查询该棋子是否被吃掉  
                        drawStone(step.getStep()[i], i);
                }
                selectNumber = correntStep;
            }
            if (correntStep == 0) selectNumber = -3;
        }
        //点击重新开始
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            cleanAllStone();    //清空整个棋盘重画
            selectNumber = -3;
            correntStep = 0;
        }
        //点击前进一步
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (correntStep != step.getNumberOfStep())
            {
                correntStep++;
                cleanAllStone();    //清空整个棋盘重画
                for (int i = 0; i < correntStep; i++)
                {
                    if (step.searchExistence(step.getStep()[i].getXCoordinate(), step.getStep()[i].getYCoordinate(), correntStep - 1))
                        //查询该棋子是否被吃掉  
                        drawStone(step.getStep()[i], i);
                }
                selectNumber = correntStep;
            }
        }

        private void 版本号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs aboutus = new AboutUs();
            aboutus.Show();
            aboutus.FormClosing += Form_Closing;
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs aboutus = new AboutUs();
            aboutus.Show();
            aboutus.FormClosing += Form_Closing;
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void 显示编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //明天搜如何写文字，太丑了
            foreach (TreeNode node in qipu.Nodes)
            {
                string text = node.Text;
                string[] textSplit = text.Split(':', ' ', ',');
                int stoneNumber = int.Parse(textSplit[0]);
                int stoneX = int.Parse(textSplit[textSplit.Count() - 2]);
                int stoneY = int.Parse(textSplit[textSplit.Count() - 1]);
                Label label1 = new Label();
                label1.BackColor = Color.Transparent;
                label1.Text = textSplit[0];
                label1.Size = new Size(10,10);
                label1.Location = new Point(xLocation[stoneX]-15,yLocation[stoneY]-35);
                this.Controls.Add(label1);
                label1.Show();
            }
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
