using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Resources;

namespace connect4
{
    public class Expression
    {
        public enum Type
        {
            lose_biglose, lose_close, lose_easy, lose_unexpected, lose_wastrolling,
            think_bigwin, think_closegame2, think_closegame, think_closetolose2,
            think_closetolose, think_closetowin, think_earlytojudge2, think_earlytojudge,
            think_veryclosetolose,
            tie, tie_closetolose, tie_closetowin,
            win, win_, win_easy, win_trolled, win_trolled2
        }
        
        public static Image GetImage(Type type)
        {
            return (Image)Properties.Resources.ResourceManager.GetObject(type.ToString());
        }
    }
}
