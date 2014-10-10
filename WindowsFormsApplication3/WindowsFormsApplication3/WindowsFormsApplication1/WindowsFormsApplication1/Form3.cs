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
        private Board board = new Board();  //棋盘类
        private TreeNode qipu = new TreeNode("棋谱"); //创建树桩棋谱展示
        private Step step = new Step(); //棋谱类，记录每一步
        private Boolean flag = true;    //true表示应该下黑棋，false表示应该下黑棋
        private Step step0 = new Step();   //用于存储从sgf文件中得到的棋谱
        private ShapeContainer sh = new ShapeContainer();//画棋盘的容器
        private ImageList imageList = new ImageList();  //树节点图片集
        private int[] xLocation = { 0, 60, 86, 112, 137, 163, 189, 214, 240, 266, 291, 317, 343, 367, 393, 419, 443, 470, 496, 520 };
        private int[] yLocation = { 0, 108, 133, 158, 182, 208, 233, 258, 282, 307, 332, 357, 382, 407, 431, 456, 481, 506, 532, 556 };
        private int xCoordinate, yCoordinate;    //鼠标相对于窗口位置的x坐标和y坐标
        private int pbX, pbY;   //棋子的显示位置
        public Form3()
        {
            InitializeComponent();
            Controls.Add(sh);   //画棋盘容器添加到form里面
            this.treeView1.Nodes.Add(qipu); //添加棋谱的树节点
            imageList.Images.Add(Image.FromFile("E://black.jpg"));
            imageList.Images.Add(Image.FromFile("E://white.jpg"));
            this.treeView1.ImageList = imageList;
        }
        //form3加载时的触发事件
        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;  //启动计时器
            timer1.Interval = 1;    //刷新周期为1毫秒
            if (xCoordinate > 50 && xCoordinate < 530 && yCoordinate > 98 && yCoordinate < 566)
                xiaqi();
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
            a.Location = new Point(xLocation[piece.getXCoordinate()] - 10, yLocation[piece.getYCoordinate()] - 10);
            a.BackStyle = BackStyle.Opaque;
            a.Tag = tag;
            if (piece.getColor() == -1) a.BackColor = Color.White;
            else a.BackColor = Color.Black;
            sh.Shapes.Add(a);   //在container中加入棋子
        }
        //删除一个棋子
        private void deleteStone(int i)
        {
            foreach (Shape j in sh.Shapes)
                if ((i + 1).ToString() == j.Tag.ToString())
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
        //点击下棋
        private void xiaqi()
        {
            if (board.existence[getPbX(xCoordinate), getPbY(yCoordinate)]) return;
            Piece piece = new Piece();  //新建一个棋子

            if (flag) piece.setColor(1);  //设置棋子的颜色
            else piece.setColor(-1);
            piece.setXCoordinate(getPbX(xCoordinate)); //设置棋子的位置坐标
            piece.setYCoordinate(getPbY(yCoordinate));

            step.addStep(piece);    //在棋谱中存入棋子
            drawStone(piece, step.getNumberOfStep());   //画棋子

            Piece[] hehe = new Piece[400];
            piece.setBelong(board);
            hehe = piece.playChess(piece.getXCoordinate(), piece.getYCoordinate(), piece.getColor());
            chiStone(hehe);
            //新建一个树节点显示棋谱
            TreeNode node = new TreeNode(piece.getXCoordinate() + "," + piece.getYCoordinate());
            if (piece.getColor() < 0) node.SelectedImageIndex = node.ImageIndex = 1;
            else node.SelectedImageIndex = node.ImageIndex = 0;
            qipu.Nodes.Add(node);

            flag = (!flag); //将flag的值取反
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
                    MessageBox.Show("Unexpected file format", "Super Go Format", MessageBoxButtons.OK);
                else
                    filename = openDlg.FileName;
            }
            return filename;
        }
        //点击打开棋谱操作
        private void button1_Click(object sender, EventArgs e)
        {
            string filename = OpenFile();   //获取文件名
            step0.fromFile(filename);   //获取文件内容
            this.treeView1.Nodes.Add(qipu);
            for (int i = 0; i < step0.getNumberOfStep(); i++)
            {
                int j = 0;
                if (step0.getStep()[i].getColor() < 0) j = 1;
                TreeNode stone = new TreeNode(step0.getStep()[i].getXCoordinate() + "," + step0.getStep()[i].getYCoordinate());
                stone.ImageIndex = stone.SelectedImageIndex = j;
                qipu.Nodes.Add(stone);
            }
        }
        //点击保存棋谱文件操作
        private void button2_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Unexpected file format", "Super Go Format", MessageBoxButtons.OK);
                else
                {
                    string file = saveDlg.FileName.ToString();
                    string content = step0.toString();
                    if (File.Exists(file) == true)
                        MessageBox.Show("存在此文件!");
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
        //清空棋谱中的棋子
        private void cleanAllStone()
        {
            int count = sh.Shapes.Count;
            for (int i = 0; i <= count; ++i)
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
            int xChi = 0, yChi = 0;
            String[] coordinate = new String[3];
            coordinate = treeView1.SelectedNode.Text.Split(',');
            try     //点击到棋谱中某一项时
            {
                xChi = int.Parse(coordinate[0]);    //获取棋子坐标
                yChi = int.Parse(coordinate[1]);
            }
            catch (Exception exception)
            {
                cleanAllStone();    //点击非具体棋谱中每一步时，清空整个棋盘
                return;
            }
            int number = 0; //用来记录棋子位于第几步
            for (number = 0; number < step.getNumberOfStep(); number++)
                if (xChi == step.getStep()[number].getXCoordinate() && yChi == step.getStep()[number].getYCoordinate()) break;
            cleanAllStone();    //清空整个棋盘重画
            for (int i = 0; i <= number; i++)
            { //重画
                drawStone(step.getStep()[i], i);
            }
        }
    }
}
