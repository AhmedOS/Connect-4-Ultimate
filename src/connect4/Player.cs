using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace connect4
{
    public class Player
    {
        public int id, score;
        public double time;
        public string name = "";
        public Color color;
        public AI brain = new AI();
        public ExternalAI externalBrain = new ExternalAI();
        public int type = 0; // 0-human, 1-built-in ai, 2-external ai
        public bool online = false;
        public string imageLocation = "";
        public Player(int id)
        {
            this.id = id;
        }
        public void Init(int id, string name)
        {
            this.id = id;
            this.name = name;
            score = 0;
        }
        public void NewGame()
        {
            time = 0;
            if (type == 1)
                brain.NewGame();
        }
    }
}
