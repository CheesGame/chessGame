using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorith_moveInChess
{
    public class Piece
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


        public Piece()
        {
            belongBoard = null;
            color = 0;
            xCoordinate = 0;
            yCoordinate = 0;
            groupNo = -1;

        }
        public Piece(int color,int xCoordinate, int yCoordinate ) {
            this.color = color;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }
        public Piece(Board belong)
        {
            this.belongBoard = belong;
            color = 0;
            xCoordinate = 0;
            yCoordinate = 0;
            groupNo = -1;

        }
        public Piece(Board belong, int color, int xCoordinate, int yCoordinate, int groupNo)
        {
            this.belongBoard = belong;
            this.color = color;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.groupNo = groupNo;
        }
        public void setBelong(Board belong)
        {
            this.belongBoard = belong;
        }
        //主程序入口
        public Piece[] playChess(int x, int y, int color)
        {
            if (check_already(x, y))
                return null;
            if (check_repeat()) //没写！！！！！！！！！！！
                return null;
            if (!(check_couldeat(x, y, color)) && (check_eated(x, y, color)))
            {
                belongBoard.existence[x][y] = false;//消除改点假设存在的痕迹
                return null;
            }
            Piece[] hehe;
            hehe = outcome(x, y, color);
            return hehe;
        }

        //判断该点是否有子
        public bool check_already(int x, int y)
        {
            return this.belongBoard.existence[x][y];
        }

        //判断该点是否下了之后自己会被吃掉
        public bool check_eated(int x, int y, int color)
        {
            int[] temp = new int[200];        //用来存储所有白子或者黑子
            int num_piece = 0;                //temp的大小——白子或者黑子的总数量
            int[] group = new int[200];       //用来存储每一个白子或者黑子所属的组号。。。最大也就不到200个组
            int size = Board.size;
            for (int i = 1; i <= size; ++i)
            {
                for (int j = 1; j <= size; ++j)
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
            for (int i = 1; i <= num_group; ++i)
            {
                int ans = eat(temp, group, i, num_piece);
                for (int j = 0; j < num_piece; ++j)
                {
                    if ((temp[j] / 100 == x) && (temp[j] % 100 == y))
                    {
                        if (group[j] == ans)
                            return true;
                    }
                }
            }
            return false;
        }

        //判断该店是否下了之后自己能不能吃掉别人
        public bool check_couldeat(int x, int y, int color)
        {
            //假装该点存在
            belongBoard.board[x, y].setColor(color);
            belongBoard.board[x, y].setXCoordinate(x);
            belongBoard.board[x, y].setYCoordinate(y);
            belongBoard.existence[x][y] = true;

            int[] temp = new int[200];        //用来存储所有白子或者黑子
            int num_piece = 0;                //temp的大小——白子或者黑子的总数量
            int[] group = new int[200];       //用来存储每一个白子或者黑子所属的组号。。。最大也就不到200个组
            int size = Board.size;
            for (int i = 1; i <= size; ++i)
            {
                for (int j = 1; j <= size; ++j)
                {
                    if (color == -belongBoard.board[i, j].color)
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
            for (int i = 1; i <= num_group; ++i)
            {
                int ans = eat(temp, group, i, num_piece);
                for (int j = 0; j < num_piece; ++j)
                {
                    if ((temp[j] / 100 == x + 1) && (temp[j] % 100 == y))
                    {
                        if (group[j] == ans)
                            return true;
                    }
                    if ((temp[j] / 100 == x - 1) && (temp[j] % 100 == y))
                    {
                        if (group[j] == ans)
                            return true;
                    }
                    if ((temp[j] / 100 == x) && (temp[j] % 100 == y + 1))
                    {
                        if (group[j] == ans)
                            return true;
                    }
                    if ((temp[j] / 100 == x) && (temp[j] % 100 == y - 1))
                    {
                        if (group[j] == ans)
                            return true;
                    }
                }
            }
            return false;

        }

        //判断该点是否耍赖皮
        public bool check_repeat()
        {
            return false;
        }

        //确定改点可以下棋且下完该点后的后果
        public Piece[] outcome(int x, int y, int color)
        {
            int hehe_id = 0;               //hehe的下标
            Piece[] hehe = new Piece[400];//返回的消失数组
            belongBoard.board[x, y].setColor(color);
            belongBoard.board[x, y].setXCoordinate(x);
            belongBoard.board[x, y].setYCoordinate(y);
            belongBoard.existence[x][y] = true;
            int[] temp = new int[200];       //用来存储所有白子或者黑子
            int num_piece = 0;                //temp的大小——白子或者黑子的总数量
            int[] group = new int[200];      //用来存储每一个白子或者黑子所属的组号。。。最大也就不到200个组
            int size = Board.size;
            for (int i = 1; i <= size; ++i)
            {
                for (int j = 1; j <= size; ++j)
                {
                    if (color == -belongBoard.board[i, j].color)
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
            for (int i = 1; i <= num_group; ++i)
            {
                int ans = eat(temp, group, i, num_piece);
                if (ans != -1)
                {
                    for (int j = 0; j < num_piece; ++j)
                        if (group[j] == ans)
                        {
                            hehe[hehe_id++] = new Piece(belongBoard, belongBoard.board[temp[j] / 100, temp[j] % 100].color, temp[j] / 100, temp[j] % 100, ans);
                            belongBoard.existence[temp[j] / 100][temp[j] % 100] = false;
                        }
                }
            }
            return hehe;
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
        public int eat(int[] temp, int[] group, int group_now, int num_piece)
        {
            int gas = 0;
            for (int i = 0; i < num_piece; ++i)
            {
                if (group[i] == group_now)
                {
                    if (temp[i] / 100 != Board.size && belongBoard.existence[temp[i] / 100 + 1][temp[i] % 100] == false)
                    {
                        ++gas;
                        return -1;
                    }
                    if (temp[i] / 100 != 1 && belongBoard.existence[temp[i] / 100 - 1][ temp[i] % 100] == false)
                    {
                        ++gas;
                        return -1;
                    }
                    if (temp[i] % 100 != Board.size && belongBoard.existence[temp[i] / 100][temp[i] % 100 + 1] == false)
                    {
                        ++gas;
                        return -1;
                    }
                    if (temp[i] % 100 != 1 && belongBoard.existence[temp[i] / 100][temp[i] % 100 - 1] == false)
                    {
                        ++gas;
                        return -1;
                    }
                }
            }
            if (gas == 0)
                return group_now;
            return -1;
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
    }
}
