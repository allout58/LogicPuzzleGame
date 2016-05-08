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
        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }

        Tank[][] board;
        public GameBoard(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.board = new Tank[height][];
        }

        public void GenerateBoard()
        {
            bool hasDirty = false;
            Random rng = new Random();
            HashSet<Pipe> pipes = new HashSet<Pipe>();

            for (int i = 0; i < Height; i++) {
                board[i] = new Tank[Width + 2];
                board[i][0] = new SourceTank();
                for (int j = 1; j < Width + 1; j++) {
                    //Generate the tanks
                    if (!hasDirty && rng.Next(Width) == 1) {
                        hasDirty = true;
                        board[i][j] = new DirtyTank();
                    }
                    else {
                        board[i][j] = new Tank();
                    }
                }
                board[i][Width + 1] = new Tank(); //End tank
            }
            for (int i = 0; i < Height; i++) {
                for (int j = 1; j < Width + 1; j++) {
                    //Generate the connections between pipes
                    int numPipes = rng.Next(1, 3);
                    pipes.Clear();
                    for (int n = 0; n < numPipes; n++) {
                        Pipe p = null;
                        do {
                            Tank randomInput = board[rng.Next(Utilities.BoundedInt(j - 1, 0, Height), Utilities.BoundedInt(j + 1, 0, Height))][j - 1];

                            p = new Pipe(randomInput, board[i][j]);
                        } while (pipes.Contains(p));
                        pipes.Add(p);

                        p.exitTank.ConnectTo(p.entranceTank);
                    }
                }
            }

            for(int i = 0; i < Height; i++) {
                board[i][Width + 1].Update();
            }
        }

        public Tank[] this[long index]
        {
            get { return board[index]; }
        }

        public void Print()
        {
            Console.WriteLine("Tank configuration:");
            for(int i = 0; i < Height; i++) {
                for(int j = 0; j < Width + 2; j++) {
                    Tank t = board[i][j];
                    if(t is SourceTank) {
                        Console.Write("S");
                    }
                    else {
                        Console.Write("T");
                    }
                    Console.Write("(%d,%d)%s", i, j, t.IsDirty?"D":"C");
                }
            }
        }
    }
}
