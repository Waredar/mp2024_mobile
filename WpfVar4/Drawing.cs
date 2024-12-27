using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfVar4
{
    internal class Drawing
    {
        private Point StartPoint;
        private bool IsDragging = false;
        private UIElement? SelectedElement { get; set; }
        public Canvas OurCanvas { get; private set; }
        private ResizeBorder ResizeBorder { get; set; } = new ResizeBorder();

        private readonly Dictionary<Shape, Dictionary<string, object>> ShapesProperties;

        public Drawing(Canvas rootCanvas) 
        {
            OurCanvas = rootCanvas;
            OurCanvas.MouseDown += OurCanvas_MouseDown;
            OurCanvas.MouseMove += OurCanvas_MouseMove;
            OurCanvas.MouseUp += OurCanvas_MouseUp;
            ShapesProperties = [];
        }

        public void CreateRectangle()
        {
            double width = 100;
            double height = 50;
            double x = 10;
            double y = 20;
            Color fillColor = Colors.Blue;
            double strokeWidth = 2;
            Color strokeColor = Colors.Black;

            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(strokeColor),
                StrokeThickness = strokeWidth
            };

            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);

            OurCanvas.Children.Add(rectangle);
        }

        public void CreateEllipse()
        {
            double width = 80;
            double height = 60;
            double x = 50;
            double y = 100;
            Color fillColor = Colors.Red;
            double strokeWidth = 3;
            Color strokeColor = Colors.Green;

            Ellipse ellipse = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(strokeColor),
                StrokeThickness = strokeWidth,
            };

            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);

            OurCanvas.Children.Add(ellipse);
        }


        public void EditShape(Shape shape, Dictionary<string, object> properties)
        {
            if (!ShapesProperties.ContainsKey(shape))
            {
                throw new ArgumentException("Shape not found.");
            }

            foreach (var property in properties)
            {
                switch (property.Key)
                {
                    case "Width":
                        if (property.Value is double width)
                        {
                            shape.Width = width;
                            ShapesProperties[shape]["Width"] = width;
                        }
                        break;
                    case "Height":
                        if (property.Value is double height)
                        {
                            shape.Height = height;
                            ShapesProperties[shape]["Height"] = height;
                        }
                        break;
                    case "X":
                        if (property.Value is double x)
                        {
                            Canvas.SetLeft(shape, x);
                            ShapesProperties[shape]["X"] = x;
                        }
                        break;
                    case "Y":
                        if (property.Value is double y)
                        {
                            Canvas.SetTop(shape, y);
                            ShapesProperties[shape]["Y"] = y;
                        }
                        break;
                    case "FillColor":
                        if (property.Value is Color fillColor)
                        {
                            shape.Fill = new SolidColorBrush(fillColor);
                            ShapesProperties[shape]["FillColor"] = fillColor;
                        }
                        break;
                    case "StrokeWidth":
                        if (property.Value is double strokeWidth)
                        {
                            shape.StrokeThickness = strokeWidth;
                            ShapesProperties[shape]["StrokeWidth"] = strokeWidth;
                        }
                        break;
                    case "StrokeColor":
                        if (property.Value is Color strokeColor)
                        {
                            shape.Stroke = new SolidColorBrush(strokeColor);
                            ShapesProperties[shape]["StrokeColor"] = strokeColor;
                        }
                        break;
                    default:
                        throw new ArgumentException($"Unsupported property: {property.Key}");
                }
            }
        }

        public Dictionary<string, object> GetShapeProperties(Shape shape)
        {
            if (ShapesProperties.ContainsKey(shape))
            {
                return new Dictionary<string, object>(ShapesProperties[shape]);
            }
            throw new ArgumentException("Shape not found.");
        }

        private void OurCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                StartPoint = e.GetPosition(OurCanvas);

                ResizeBorder.RemoveResizeRectangle(OurCanvas);
                SelectedElement = null;

                foreach (UIElement element in OurCanvas.Children)
                {
                    if (element.IsMouseOver)
                    {
                        SelectedElement = element;
                        break;
                    }
                }

                if (SelectedElement != null)
                {
                    ResizeBorder.ShowResizeRectangle(OurCanvas, SelectedElement);
                    IsDragging = true;
                }

            }
        }

        private void OurCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging && SelectedElement != null)
            {
                Point currentPoint = e.GetPosition(OurCanvas);

                double offsetX = currentPoint.X - StartPoint.X;
                double offsetY = currentPoint.Y - StartPoint.Y;

                Canvas.SetLeft(SelectedElement, Canvas.GetLeft(SelectedElement) + offsetX);
                Canvas.SetTop(SelectedElement, Canvas.GetTop(SelectedElement) + offsetY);

                if (ResizeBorder.resizeRectangle != null)
                {
                    Canvas.SetLeft(ResizeBorder.resizeRectangle, Canvas.GetLeft(SelectedElement));
                    Canvas.SetTop(ResizeBorder.resizeRectangle, Canvas.GetTop(SelectedElement));
                }

                StartPoint = currentPoint;
            }

        }

        private void OurCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsDragging = false;
        }
    }
}
