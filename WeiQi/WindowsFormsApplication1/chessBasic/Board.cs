using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorith_moveInChess
{
    public class Board
    {
        public Piece[,] board;
        public bool[][] existence;
        public static int size = 19;
        //默认棋盘大小为19的构造函数
        public Board()
        {
            //整个棋盘用(size+2)*(size+2)的数组表示
            //实际下棋位置在(size*size)大小的范围内
            board = new Piece[size + 2, size + 2];
            existence = new bool[size + 2][];
            for (int i = 0; i < size + 2; i++) {
                existence[i] = new bool[size + 2];
            }
                for (int i = 0; i < (size + 2); ++i)
                {
                    for (int j = 0; j < (size + 2); ++j)
                    {
                        board[i, j] = new Piece();
                        existence[i][j] = false;
                    }
                }
        }

        //制定棋盘大小的构造函数
        public Board(int size)
        {
            Board.size = size;
            //整个棋盘用(size+2)*(size+2)的数组表示
            //实际下棋位置在(size*size)大小的范围内
            board = new Piece[size + 2, size + 2];
            existence = new bool[size + 2][];
            for (int i = 0; i < (size + 2); ++i)
            {
                for (int j = 0; j < (size + 2); ++j)
                {
                    board[i, j] = new Piece(this);
                }
            }
            for (int i = 0; i < (size + 2); ++i)
            {
                for (int j = 0; j < (size + 2); ++j)
                {
                    existence[i][j] = false;
                }
            }
        }
        //board的existence全部设置为false
        public void resetExistence() {
            for (int i = 0; i < 21; i++) {
                for (int j = 0; j < 21; j++) {
                    this.existence[i][j] = false;
                }
            }
        }
        public void setExistence(bool[][] existence) {
            for (int i = 0; i < 21; i++) {
                for (int j = 0; j < 21; j++) {
                    this.existence[i][j] = existence[i][j];
                }
            }
        }

    }
}
