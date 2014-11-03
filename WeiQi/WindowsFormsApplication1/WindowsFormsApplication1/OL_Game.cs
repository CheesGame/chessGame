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
using Microsoft.DirectX.DirectSound;
using clientSocket;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class OL_Game : Form
    {
        public Board board = new Board();  //棋盘类
        public Step step = new Step(); //棋谱类，记录每一步

        private ShapeContainer sh = new ShapeContainer();//画棋盘的容器
        private int[] xLocation = { 0, 62, 87, 112, 138, 162, 188, 213, 238, 262, 288, 313, 336, 363, 388, 412, 437, 463, 487, 510 };
        private int[] yLocation = { 0, 128, 153, 179, 204, 228, 254, 277, 302, 328, 350, 378, 402, 427, 452, 477, 502, 527, 551, 577 };
        public int xCoordinate, yCoordinate;    //鼠标相对于窗口位置的x坐标和y坐标
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

        public bool receivedStone = false;
        public bool wait = true;   //true表示处于等待状态，false表示已经接收到数据
        private Client player;

        public bool alreadyreceived = false;
        public int quit = 0;  //表示弃步，1表示本机弃步，2表示对方也弃步，游戏结束
        public bool takeOver = false;
        public bool takeStart = false;
        private int correntStep = 0;
        public string host;
        private string hostpassword;
        private string opponent;
        private bool sendnameFlag = false;
        private bool firstFlag = false;
        public string takeStoneString="";
        public OL_Game()
        {
            InitializeComponent();
            initFunction();
        }
        public OL_Game(string user,Client clientPlayer,bool first)
        {
            InitializeComponent();
            this.host = user;
            this.player = clientPlayer;
            this.firstFlag = first;
            initFunction();
        }
        public void initFunction()
        {
            pictureBox3.Visible = false;
            label3.Visible = false;
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

            if (firstFlag)
            {
                label1.Text = host;
                player.receive();
                player.setSendMessage(host);
                player.send();
                firstFlag = true;
            }
            else
            {
                label2.Text = host;
                player.setSendMessage(host);
                player.send();
                player.receive();
                firstFlag = false;
            }

            this.opponent = player.getReceiveMessage();
            if (label1.Text == "")
            {
                label1.Text = opponent;
                wait = true;
            }
            else
            {
                label2.Text = opponent;
                wait = false;
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            xCoordinate = MousePosition.X - this.Location.X;
            yCoordinate = MousePosition.Y - this.Location.Y;
            pbX = xLocation[getPbX(xCoordinate)] - 10;
            pbY = yLocation[getPbY(yCoordinate)] - 10;
            if (takeStart && xCoordinate > 45 && xCoordinate < 525 && yCoordinate > 98 && yCoordinate < 566)
            {
                pictureBox3.Visible = true;
            }
            else
                pictureBox3.Visible = false;
            pictureBox3.Location = new Point(xCoordinate-15, yCoordinate-40);
            if (!player.connectSever)
            {
                AllError error = new AllError("服务器连接失败！");
                error.Show();
            }
           
            //表示游戏结束
            if (quit == 2)
            {
                wait = false;
                if (firstFlag)
                {
                    //Take take = new Take();
                    //take.Show();
                    takeStart = true;   //先手先进行提子
                    pictureBox3.Visible = true;
                    label3.Visible = true;
                    takeColor = 1;
                }
                else {
                    player.setSendMessage("quit==2");
                    player.send();
                    //先接受对方提掉的子
                    player.receive();
                    TakeOpponentStone();
                    //Take take = new Take();
                    //take.Show();
                    takeStart = true;   //白棋先进行提子
                    pictureBox3.Visible = true;
                    label3.Visible = true;
                    takeColor = -1;
                    //将自己提掉的子发给对方
                }
                quit = 0;
                return;
            }
            if (takeOver) { //提子结束，进行结算
                int num_black = 0;
                int num_white = 0;
                for (int i = 0; i < 19; ++i)
                    for (int j = 0; j < 19; ++j)
                    {
                        if (board.existence[i][j])
                        {
                            if (board.board[i, j].getColor() == 1)
                                ++num_black;
                            if (board.board[i, j].getColor() == -1)
                                ++num_white;
                        }
                    }
                GameOver(num_black, num_white);
                takeOver = false;
            }
            if (wait && !alreadyreceived)
            {
                receive();
            }
        }
        public int takeColor = 1;
        //提子结束
        private void label3_Click(object sender, EventArgs e)
        {
            //将提掉的子发给对方
            player.setSendMessage(takeStoneString);
            player.send();
            if (firstFlag)
            {
                do
                {
                    player.receive();
                } while (player.getReceiveMessage()[0] != ';');
                TakeOpponentStone();    //提对方的子
            }
            takeOver = true;    //提子全部结束，进行结果的展示
        }
        //提对方的子，根据获得的字符串进行解析获取坐标
        public void TakeOpponentStone()
        {
            String takeStoneString1 = player.getReceiveMessage();
            string[] takeStoneSplit = takeStoneString1.Split(';');
            //解析string
            if (firstFlag) takeColor = -1;
            else takeColor = 1;
            for (int i = 1; i < takeStoneSplit.Count(); i++)
            {
                string[] coordinateSplit = takeStoneSplit[i].Split(',');
                int xcoordinateSplit = int.Parse(coordinateSplit[0]);
                int ycoordinateSplit = int.Parse(coordinateSplit[1]);
                GameTake(xcoordinateSplit, ycoordinateSplit);
            }
        }
        //输入x，y坐标，提子
        public void GameTake(int pbx, int pby)
        {
            if (board.board[pbx, pby].getColor() == takeColor)
            {
                //把该子移除掉
                for (int j = 0; j < step.getNumberOfStep(); j++)
                    if (pbx == step.getStep()[j].getXCoordinate() && pby == step.getStep()[j].getYCoordinate() && board.existence[pbx][pby] == true)
                    {
                        deleteStone(j);
                        board.existence[pbx][pby] = false;
                        takeStoneString = takeStoneString + ";" + pbx + "," + pby;
                    }
            }
        }
        //OL_Game加载时的触发事件
        private void OL_Game_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
        }
        private void OL_Game_Click(object sender, EventArgs e)
        {
            if (wait) return;
            if (xCoordinate > 45 && xCoordinate < 525 && yCoordinate > 98 && yCoordinate < 566)
            {
                xiaqi();
            }
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
        //重新画棋盘
        private void redrawBoard()
        {
            int number = step.getNumberOfStep() - 1;
            cleanAllStone();    //清空整个棋盘重画
            for (int i = 0; i <= number; i++)
            {
                if (step.getStep()[i].getXCoordinate() == 0 && step.getStep()[i].getYCoordinate() == 0)
                    continue;
                if (step.searchExistence(step.getStep()[i].getXCoordinate(), step.getStep()[i].getYCoordinate(), number))
                    //查询该棋子是否被吃掉  
                    drawStone(step.getStep()[i], i);
            }
        }
        //点击下棋
        private void xiaqi()
        {
            if (quit == 1) quit = 0;
            redrawBoard();
            Piece piece = new Piece();  //新建一个棋子
            flag = (step.getNumberOfStep() % 2 == 0);
            if (flag) piece.setColor(1);  //设置棋子的颜色
            else piece.setColor(-1);
            //设置棋子的位置坐标
            if (receivedStone)
            {
                piece.setXCoordinate(xCoordinate);
                piece.setYCoordinate(yCoordinate);
            }
            else
            {
                piece.setXCoordinate(getPbX(xCoordinate));
                piece.setYCoordinate(getPbY(yCoordinate));
                wait = true;
                alreadyreceived = false;
            }
            Piece[] hehe = new Piece[400];
            piece.setBelong(board);
            hehe = piece.playChess(piece.getXCoordinate(), piece.getYCoordinate(), piece.getColor());
            if (hehe == null)
            { //在同一点重复下子或者下的子气为0，则错误提示
                wait = false;
                AllError error = new AllError("这里不能下棋！");
                error.ShowDialog();
            //    wait = true;
                return;
            }
            if (isSame())
            { //判断是否同形，同形则错误提示
                wait = false;
                AllError error = new AllError("这里不能下棋！");
                board.existence[getPbX(xCoordinate)][getPbY(yCoordinate)] = false;
                error.Show();
             //   wait = true;
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
            if (!receivedStone)
            {
                player.setSendMessage(piece.getXCoordinate() + "," + piece.getYCoordinate());
                player.send();
            }
            else
            {
                receivedStone = false;
            }
            correntStep = step.getNumberOfStep();
        }
        //删除树中的所有节点
        private void deleteTreeNode()
        {
            for (; qipu.Nodes.Count != 0; )
            {   //循环删除树桩图的节点
                qipu.Nodes.RemoveAt(qipu.Nodes.Count - 1);
            }
        }
        private void deleteTwoStep() {
            selectNumber = step.getNumberOfStep() - 3;
            int number1 = selectNumber + 1;
            int number = selectNumber + 1;  //从1开始数的
            selectNumber = -1;
            int stepnumber = step.getNumberOfStep();
            deleteStone(qipu.Nodes.Count-1);
            deleteStone(qipu.Nodes.Count-2);
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
            if (number1 == 0) {
                board.resetExistence();
            }else
                board.setExistence(step.existence[step.getNumberOfStep() - 1]);     //删除board的existence
            
        }
        //用于接收对方消息的函数
        public void receive()
        {
            player.receive();
            if (player.getReceiveMessage() == "huiqi") {
                //Form2 form = new Form2();
                //form.ShowDialog();
                wait = false;
                AskHuiQi huiqi = new AskHuiQi();
                huiqi.ShowDialog();
                if (huiqi.ok)
                {
                    player.setSendMessage("ok");
                    player.send();
                    deleteTwoStep();
                    wait = true;
                    return;
                }
                else {
                    player.setSendMessage("nook");
                    player.send();
                    wait = true;
                    return;
                }
            }
            
            if (player.getReceiveMessage() == "end")
            {   
                wait = false;
                AllError error = new AllError("对方已经离开！");
                error.Show();
                this.Close();
                return;
            }
            if (player.getReceiveMessage() == "quit==2") {
                quit = 2;
                return;
            }
            string[] coordinate = player.getReceiveMessage().Split(',');
            xCoordinate = int.Parse(coordinate[coordinate.Count() - 2]);    //获取棋子坐标
            yCoordinate = int.Parse(coordinate[coordinate.Count() - 1]);
            alreadyreceived = true;
            wait = false;
            if (xCoordinate == 0 && yCoordinate == 0)
            {
                quit++;
                Piece piece = new Piece();
                flag = (step.getNumberOfStep() % 2 == 0);
                if (flag) piece.setColor(1);  //设置棋子的颜色
                step.addStep(piece);
                step.addExistence(board.existence); //记录保存棋盘中所有棋子的位置
                step.stepIncrease();
                redrawBoard();
                return;
            }
            receivedStone = true;
            xiaqi();
        }
        //游戏结束界面显示
        private void GameOver(int num_black, int num_white)
        {
            Game_Over gameover = new Game_Over(num_black, num_white);
            gameover.Show();
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
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void 保存游戏ToolStripMenuItem_Click_1(object sender, EventArgs e)
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
        private void 退出游戏ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void 返回主菜单ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void qibu()
        {
            Piece piece = new Piece();
            flag = (step.getNumberOfStep() % 2 == 0);
            if (flag) piece.setColor(1);  //设置棋子的颜色
            step.addStep(piece);
            step.addExistence(board.existence); //记录保存棋盘中所有棋子的位置
            step.stepIncrease();
            wait = true;
            alreadyreceived = false;
            player.setSendMessage(0 + "," + 0);
            player.send();
            quit++;
        }
        
        private void 投降ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qibu();
        }
        private void 音量选择ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Volume form6 = new Volume();
            form6.Show();
        }
        private void 棋子声音ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ChessSoundFlag = !ChessSoundFlag;
        }
        private void 背景声音ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (music.flag)
            {
                music.StopMusic();
                music.flag = !music.flag;
            }
            else music.StartMusic();
        }

        private void 显示我们的GitHub网址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/CheesGame/chessGame");
        }

        private void 显示我们的邮箱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailSend mailsend = new MailSend();
            mailsend.Show();
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
        private void 游戏名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs aboutus = new AboutUs();
            aboutus.Show();
            aboutus.FormClosing += Form_Closing;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (firstFlag)
                qibu();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!firstFlag)
                qibu();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //如果处于提子结算阶段，则不下子
            GameTake(getPbX(xCoordinate), getPbY(yCoordinate));
        }

        private void 悔棋ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (step.getNumberOfStep() < 2) {
                AllError error = new AllError("您还没有下子呢！");
                error.Show();
                return;
            }
            player.setSendMessage("huiqi");
            player.send();
            player.receive();
            if (player.getReceiveMessage() == "ok")
            {
                //把棋谱删掉2个
                AllSuccess success = new AllSuccess("对方玩家允许你悔棋了~");
                success.Show();
                deleteTwoStep();
                return;
            }
            if (player.getReceiveMessage() == "nook")
            {
                AllError error = new AllError("对方玩家不允许你悔棋！");
                error.Show();
                return;
            }
        }
        public bool huiqiok = true;
        private void button1_Click(object sender, EventArgs e)
        {
            AskHuiQi huiqi = new AskHuiQi();
            huiqi.ShowDialog();
            if (huiqi.ok) {
                this.huiqiok = true;
            }
            else 
                this.huiqiok=false;
        }
    }
}
