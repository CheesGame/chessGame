﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorith_moveInChess
{
    class Piece
    {
        //-1表示为白棋，+1表示为黑棋，0表示没有棋
        private int color;
        //棋子所在的X坐标
        private int xCoordinate;
        //棋子所在的Y坐标
        private int yCoordinate;
        //棋子的气
        private int liberty;
        private bool update;

        public Piece()
        {
            this.color = 0;
            this.xCoordinate = 0;
            this.yCoordinate = 0;
            this.liberty = 0;
            this.update = false;
        }
        public Piece(int color, int xCoordinate, int yCoordinate, int liberty, bool update)
        {
            this.color = color;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.liberty = liberty;
            this.update = update;
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
