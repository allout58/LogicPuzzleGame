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


        public Tank Tank {
            get; set;
        }

        public event RoutedEventHandler Click;

        public Boolean IsRevealed {
            get; private set;
        }

        public TankControl() {
            InitializeContent();

            LayoutUpdated += OnLayoutUpdated;
            MouseDown += TankControlOld_MouseDown;
            Click += OnClick;
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
            this.IsRevealed = false;
            this.MinHeight = 30;
            this.MinWidth = 30;
        }

        private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            this.Background = null;
        }

        private void OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            this.Background = Brushes.Orange;
        }

        private void TankControlOld_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (IsEnabled && Click != null)
            {
                Click(this, new RoutedEventArgs(e.RoutedEvent));
            }
        }

        private void InitializeContent() {
            Path path = new Path() {
                Data =
                    Geometry.Parse(
                        "M 31.863278 0.76313752 A 31.190374 9.308028 0 0 0 0.68770804 9.9815288 l -0.0156573 0 0 0.091657 0 0.03888 a 31.190374 9.308028 0 0 1 0.003131 -0.01666 31.190374 9.308028 0 0 0 31.18809626 9.285052 31.190374 9.308028 0 0 0 31.188095 -9.290606 31.190374 9.308028 0 0 1 0.0032 0.02222 l 0 -0.03888 0 -0.091657 -0.0094 0 A 31.190374 9.308028 0 0 0 31.863278 0.76313752 Z M 63.054504 10.11207 A 31.190374 9.308028 0 0 1 31.863278 19.419341 31.190374 9.308028 0 0 1 0.67205076 10.11207 l 0 43.889432 0 0.322187 0.0375768 0 A 31.190374 9.308028 0 0 0 31.863278 63.308773 31.190374 9.308028 0 0 0 63.001272 54.323689 l 0.05324 0 0 -0.322187 0 -43.889432 z"),
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
            if (v != null)
            {
                p.TransformToVisual(v);
            }
            Size size = p.RenderSize;
            Size parentSize = RenderSize;
            double center = parentSize.Width/2;
            Point ofsL = new Point(center - size.Width / 2, size.Height / 2);
            Point ofsR = new Point(center + size.Width / 2, size.Height / 2);
            
            if (v != null) {
                AnchorPointLeft = TransformToVisual(v).Transform(ofsL);
                AnchorPointRight = TransformToVisual(v).Transform(ofsR);
            }
        }

        private void OnClick(object sender, EventArgs EventArgs) {
            this.IsRevealed = true;
            this.IsEnabled = false;
        }

    }
}
