using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace basicGame
{
    public partial class Gobang : Control
    {
        public Gobang()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true); 

            this.Resize += new EventHandler(GobangControl_Resize);
            this.MouseClick += new MouseEventHandler(Gobang_MouseClick);
            //this.MouseMove += new MouseEventHandler(Gobang_MouseMove);

            InitMatrix();
        }

        /// <summary>
        /// 半径的平方值
        /// </summary>
        private float _RadiusSquare = 0;
        /// <summary>
        /// 计步器
        /// </summary>
        private int _Step = 0;

        /// <summary>
        /// 纵线数
        /// </summary>
        private int _vertical = 19;
        [Description("纵线数")]
        public int Vertical
        {
            get { return _vertical; }
            set
            {
                _vertical = value;
                InitMatrix();
                Invalidate();
            }
        }

        /// <summary>
        /// 横线数
        /// </summary>
        private int _horizontal = 19;
        [Description("横线数")]
        public int Horizontal
        {
            get { return _horizontal; }
            set
            {
                _horizontal = value;
                InitMatrix();
                Invalidate();
            }
        }

        /// <summary>
        /// 纵横线颜色
        /// </summary>
        private Color _paintLine = Color.Black;
        [Description("纵横线颜色")]
        public Color PaintLine
        {
            get { return _paintLine; }
            set { _paintLine = value; }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            float vag_width = (float)(1.0 * this.Width / this._vertical);
            float vag_height = (float)(1.0 * this.Height / this._horizontal);

            this._RadiusSquare = (vag_width * vag_width + vag_height * vag_height) / 9;

            pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            using (Pen pen = new Pen(new SolidBrush(_paintLine), 2))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                for (int row = 1; row <= this._horizontal; row++)
                {
                    pe.Graphics.DrawLine(pen, (float)(vag_width * 0.5), (float)(vag_height * (row - 0.5)), (float)(vag_width * (this._vertical - 0.5)), (float)(vag_height * (row - 0.5)));
                    base.OnPaint(pe);
                }

                for (int col = 1; col <= this._vertical; col++)
                {
                    pe.Graphics.DrawLine(pen, (float)(vag_width * (col - 0.5)), (float)(vag_height * 0.5), (float)(vag_width * (col - 0.5)), (float)(vag_height * (this._horizontal - 0.5)));
                    base.OnPaint(pe);
                }
            }


            for (int row = 0; row < this._horizontal; row++)
            {
                for (int col = 0; col < this._vertical; col++)
                {
                    ChessPieces piec = _Matrix[row * this._vertical + col];

                    if (piec.IsOk)
                    {
                        using(SolidBrush  solidBrush = new SolidBrush(piec.Step % 2 == 1 ? Color.Black : Color.White))
                        {
                            float x = (float)((col + 0.2) * vag_width);
                            float y = (float)((row + 0.2) * vag_height);
                            float width = (float)(vag_width * 0.6);
                            float height = (float)(vag_height * 0.6);

                            pe.Graphics.FillEllipse(solidBrush, x, y, width, height);
                            base.OnPaint(pe);
                        }
                    }
                }
            }
        }

        ////鼠标指针在区域内移动时，指针顶端有一颗棋子
        //void Gobang_MouseMove(object sender, MouseEventArgs e)
        //{

        //}

        //点击鼠标、落子
        void Gobang_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    //点击左键，表示落子

                    float vag_width = (float)(1.0 * this.Width / this._vertical);
                    float vag_height = (float)(1.0 * this.Height / this._horizontal);

                    int nx = (int)Math.Round(e.X / vag_width - 0.5);
                    int ny = (int)Math.Round(e.Y / vag_height - 0.5);

                    float div_width = (float)((nx + 0.5) * vag_width) - e.X;
                    float div_height = (float)((ny + 0.5) * vag_height) - e.Y;

                    if (div_width * div_width + div_height * div_height <= this._RadiusSquare)
                    {
                        ChessPieces piec = this._Matrix[nx + ny * this._vertical];
                        if (!piec.IsOk)
                        {
                            piec.Step = ++_Step;
                            if (_Step % 2 == 1)
                                piec.Color = Color.Black;
                            else
                                piec.Color = Color.White;
                            piec.IsOk = true;

                            Invalidate();
                        }
                    }

                    break;

                case MouseButtons.Right:
                    //点击右键，表示撤销步骤

                    int maxpos = 0;

                    for (int i = 0; i < _Matrix.Length; i++)
                    {
                        if (_Matrix[i].IsOk && _Matrix[i].Step == _Step)
                        {
                            _Step--;
                            _Matrix[i].Step = 0;
                            _Matrix[i].IsOk = false;

                            Invalidate();

                            break;
                        }
                    }

                    break;
            }
        }

        void GobangControl_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }

    public class ChessPieces
    {
        public Color Color { set; get; }
        public bool IsOk { set; get; }
        public int Step { set; get; }
    }
}
