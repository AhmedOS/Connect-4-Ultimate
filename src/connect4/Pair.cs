using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{
    public class Pair
    {
        public int first = 0, second = 0;
        public Pair(int first, int second)
        {
            this.first = first;
            this.second = second;
        }
        public void Set(int first, int second)
        {
            this.first = first;
            this.second = second;
        }
    }
}
