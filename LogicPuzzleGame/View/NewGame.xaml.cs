using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogicPuzzleGame.View
{
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        static Regex NumberRegex = new Regex("[0-9]+");

        public int GameWidth { get; private set; }
        public int GameHeight { get; private set; }
        public int Seed { get; private set; }
        public NewGame() {
            InitializeComponent();
            textBoxSeed.Text = ((Int32) (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000).ToString(CultureInfo.CurrentCulture);
        }

        private void textBoxWidth_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            e.Handled = !IsNumeric(e.Text);
        }

        private void textBoxHeight_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            e.Handled = !IsNumeric(e.Text);
        }

        private static bool IsNumeric(string text) {
            return NumberRegex.IsMatch(text);
        }

        private void buttonNewGame_Click(object sender, RoutedEventArgs e) {
            this.GameHeight = Int32.Parse(textBoxHeight.Text);
            this.GameWidth = Int32.Parse(textBoxWidth.Text);
            if (IsNumeric(textBoxSeed.Text)) {
                this.Seed = Int32.Parse(textBoxSeed.Text);
            }
            else {
                this.Seed = textBoxSeed.Text.GetHashCode();
            }
            this.DialogResult = true;
        }
    }
}
