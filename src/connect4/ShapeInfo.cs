using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace connect4
{
    public class ShapeInfo
    {
        public int id, x, y, a, b;
        public Pen pen;
        public SolidBrush sb;
        public ShapeInfo(int id, Pen pen, int x, int y, int a, int b)
        {
            this.id = id;
            this.pen = pen;
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
        }
        public ShapeInfo(SolidBrush sb, int x, int y, int a, int b)
        {
            this.sb = sb;
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
        }
        public ShapeInfo(int x, int y, int a, int b)
        {
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
        }
    }
}
