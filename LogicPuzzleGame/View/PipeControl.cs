using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using LogicPuzzleGame.Model;

namespace LogicPuzzleGame.View
{
    public sealed class PipeControl : UserControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Point), typeof(PipeControl), new FrameworkPropertyMetadata(default(Point)));
        public Point Source { get { return (Point) this.GetValue(SourceProperty); } set { this.SetValue(SourceProperty, value); } }

        public static readonly DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(Point), typeof(PipeControl), new FrameworkPropertyMetadata(default(Point)));
        public Point Destination { get { return (Point) this.GetValue(DestinationProperty); } set { this.SetValue(DestinationProperty, value); } }

        public Pipe Pipe {
            get; set;
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double), typeof(PipeControl), new FrameworkPropertyMetadata(default(double)));
        public double StrokeThickness { get { return (double) this.GetValue(StrokeThicknessProperty); } set { this.SetValue(StrokeThicknessProperty, value); } }

        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register("Brush", typeof(Brush), typeof(PipeControl), new FrameworkPropertyMetadata(default(Brush)));
        public Brush Brush { get { return (Brush) this.GetValue(BrushProperty); } set { this.SetValue(BrushProperty, value); } }

        public PipeControl() {
            LineSegment segment = new LineSegment(default(Point), true);
            PathFigure figure = new PathFigure(default(Point), new[] { segment }, false);
            PathGeometry geometry = new PathGeometry(new[] { figure });
            BindingBase sourceBinding = new Binding { Source = this, Path = new PropertyPath(SourceProperty) };
            BindingBase destinationBinding = new Binding { Source = this, Path = new PropertyPath(DestinationProperty) };
            BindingOperations.SetBinding(figure, PathFigure.StartPointProperty, sourceBinding);
            BindingOperations.SetBinding(segment, LineSegment.PointProperty, destinationBinding);
            Path path = new Path {
                Data = geometry,
                StrokeThickness = 5,
                Stroke = Brushes.Black,
                MinWidth = 1,
                MinHeight = 1
            };

            Content = path;

            BindingBase strokeBinding = new Binding {Source = this, Path = new PropertyPath(StrokeThicknessProperty)};
            BindingBase brushBinding = new Binding {Source = this, Path = new PropertyPath(BrushProperty)};
            BindingOperations.SetBinding(path, Path.StrokeThicknessProperty, strokeBinding);
            BindingOperations.SetBinding(path, Path.StrokeProperty, brushBinding);
        }
    }
}
