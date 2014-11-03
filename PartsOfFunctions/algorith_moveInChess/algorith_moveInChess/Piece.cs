using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorith_moveInChess
{
    class Piece
    {
        //棋子所属的棋盘
        Board belongBoard;
        //-1表示为白棋，+1表示为黑棋，0表示没有棋
        private int color;
        //棋子所在的X坐标
        private int xCoordinate;
        //棋子所在的Y坐标
        private int yCoordinate;
        //棋子的气
        private int liberty;
        //棋子的组号
        private int groupNo;

        private bool update;

        public Piece()
        {
            belongBoard = null;
            color = 0;
            xCoordinate = 0;
            yCoordinate = 0;
            liberty = 0;
            groupNo = -1;
            update = false;
        }
        public Piece(Board belong)
        {
            this.belongBoard = belong;
            color = 0;
            xCoordinate = 0;
            yCoordinate = 0;
            liberty = 0;
            groupNo = -1;
            update = false;
        }
        public Piece(Board belong, int color, int xCoordinate, int yCoordinate, int liberty, int groupNo, bool update)
        {
            this.belongBoard = belong;
            this.color = color;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.liberty = liberty;
            this.groupNo = groupNo;
            this.update = update;
        }

        public void playChess(int x, int y, int color)
        {
            check_already(x, y);
            check_repeat();
            check_eated(x, y,color);
            
            Console.WriteLine("successful");
            outcome(x, y, color);
        }

        //判断该点是否有子
        public bool check_already(int x, int y)
        {
            return !this.belongBoard.existence[x, y];
        }

        //判断该点是否下了之后自己会被吃掉
        public bool check_eated(int x, int y,int color)
        {

            return true;
        }

        //判断该店是否下了之后自己能不能吃掉别人
        public bool check_couldeat()
        {
            return true;
        }

        //判断该点是否耍赖皮
        public void check_repeat()
        {

        }

        //下完该点后的后果
        public void outcome(int x, int y, int color)
        {
            belongBoard.board[x, y].setColor(color);
            belongBoard.board[x, y].setXCoordinate(x);
            belongBoard.board[x, y].setYCoordinate(y);
            belongBoard.existence[x, y] = true;
            int[] temp = new int[200];       //用来存储所有白子或者黑子
            int num_piece = 0;                //temp的大小——白子或者黑子的总数量
            int[] group = new int[200];      //用来存储每一个白子或者黑子所属的组号。。。最大也就不到200个组
            int size = Board.size;
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (color == belongBoard.board[i, j].color)
                        temp[num_piece++] = i * 100 + j;
                }
            }
            //将所有相连的同颜色棋子分在同一个组中
            for (int i = 0; i < num_piece; ++i)
                group[i] = 0;//表示未分组的情况
            int num_group = 0;//表示分组的后小组的数量,也不断更新为当前组号
            for (int i = 0; i < num_piece; ++i)
            {
                if (0 == group[i])
                {
                    group[i] = ++num_group;
                    merge(temp, group, temp[i], num_group, num_piece);
                }
            }
            for (int i = 0; i < num_group; ++i)
                eat(temp, group, i, num_piece);
        }

        //合并所有相连的棋子
        public void merge(int[] temp, int[] group, int x, int group_no, int num_piece)
        {
            for (int i = 0; i < num_piece; ++i)
            {
                if (group[i] != group_no)
                {
                    if (x + 1 == temp[i] || x - 1 == temp[i] || x + 100 == temp[i] || x - 100 == temp[i])
                    {
                        group[i] = group_no;
                        merge(temp, group, temp[i], group_no, num_piece);
                    }
                }
            }
        }

        //吃掉下棋后所有气为0的点
        public void eat(int[] temp, int[] group, int group_now, int num_piece)
        {
            int gas = 0;
            for (int i = 0; i < num_piece; ++i)
            {
                if (group[i] == group_now)
                {
                    if (temp[i] / 100 != Board.size && belongBoard.existence[temp[i] / 100 + 1, temp[i] % 100] == false)
                    {
                        ++gas;
                        return;
                    }
                    if (temp[i] / 100 != 1 && belongBoard.existence[temp[i] / 100 - 1, temp[i] % 100] == false)
                    {
                        ++gas;
                        return;
                    }
                    if (temp[i] % 100 != Board.size && belongBoard.existence[temp[i] / 100, temp[i] % 100 + 1] == false)
                    {
                        ++gas;
                        return;
                    }
                    if (temp[i] % 100 != 1 && belongBoard.existence[temp[i] / 100, temp[i] % 100 + 1] == false)
                    {
                        ++gas;
                        return;
                    }
                }
            }
            if(gas==0)
                for(int i=0;i<num_piece;++i)
                    if (group[i] == group_now)
                    {
                        eraseChess();
                        belongBoard.existence[temp[i]/100,temp[i]%100]=false;
                    }

        }
        //从界面抹去一个棋子点
        public void eraseChess()
        {

        }



        public void setColor(int color)
        {
            this.color = color;
        }
        public int getColor()
        {
            return this.color;
        }
        public void setXCoordinate(int xCoordinate)
        {
            this.xCoordinate = xCoordinate;
        }
        public int getXCoordinate()
        {
            return this.xCoordinate;
        }
        public void setYCoordinate(int yCoordinate)
        {
            this.yCoordinate = yCoordinate;
        }
        public int getYCoordinate()
        {
            return this.yCoordinate;
        }
        public void setLiberty(int liberty)
        {
            this.liberty = liberty;
        }
        public int getLiberty()
        {
            return this.liberty;
        }
        public void setUpdate(bool update)
        {
            this.update = update;
        }
        public bool getUpdate()
        {
            return this.update;
        }
    }
}
