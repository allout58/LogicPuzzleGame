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
        public int Width {
            get;
            set;
        }
        public int Height {
            get;
            set;
        }

        public int RandomSeed {
            get; set;
        }

        Tank[][] board;
        public GameBoard(int width, int height) {
            this.Width = width;
            this.Height = height;

            this.board = new Tank[height][];
        }

        public void GenerateBoard() {
            bool hasDirty = false;
            Random rng = new Random(this.RandomSeed);
            HashSet<Pipe> pipes = new HashSet<Pipe>();

            //Generate the layout of the tanks, including the source and drain tanks
            for (int i = 0; i < Height; i++) {
                board[i] = new Tank[Width + 2];
                board[i][0] = new SourceTank { X = i, Y = 0 };
                for (int j = 1; j < Width + 1; j++) {
                    //Generate the tanks
                    if (!hasDirty && rng.Next(Width) == 1) {
                        hasDirty = true;
                        board[i][j] = new DirtyTank { X = i, Y = j };
                    }
                    else {
                        board[i][j] = new Tank { X = i, Y = j };
                    }
                }
                board[i][Width + 1] = new Tank { X = i, Y = Width + 1 }; //End tank
            }

            //Generate the pipes from each tank
            for (int i = 0; i < Height; i++) {
                for (int j = 1; j < Width + 2; j++) {
                    //Generate the connections between pipes
                    int numPipes = rng.Next(1, (i == 0 || i == Height) ? 2 : 3);
                    pipes.Clear();
                    for (int n = 0; n < numPipes; n++) {
                        Pipe p = null;
                        do {
                            int row = rng.Next(Utilities.BoundedInt(i - 1, 0, Height), Utilities.BoundedInt(i + 1, 0, Height));
                            p = new Pipe(board[row][j - 1], board[i][j]);
                        } while (pipes.Contains(p));
                        pipes.Add(p);

                        p.exitTank.ConnectTo(p.entranceTank);
                    }
                }
            }

            //Ensure every tank (except sinks) has an output
            for (int i = 0; i < Width; i++) {
                for (int j = 0; j < Width + 1; j++) {
                    if (board[i][j].Outputs.Length == 0) {
                        int row = rng.Next(Utilities.BoundedInt(i - 1, 0, Height), Utilities.BoundedInt(i + 1, 0, Height));
                        board[row][j + 1].ConnectTo(board[i][j]);

                    }
                }
            }

            for (int i = 0; i < Height; i++) {
                board[i][Width + 1].Update();
            }
        }

        public Tank[] this[long index] {
            get { return board[index]; }
        }

        public void Print() {
            Console.WriteLine("Tank configuration:");
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width + 2; j++) {
                    Tank t = board[i][j];
                    if (t is SourceTank) {
                        Console.Write("S");
                    }
                    else {
                        Console.Write("T");
                    }
                    Console.Write("({0:D},{1:D}){2:S} ", i, j, t.IsDirty ? "D" : "C");
                }
                Console.WriteLine("");
            }
        }
    }
}
