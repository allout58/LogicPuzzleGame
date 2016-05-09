using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LogicPuzzleGame.Model;

namespace LogicPuzzleGame.View
{
    class TankControl : Button
    {
        public static readonly DependencyProperty AnchorPointLeftProperty = DependencyProperty.Register("AnchorPointLeft", typeof(Point), typeof(TankControl), new FrameworkPropertyMetadata(default(Point)));
        public Point AnchorPointLeft { get { return (Point) this.GetValue(AnchorPointLeftProperty); } set { this.SetValue(AnchorPointLeftProperty, value); } }

        public static readonly DependencyProperty AnchorPointRightProperty = DependencyProperty.Register("AnchorPointRight", typeof(Point), typeof(TankControl), new FrameworkPropertyMetadata(default(Point)));
        public Point AnchorPointRight { get { return (Point) this.GetValue(AnchorPointRightProperty); } set { this.SetValue(AnchorPointRightProperty, value); } }

        public Tank Tank {
            get; set;
        }

        public TankControl() {
            LayoutUpdated += OnLayoutUpdated;
        }

        private void OnLayoutUpdated(object sender, EventArgs eventArgs) {
            Size size = RenderSize;
            Point ofsL = new Point(0, size.Height / 2);
            Point ofsR = new Point(size.Width, size.Height / 2);
            Visual v = this.Parent as Visual;
            AnchorPointLeft = TransformToVisual(v).Transform(ofsL);
            AnchorPointRight = TransformToVisual(v).Transform(ofsR);
        }
    }
}
