using LogicPuzzleGame.Controller;
using LogicPuzzleGame.Model;
using LogicPuzzleGame.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogicPuzzleGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameBoard Board { get; set; }
        public GameController Controller { get; private set; }

        public MainWindow() {
            Controller = new GameController();
            Controller.ScoreChanged += Controller_ScoreChanged;
            Board = new GameBoard(3, 3);
            Board.GenerateBoard();
            InitializeComponent();
        }

        private void Controller_ScoreChanged(object sender, EventArgs e) {
            TxtBlockScore.Text = String.Format("Score: {0}", Controller.CurrentScore);
        }

        private TankControl[][] btns = new TankControl[3][];

        private void RenderGameBoard() {
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();
            MainGrid.Children.Clear();

            btns = new TankControl[Board.Height][];

            GridLength width = new GridLength(100 / ((double) Board.Width + 2), GridUnitType.Star);
            GridLength height = new GridLength(100 / (double) Board.Height, GridUnitType.Star);
            for (int i = 0; i < Board.Height; i++) {
                RowDefinition rd = new RowDefinition();
                rd.Height = height;
                MainGrid.RowDefinitions.Add(rd);

                btns[i] = new TankControl[Board.Width + 2];
            }
            for (int j = 0; j < Board.Width + 2; j++) {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = width;
                MainGrid.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < Board.Height; i++) {
                for (int j = 0; j < Board.Width + 2; j++) {
                    TankControl btn = new TankControl();
                    btns[i][j] = btn;
                    Tank t = Board[i][j];
                    btn.Tank = t;
                    btn.Brush = Brushes.Black;
                    btn.Fill = Brushes.Beige;
                    btn.StrokeThickness = 1;
                    btn.Margin = new Thickness(20);
                    btn.SetValue(Grid.ColumnProperty, j);
                    btn.SetValue(Grid.RowProperty, i);
                    btn.Click += TankClicked;
                    if (j == 0 || j == Board.Width + 1) {
                        btn.IsVisible = true;
                        btn.IsEnabled = false;
                    }
                    MainGrid.Children.Add(btn);
                }
            }

            for (int i = 0; i < Board.Height; i++) {
                for (int j = 1; j < Board.Width + 2; j++) {
                    TankControl btn = btns[i][j];
                    foreach (Pipe pipe in btn.Tank.Inputs) {
                        for (int k = 0; k < Board.Height; k++) {
                            TankControl other = btns[k][j - 1];
                            if (other.Tank == pipe.entranceTank) {
                                PipeControl edge = new PipeControl();

                                edge.Pipe = pipe;

                                edge.SetBinding(PipeControl.SourceProperty, new Binding {
                                    Source = other,
                                    Path = new PropertyPath("AnchorPointRight")
                                });
                                edge.SetBinding(PipeControl.DestinationProperty, new Binding {
                                    Source = btn,
                                    Path = new PropertyPath("AnchorPointLeft")
                                });

                                edge.SetValue(Grid.ColumnSpanProperty, Board.Width + 2);
                                edge.SetValue(Grid.RowSpanProperty, Board.Height);

                                edge.Brush = Brushes.Black;
                                edge.StrokeThickness = 3;

                                edge.Click += EdgeOnClick;


                                MainGrid.Children.Add(edge);
                            }
                        }
                    }
                }
            }
        }

        private void EdgeOnClick(object sender, RoutedEventArgs routedEventArgs) {
            Controller.PipeClick();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Board.Print();
            RenderGameBoard();
        }

        private void TankClicked(object sender, RoutedEventArgs e) {
            TankControl tank = sender as TankControl;
            Controller.TankClick(tank.Tank);
        }

        private void btnNewTame_Click(object sender, RoutedEventArgs e) {
            this.Board = new GameBoard(5, 5);
            this.Board.RandomSeed = (Int32) (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            this.Board.GenerateBoard();
            RenderGameBoard();
        }
    }
}
