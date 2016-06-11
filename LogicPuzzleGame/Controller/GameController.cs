using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicPuzzleGame.Model;

namespace LogicPuzzleGame.Controller
{
    public class GameController
    {
        public const int TANK_COST = 90;
        public const int PIPE_COST = 10;

        private int curScore = 0;

        public event EventHandler Win;

        public int CurrentScore {
            get { return curScore; }
            private set {
                curScore = value;
                if (ScoreChanged != null) {
                    ScoreChanged(this, new EventArgs());
                }
            }
        }

        public event EventHandler ScoreChanged;

        public GameController() {
        }

        public void TankClick(Tank tank) {
            CurrentScore += TANK_COST;
            if (tank is DirtyTank) {
                Win?.Invoke(this, new EventArgs());
            }
        }

        public void PipeClick() {
            CurrentScore += PIPE_COST;
        }

        public void NewGame() {
            CurrentScore = 0;
        }
    }
}
