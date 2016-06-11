using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using LogicPuzzleGame.Model;

namespace LogicPuzzleGame.View
{
    class TankControl : UserControl
    {
        public static readonly DependencyProperty AnchorPointLeftProperty = DependencyProperty.Register("AnchorPointLeft", typeof(Point), typeof(TankControl), new FrameworkPropertyMetadata(default(Point)));
        public Point AnchorPointLeft { get { return (Point) this.GetValue(AnchorPointLeftProperty); } set { this.SetValue(AnchorPointLeftProperty, value); } }

        public static readonly DependencyProperty AnchorPointRightProperty = DependencyProperty.Register("AnchorPointRight", typeof(Point), typeof(TankControl), new FrameworkPropertyMetadata(default(Point)));
        public Point AnchorPointRight { get { return (Point) this.GetValue(AnchorPointRightProperty); } set { this.SetValue(AnchorPointRightProperty, value); } }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double), typeof(TankControl), new FrameworkPropertyMetadata(default(double)));
        public double StrokeThickness { get { return (double) this.GetValue(StrokeThicknessProperty); } set { this.SetValue(StrokeThicknessProperty, value); } }

        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register("Brush", typeof(Brush), typeof(TankControl), new FrameworkPropertyMetadata(default(Brush)));
        public Brush Brush { get { return (Brush) this.GetValue(BrushProperty); } set { this.SetValue(BrushProperty, value); } }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(TankControl), new FrameworkPropertyMetadata(default(Brush)));
        public Brush Fill { get { return (Brush) this.GetValue(FillProperty); } set { this.SetValue(FillProperty, value); } }

        private Brush oldFill;

        private int MarkedStatus = 0;

        private bool isVis = false;
        public bool IsVisible {
            get { return isVis; }
            set {
                isVis = value;
                this.Fill = isVis ? (this.Tank.IsDirty ? Brushes.SaddleBrown : Brushes.RoyalBlue) : oldFill;
            }
        }

        public Tank Tank {
            get; set;
        }

        public event RoutedEventHandler Click;

        public TankControl() {
            InitializeContent();

            LayoutUpdated += OnLayoutUpdated;
            MouseDown += TankControlOld_MouseDown;
            Click += OnClick;
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
            this.MinHeight = 30;
            this.MinWidth = 30;
        }

        private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs) {
            if (this.MarkedStatus == 0)
                this.StrokeThickness -= 5;
        }

        private void OnMouseEnter(object sender, MouseEventArgs mouseEventArgs) {
            if (this.MarkedStatus == 0)
                this.StrokeThickness += 5;
        }

        private void TankControlOld_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed && MarkedStatus > 0 && IsEnabled) {
                Click?.Invoke(this, new RoutedEventArgs(e.RoutedEvent));
            }
        }

        private void InitializeContent() {
            Path path = new Path() {
                Data =
                    Geometry.Parse(
                        " M 64 10 A 32 10 0 0 0 32 0 32 10 0 0 0 0.0 10 M 64 10 A 32 10 0 0 1 32 20 32 10 0 0 1 0.0 10 M 64 10 Z M 64 10 A 32 10 0 0 1 32 20 32 10 0 0 1 0.0 10 M 0.0 10  l 0 44 A 32 10 0 0 0 32 64 32 10 0 0 0 64 54 l 0 -44 m 0 10  z"
                        ),
                StrokeThickness = 5,
                Stroke = Brushes.Black,
                Fill = Brushes.Beige,
                MinHeight = 30,
                MinWidth = 30,
                Stretch = Stretch.Uniform
            };

            Content = path;

            BindingBase strokeBinding = new Binding { Source = this, Path = new PropertyPath(StrokeThicknessProperty) };
            BindingBase brushBinding = new Binding { Source = this, Path = new PropertyPath(BrushProperty) };
            BindingBase fillBinding = new Binding {
                Source = this,
                Path = new PropertyPath(FillProperty)
            };
            BindingOperations.SetBinding(path, Path.StrokeThicknessProperty, strokeBinding);
            BindingOperations.SetBinding(path, Path.StrokeProperty, brushBinding);
            BindingOperations.SetBinding(path, Path.FillProperty, fillBinding);

        }

        private void OnLayoutUpdated(object sender, EventArgs eventArgs) {
            Path p = this.Content as Path;
            Visual v = this.Parent as Visual;
            if (v != null) {
                p.TransformToVisual(v);
            }
            Size size = p.RenderSize;
            Size parentSize = RenderSize;
            double center = parentSize.Width / 2;
            Point ofsL = new Point(center - size.Width / 2, size.Height / 2);
            Point ofsR = new Point(center + size.Width / 2, size.Height / 2);

            if (v != null) {
                AnchorPointLeft = TransformToVisual(v).Transform(ofsL);
                AnchorPointRight = TransformToVisual(v).Transform(ofsR);
            }
        }

        private void OnClick(object sender, EventArgs EventArgs) {
            this.IsEnabled = false;
            this.oldFill = this.Fill;
            this.Fill = this.Tank.IsDirty ? Brushes.SaddleBrown : Brushes.RoyalBlue;
        }

        public void MarkAsDirty() {

            this.MarkedStatus = (MarkedStatus + 1) % 3;
            switch (this.MarkedStatus) {
                case 0:
                    this.Fill = this.oldFill;
                    this.StrokeThickness -= 2;
                    break;
                case 1:
                    this.oldFill = this.Fill;
                    this.Fill = Brushes.DeepSkyBlue;
                    this.StrokeThickness += 2;
                    break;
                case 2:
                    this.Fill = Brushes.SandyBrown;
                    break;
            }
        }

    }
}
