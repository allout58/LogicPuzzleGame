using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Controller
{
    public class GameController
    {
        public const int TANK_COST = 90;
        public const int PIPE_COST = 10;

        public int CurrentScore { get; private set; }

        public GameController() {
            CurrentScore = 0;
        }

        public void TankClick() {
            CurrentScore += TANK_COST;
        }

        public void PipeClick() {
            CurrentScore += PIPE_COST;
        }
    }
}
