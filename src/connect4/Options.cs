using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace connect4
{
    public class Options
    {
        public int searchDepth = 6;
        public Color boardColor, backColor;
        public Options()
        {
            boardColor = Color.Lime;
            backColor = Color.Black;
        }
        public Options(Color boardColor, Color backColor)
        {
            this.boardColor = boardColor;
            this.backColor = backColor;
        }
    }
}
