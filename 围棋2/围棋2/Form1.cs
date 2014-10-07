using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace 围棋2//解决方案 使用数组解决问题 ：将相邻的子放进一个数组 然后判断这些子的气 如果所有子的气为0 则遍历吃掉这些子
{
    public partial class Form1 : Form
    {
        int[,] ai = new int[19, 19];//存数据的棋盘
        int[] bi = new int[100];//存黑棋和白棋
        int[] cc = new int[100];//分组存的
        static int bo = 0;//设置谁下
        ShapeContainer sh = new ShapeContainer();//画棋盘的容器
        const int f = 30, f2 = 40;//设置常量
        public Form1()
        {
            Text = "Demo= #!";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.Gray;
            Size = new Size(800, 850);
            Controls.Add(sh);
            KeyPreview = true;
            MouseDown += new MouseEventHandler(md);//布局
            for (int x = 1, y = 1, x2 = 19; y < 20; y++)//这个循环用来画棋盘qw4
            {
                LineShape a = new LineShape(x * f2, y * f2, x2 * f2, y * f2);
                LineShape b = new LineShape(y * f2, x * f2, y * f2, x2 * f2);
                a.Tag = b.Tag = 10000;
                a.Enabled = b.Enabled = false;//这一步很重要
                sh.Shapes.AddRange(new Shape[] { a, b });
            }
        }
        private void md(object sender, MouseEventArgs e)//鼠标点击事件
        {
            int d = dingwei(e.Location);
            if (d != 0)
            {
                bo++;
                OvalShape a = new OvalShape();
                a.Size = new Size(f, f);
                a.Location = new Point(d / 100 * f2 - f / 2, d % 100 * f2 - f / 2);
                a.BackStyle = BackStyle.Opaque;
                a.Tag = d-101;
                if (bo % 2 == 0)
                {
                    a.BackColor = Color.White;
                    panduan(1);
                }
                else
                {
                    a.BackColor = Color.Black;
                }
                sh.Shapes.Add(a);
                panduan(2);
                panduan(1);
            }
        }
        int dingwei(Point p)//落子的定位判断
        {
            for (int a = 1; a < 20; a++)
                for (int b = 1; b < 20; b++)
                    if (Math.Abs(p.X - a * f2) < 15 && Math.Abs(p.Y - b * f2) < 15)
                    {
                        ai[a - 1, b - 1] = bo % 2 + 1;
                        return a * 100 + b;//这里写得真是漂亮！琛老爷是由衷地佩服啊！
                    }
            return 0;
        }
        void panduan(int h)
        {
            int num=0;
            for (int i = 0; i < 19; i++)
                for (int j = 0; j < 19; j++)
                    if (ai[i, j] == h)
                        bi[num++] = i * 100 + j;//麻烦了点 还是弄到了所有黑子的坐标
            for (int i = 0; i < 100; i++)
                cc[i] = 0;
            int t = 1;
            for (int i = 0; i < num; i++)
                if (cc[i] == 0)
                {
                    cc[i] = t++;
                    xunzhao(bi[i], t - 1,num);//将其分组
                }
            for (int i = 1; i < t; i++)
                zhaoqi(i, num);         
        }
        void xunzhao(int x, int group_no, int num)//寻找 - -!强大的调用自己 第一次用
        {
            for (int i = 0; i < num; i++)
                if (cc[i] != group_no)
                    if (x + 1 == bi[i] || x - 1 == bi[i] || x + 100 == bi[i] || x - 100 == bi[i])
                    {
                        cc[i] = group_no;
                        xunzhao(bi[i], group_no, num);
                    }
        }
        void zhaoqi(int x,int z)//找一个区域的子的气 如果这个区域没有气 则扼杀这个区域的所有子 x代表区域的编号
        {
            int t=0;
            for(int i=0;i<z;i++)
                if (cc[i] == x)
                {
                    if (bi[i]/100!=19&&ai[bi[i] / 100+1, bi[i] % 100] == 0)
                        t++;
                    if (bi[i]/100!=0&&ai[bi[i] / 100 - 1, bi[i] % 100] == 0)
                        t++;
                    if (bi[i]%100!=19&&ai[bi[i] / 100 , bi[i] % 100+1] == 0)
                        t++;
                    if (bi[i]%100!=0&&ai[bi[i] /  100 , bi[i] % 100-1] == 0)
                        t++;
                }
            if(t==0)//杀掉这个区域的所有棋子
                for(int i=0;i<z;i++)
                    if (cc[i] == x)
                    {
                        chidiao(bi[i]);
                        ai[bi[i] / 100, bi[i] % 100] = 0;
                    }
        }
        void chidiao(int p)//测试无误
        {
            foreach (Shape i in sh.Shapes)
                if (p.ToString() == i.Tag.ToString())
                {
                    sh.Shapes.Remove(i);
                    break;//遍历中删除元素必须退出
                }
        }
    }
}