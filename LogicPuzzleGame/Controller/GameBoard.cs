using LogicPuzzleGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Controller
{
    public class GameBoard
    {
        int width, height;

        Tank[][] board;
        public GameBoard(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.board = new Tank[height][];
        }

        public void GenerateBoard()
        {
            for(int i = 0; i < height; i++) {
                board[i][0] = new SourceTank();
                for(int j = 0; j < width; j++) {

                }
                board[i][width + 1] = new Tank(); //End tank
            }
        }
    }
}
