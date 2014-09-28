using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicGame
{
    public partial class Gobang
    {
        ChessPieces[] _Matrix;

        private void InitMatrix()
        {
            _Matrix = new ChessPieces[this._horizontal * this._vertical];

            for (int i = 0; i < _Matrix.Length; i++)
            {
                _Matrix[i] = new ChessPieces
                {
                    Color = Color.Black,
                    IsOk = false,
                    Step = 0
                };
            }
        }
    }
}
