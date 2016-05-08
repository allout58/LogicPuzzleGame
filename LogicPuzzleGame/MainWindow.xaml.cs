using LogicPuzzleGame.Controller;
using LogicPuzzleGame.Model;
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
        public GameBoard board
        {
            get;
            set;
        }

        public MainWindow()
        {
            board = new GameBoard(3, 3);
            board.GenerateBoard();
            InitializeComponent();
            
        }

        Button[][] btns = new Button[3][];

        private void RenderGameBoard()
        {
            GridLength width = new GridLength(100 / (board.Width + 2), GridUnitType.Star);
            GridLength height = new GridLength(100 / board.Height, GridUnitType.Star);
            for (int i = 0; i < board.Height; i++) {
                RowDefinition rd = new RowDefinition();
                rd.Height = height;
                MainGrid.RowDefinitions.Add(rd);

                btns[i] = new Button[5];
            }
            for (int j = 0; j < board.Width + 2; j++) {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = width;
                MainGrid.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < board.Height; i++) {
                for (int j = 0; j < board.Width + 2; j++) {
                    Button btn = new Button();
                    btns[i][j] = btn;
                    Tank t = board[i][j];
                    btn.Content = "Tank (" + i + ", " + j + ") " + (t.IsDirty ? "Dirty" : "Clean");
                    btn.Margin = new Thickness(10);
                    btn.SetValue(Grid.ColumnProperty, j);
                    btn.SetValue(Grid.RowProperty, i);
                    MainGrid.Children.Add(btn);
                }
            }

            for (int i = 0; i < board.Height; i++) {
                for (int j = 0; j < board.Width + 2; j++) {
                    Button btn = btns[i][j];
                    Tank t = board[i][j];
                    
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            board.Print();
            RenderGameBoard();
        }
    }
}
