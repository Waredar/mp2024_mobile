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
        public Shape? SelectedShape { get; private set; }
        public Canvas OurCanvas { get; private set; }
        private ResizeBorder ResizeBorder { get; set; }
        public readonly Dictionary<Shape, Dictionary<string, object>> ShapesProperties;
        public event Action<Shape, Dictionary<string, object>> ShapePropertiesChanged;
        public event Action<Shape?, Dictionary<string, object>?> SelectShape;

        public Drawing(Canvas rootCanvas) 
        {
            OurCanvas = rootCanvas;
            OurCanvas.MouseDown += OurCanvas_MouseDown;
            OurCanvas.MouseMove += OurCanvas_MouseMove;
            OurCanvas.MouseUp += OurCanvas_MouseUp;
            ShapesProperties = [];
            ResizeBorder = new(this);
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

            ShapesProperties[rectangle] = new Dictionary<string, object>
            {
                { "Type", "Rectangle" },
                { "Width", width },
                { "Height", height },
                { "X", x },
                { "Y", y },
                { "FillColor", fillColor },
                { "StrokeWidth", strokeWidth },
                { "StrokeColor", strokeColor }
            };
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

            ShapesProperties[ellipse] = new Dictionary<string, object>
            {
                { "Type", "Ellipse" },
                { "Width", width },
                { "Height", height },
                { "X", x },
                { "Y", y },
                { "FillColor", fillColor },
                { "StrokeWidth", strokeWidth },
                { "StrokeColor", strokeColor }
            };
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
                    case "Type":
                        continue;
                    case "Width":
                        if (property.Value is string width)
                        {
                            if (double.TryParse(width, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double number))
                            {
                                shape.Width = number;
                                ShapesProperties[shape]["Width"] = width;
                            }
                        }
                        break;
                    case "Height":
                        if (property.Value is string height)
                        {
                            if (double.TryParse(height, System.Globalization.NumberStyles.Float,
                                    System.Globalization.CultureInfo.InvariantCulture, out double number))
                            {
                                shape.Height = number;
                                ShapesProperties[shape]["Height"] = height;
                            }
                        }
                        break;
                    case "X":
                        if (property.Value is string x)
                        {
                            if (double.TryParse(x, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double number))
                            {
                                Canvas.SetLeft(shape, number);
                                ShapesProperties[shape]["X"] = x;
                            }
                        }
                        break;
                    case "Y":
                        if (property.Value is string y)
                        {
                            if (double.TryParse(y, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double number))
                            {
                                Canvas.SetTop(shape, number);
                                ShapesProperties[shape]["Y"] = y;
                            }
                        }
                        break;
                    case "FillColor":
                        if (property.Value is string fillColor)
                        {
                            try
                            {
                                Color color = (Color)ColorConverter.ConvertFromString(fillColor);
                                shape.Fill = new SolidColorBrush(color);
                                ShapesProperties[shape]["FillColor"] = fillColor;
                            }
                            catch {
                                continue;
                            }

                        }
                        break;
                    case "StrokeWidth":
                        if (property.Value is string strokeWidth)
                        {
                            if (double.TryParse(strokeWidth, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double number))
                            {
                                shape.StrokeThickness = number;
                                ShapesProperties[shape]["StrokeWidth"] = strokeWidth;
                            }
                        }
                        break;
                    case "StrokeColor":
                        if (property.Value is string strokeColor)
                        {
                            try
                            {
                                Color color = (Color)ColorConverter.ConvertFromString(strokeColor);
                                shape.Stroke = new SolidColorBrush(color);
                                ShapesProperties[shape]["StrokeColor"] = strokeColor;
                            }
                            catch { continue; }
                        }
                        break;
                    default:
                        throw new ArgumentException($"Unsupported property: {property.Key}");
                }
            }
        }

        private void OurCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                StartPoint = e.GetPosition(OurCanvas);

                var template = SelectedShape;
                SelectedShape = null;

                foreach (Shape shape in OurCanvas.Children)
                {
                    if (shape.IsMouseOver)
                    {
                        if (shape.Name == "Handle" || shape.Name == "ResizeBorder") { SelectedShape = template; IsDragging = false; continue; }
                        SelectedShape = shape;
                        ResizeBorder.ShowResizeRectangle(SelectedShape);
                        IsDragging = true;
                        break;
                    }
                }

                if (SelectedShape == null)
                {
                    IsDragging = false;
                    ResizeBorder.RemoveResizeRectangle();
                    SelectedShape = null;
                    SelectShape?.Invoke(null, null);
                    return;
                }
                SelectShape?.Invoke(SelectedShape, ShapesProperties[SelectedShape]);
            }
        }

        private void OurCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging && SelectedShape != null)
            {
                Point currentPoint = e.GetPosition(OurCanvas);

                double offsetX = currentPoint.X - StartPoint.X;
                double offsetY = currentPoint.Y - StartPoint.Y;

                Canvas.SetLeft(SelectedShape, Canvas.GetLeft(SelectedShape) + offsetX);
                Canvas.SetTop(SelectedShape, Canvas.GetTop(SelectedShape) + offsetY);

                if (ShapesProperties.ContainsKey(SelectedShape))
                {
                    ShapesProperties[SelectedShape]["X"] = Canvas.GetLeft(SelectedShape) + offsetX;
                    ShapesProperties[SelectedShape]["Y"] = Canvas.GetTop(SelectedShape) + offsetY;
                    ShapePropertiesChanged?.Invoke(SelectedShape, ShapesProperties[SelectedShape]);
                }

                ResizeBorder.UpdateSelectionRectangle(SelectedShape);
                ResizeBorder.UpdateResizeHandles(SelectedShape);

                StartPoint = currentPoint;
            }

        }

        public void RaiseShapePropertiesChanged(Shape shape, Dictionary<string, object> shapeProperties)
        {
            Console.WriteLine($"Событие вызвано для фигуры: {shape.Name}");
            ShapePropertiesChanged?.Invoke(shape, shapeProperties);
        }
        private void OurCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsDragging = false;
        }
    }
}
