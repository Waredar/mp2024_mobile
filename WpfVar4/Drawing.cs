using System;
using System.IO;
using System.Text.Json;
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
        public readonly List<Shape> SelectedShapes;
        public Shape? SelectedShape;
        public Canvas OurCanvas { get; private set; }
        public ResizeBorder ResizeBorder { get; private set; }
        public readonly Dictionary<Shape, Dictionary<string, object>> ShapesProperties;
        public event Action<Shape, Dictionary<string, object>>? ShapePropertiesChanged;
        public event Action<Shape?, Dictionary<string, object>?>? SelectingShape;

        public Drawing(Canvas rootCanvas)
        {
            OurCanvas = rootCanvas;
            OurCanvas.ClipToBounds = true;
            OurCanvas.MouseDown += OurCanvas_MouseDown;
            OurCanvas.MouseMove += OurCanvas_MouseMove;
            OurCanvas.MouseUp += OurCanvas_MouseUp;

            SelectedShapes = [];
            ShapesProperties = new Dictionary<Shape, Dictionary<string, object>>();
            ResizeBorder = new(this);
        }

        public void RaiseShapePropertiesChanged(Shape shape, Dictionary<string, object> shapeProperties)
        {
            ShapePropertiesChanged?.Invoke(shape, shapeProperties);
        }

        public void CreateBasicRectangle()
        {
            double width = 100;
            double height = 50;
            double x = 10;
            double y = 20;
            Color fillColor = Colors.Blue;
            double strokeWidth = 2;
            Color strokeColor = Colors.Black;
            double angle = 0;
            int ZIndex = 0;

            CreateRectangle(width, height, x, y, fillColor, strokeWidth, strokeColor, angle, ZIndex);
        }

        public void CreateRectangle(double width, double height, double left, double top, Color fillColor, double strokeWidth, Color strokeColor, double angle, int ZIndex)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(strokeColor),
                StrokeThickness = strokeWidth
            };

            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);

            RotateTransform rotateTransform = new RotateTransform(angle, width / 2, height / 2);
            rectangle.RenderTransform = rotateTransform;

            OurCanvas.Children.Add(rectangle);

            Panel.SetZIndex(rectangle, ZIndex);

            ShapesProperties[rectangle] = new Dictionary<string, object>
            {
                { "Type", "Rectangle" },
                { "Width", width },
                { "Height", height },
                { "X", left },
                { "Y", top },
                { "FillColor", fillColor },
                { "StrokeWidth", strokeWidth },
                { "StrokeColor", strokeColor },
                { "RotationAngle", angle },
                { "ZIndex", ZIndex }
            };
        }

        public void CreateBasicTriangle()
        {
            double baseWidth = 100;
            double height = 80;
            double x = 50;
            double y = 50;
            Color fillColor = Colors.Yellow;
            double strokeWidth = 2;
            Color strokeColor = Colors.Black;
            double angle = 0;
            int zIndex = 0;

            CreateTriangle(baseWidth, height, x, y, fillColor, strokeWidth, strokeColor, angle, zIndex);
        }

        public void CreateTriangle(double baseWidth, double height, double left, double top, Color fillColor, double strokeWidth, Color strokeColor, double angle, int zIndex)
        {
            PointCollection points = new PointCollection
            {
                new Point(0, height),
                new Point(baseWidth, height),
                new Point(baseWidth / 2, 0)
            };

            Polygon triangle = new Polygon
            {
                Points = points,
                Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(strokeColor),
                StrokeThickness = strokeWidth
            };

            Canvas.SetLeft(triangle, left);
            Canvas.SetTop(triangle, top);

            RotateTransform rotateTransform = new RotateTransform(angle, baseWidth / 2, height / 2);
            triangle.RenderTransform = rotateTransform;

            OurCanvas.Children.Add(triangle);

            Panel.SetZIndex(triangle, zIndex);

            ShapesProperties[triangle] = new Dictionary<string, object>
            {
                { "Type", "Triangle" },
                { "Width", baseWidth },
                { "Height", height },
                { "X", left },
                { "Y", top },
                { "FillColor", fillColor },
                { "StrokeWidth", strokeWidth },
                { "StrokeColor", strokeColor },
                { "RotationAngle", angle },
                { "ZIndex", zIndex }
            };
        }

        public double GetTriangleWidth(Polygon triangle)
        {
            double minX = triangle.Points.Min(point => point.X);
            double maxX = triangle.Points.Max(point => point.X);
            return maxX - minX;
        }

        public double GetTriangleHeight(Polygon triangle)
        {
            double minY = triangle.Points.Min(point => point.Y);
            double maxY = triangle.Points.Max(point => point.Y);
            return maxY - minY;
        }

        public void SetTriangleDimensions(Polygon triangle, double newWidth, double newHeight)
        {
            if (ShapesProperties.ContainsKey(triangle) && ShapesProperties[triangle]["Type"].ToString() == "Triangle")
            {
                double currentWidth = GetTriangleWidth(triangle);
                double currentHeight = GetTriangleHeight(triangle);

                double scaleX = newWidth / currentWidth;
                double scaleY = newHeight / currentHeight;

                PointCollection newPoints = new PointCollection();
                foreach (Point point in triangle.Points)
                {
                    double newX = point.X * scaleX;
                    double newY = point.Y * scaleY;
                    newPoints.Add(new Point(newX, newY));
                }

                triangle.Points = newPoints;

                ShapesProperties[triangle]["Width"] = newWidth;
                ShapesProperties[triangle]["Height"] = newHeight;
            }
            else
            {
                throw new ArgumentException("Invalid triangle.");
            }
        }

        public void RemoveShape()
        {
            foreach (Shape shape in SelectedShapes.ToList())
            {
                if (ShapesProperties.ContainsKey(shape))
                {
                    ShapesProperties.Remove(shape);
                    OurCanvas.Children.Remove(shape);
                }
            }

            SelectedShapes.Clear();
            ResizeBorder.RemoveResizeRectangle();
            SelectingShape?.Invoke(null, null);
        }

        public void RemoveAllShapes()
        {
            ShapesProperties.Clear();
            OurCanvas.Children.Clear();
            SelectedShapes.Clear();
            ResizeBorder.RemoveResizeRectangle();
            SelectingShape?.Invoke(null, null);
        }

        public void EditShape(Shape shape, Dictionary<string, object> properties)
        {
            if (!ShapesProperties.ContainsKey(shape))
            {
                throw new ArgumentException("Shape not found.");
            }

            foreach (var property in properties)
            {
                try
                {
                    switch (property.Key)
                    {
                        case "Type":
                            continue;

                        case "Width":
                            if (property.Value is string newWidthStr && double.TryParse(newWidthStr, out double newWidth))
                            {
                                if (shape is Polygon polygon && ShapesProperties[shape]["Type"].ToString() == "Triangle")
                                {
                                    double currentHeight = GetTriangleHeight(polygon);
                                    SetTriangleDimensions(polygon, newWidth, currentHeight);
                                    ShapesProperties[shape]["Width"] = newWidth;
                                }
                                else
                                {
                                    shape.Width = newWidth;
                                    ShapesProperties[shape]["Width"] = newWidth;
                                }
                            }
                            break;

                        case "Height":
                            if (property.Value is string newHeightStr && double.TryParse(newHeightStr, out double newHeight))
                            {
                                if (shape is Polygon polygon && ShapesProperties[shape]["Type"].ToString() == "Triangle")
                                {
                                    double currentWidth = GetTriangleWidth(polygon);
                                    SetTriangleDimensions(polygon, currentWidth, newHeight);
                                    ShapesProperties[shape]["Height"] = newHeight;
                                }
                                else
                                {
                                    shape.Height = newHeight;
                                    ShapesProperties[shape]["Height"] = newHeight;
                                }
                            }
                            break;


                        case "X":
                        case "Y":
                            UpdateNumericProperty(shape, property, (value) =>
                            {
                                if (property.Key == "X") Canvas.SetLeft(shape, value);
                                else Canvas.SetTop(shape, value);

                                ShapesProperties[shape][property.Key] = value;
                            });
                            break;

                        case "FillColor":
                            UpdateColorProperty(shape, property, (color) =>
                            {
                                shape.Fill = new SolidColorBrush(color);
                                ShapesProperties[shape]["FillColor"] = color.ToString();
                            });
                            break;

                        case "StrokeWidth":
                            UpdateNumericProperty(shape, property, (value) =>
                            {
                                shape.StrokeThickness = value;
                                ShapesProperties[shape]["StrokeWidth"] = value;
                            });
                            break;

                        case "StrokeColor":
                            UpdateColorProperty(shape, property, (color) =>
                            {
                                shape.Stroke = new SolidColorBrush(color);
                                ShapesProperties[shape]["StrokeColor"] = color.ToString();
                            });
                            break;

                        case "RotationAngle":
                            UpdateNumericProperty(shape, property, (value) =>
                            {
                                if (shape.RenderTransform is RotateTransform rotateTransform)
                                {
                                    rotateTransform.Angle = value;
                                    ShapesProperties[shape]["RotationAngle"] = value;
                                }
                                else
                                {
                                    shape.RenderTransform = new RotateTransform(value);
                                    ShapesProperties[shape]["RotationAngle"] = value;
                                }
                            });
                            break;

                        case "ZIndex":
                            UpdateNumericProperty(shape, property, (value) =>
                            {
                                Panel.SetZIndex(shape, (int)value);
                                ShapesProperties[shape]["ZIndex"] = (int)value;
                            });
                            break;

                        default:
                            throw new ArgumentException($"Unsupported property: {property.Key}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating property {property.Key}: {ex.Message}");
                }
            }

            ResizeBorder.UpdateRectangle(SelectedShapes);
        }
        private void UpdateNumericProperty(Shape shape, KeyValuePair<string, object> property, Action<double> applyValue)
        {
            if (property.Value is string valueStr &&
                double.TryParse(valueStr, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double value))
            {
                applyValue(value);
            }
            else
            {
                throw new ArgumentException($"Invalid numeric value for {property.Key}");
            }
        }

        private void UpdateColorProperty(Shape shape, KeyValuePair<string, object> property, Action<Color> applyColor)
        {
            if (property.Value is string colorStr &&
                ColorConverter.ConvertFromString(colorStr) is Color color)
            {
                applyColor(color);
            }
            else
            {
                throw new ArgumentException($"Invalid color value for {property.Key}");
            }
        }

        public void SaveShapesToFile(string filePath)
        {
            var shapesData = new List<Dictionary<string, object>>();

            foreach (var shapeProperties in ShapesProperties.Values)
            {
                var shapeData = new Dictionary<string, object>();

                foreach (var property in shapeProperties)
                {
                    if (property.Value is Color color)
                    {
                        shapeData[property.Key] = color.ToString();
                    }
                    else
                    {
                        shapeData[property.Key] = property.Value;
                    }
                }

                shapesData.Add(shapeData);
            }

            string json = JsonSerializer.Serialize(shapesData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }


        public void LoadShapesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            string json = File.ReadAllText(filePath);
            var shapesData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

            if (shapesData == null)
            {
                throw new InvalidOperationException("Failed to deserialize shapes data.");
            }

            RemoveAllShapes();

            foreach (var shapeData in shapesData)
            {
                string type = shapeData["Type"].GetString();
                double width = shapeData["Width"].GetDouble();
                double height = shapeData["Height"].GetDouble();
                double x = shapeData["X"].GetDouble();
                double y = shapeData["Y"].GetDouble();
                double strokeWidth = shapeData["StrokeWidth"].GetDouble();
                double angle = shapeData["RotationAngle"].GetDouble();
                int zIndex = shapeData["ZIndex"].GetInt32();

                string fillColorStr = shapeData["FillColor"].GetString();
                Color fillColor = (Color)ColorConverter.ConvertFromString(fillColorStr);

                string strokeColorStr = shapeData["StrokeColor"].GetString();
                Color strokeColor = (Color)ColorConverter.ConvertFromString(strokeColorStr);

                if (type == "Rectangle")
                {
                    CreateRectangle(width, height, x, y, fillColor, strokeWidth, strokeColor, angle, zIndex);
                }
                else if (type == "Ellipse")
                {
                    CreateEllipse(width, height, x, y, fillColor, strokeWidth, strokeColor, angle, zIndex);
                }
                else if (type == "Triangle")
                {
                    CreateTriangle(width, height, x, y, fillColor, strokeWidth, strokeColor, angle, zIndex);
                }
            }
        }

        public void DuplicateSelectedShapes()
        {
            var templ = new List<Shape>(SelectedShapes);
            SelectedShapes.Clear();

            foreach (Shape originalShape in templ)
            {
                if (!ShapesProperties.ContainsKey(originalShape)) continue;

                var properties = ShapesProperties[originalShape];
                Shape? newShape = null;

                double offset = 20;

                if (properties["Type"].ToString() == "Rectangle")
                {
                    newShape = new Rectangle
                    {
                        Width = (double)properties["Width"],
                        Height = (double)properties["Height"],
                        Fill = new SolidColorBrush((Color)properties["FillColor"]),
                        Stroke = new SolidColorBrush((Color)properties["StrokeColor"]),
                        StrokeThickness = (double)properties["StrokeWidth"]
                    };
                }
                else if (properties["Type"].ToString() == "Ellipse")
                {
                    newShape = new Ellipse
                    {
                        Width = (double)properties["Width"],
                        Height = (double)properties["Height"],
                        Fill = new SolidColorBrush((Color)properties["FillColor"]),
                        Stroke = new SolidColorBrush((Color)properties["StrokeColor"]),
                        StrokeThickness = (double)properties["StrokeWidth"]
                    };
                }
                else if (properties["Type"].ToString() == "Triangle" && originalShape is Polygon polygon)
                {
                    var points = new PointCollection();
                    foreach (var point in polygon.Points)
                    {
                        points.Add(point);
                    }

                    newShape = new Polygon
                    {
                        Points = points,
                        Fill = new SolidColorBrush((Color)properties["FillColor"]),
                        Stroke = new SolidColorBrush((Color)properties["StrokeColor"]),
                        StrokeThickness = (double)properties["StrokeWidth"]
                    };
                }

                if (newShape != null)
                {
                    double newX = (double)properties["X"] + offset;
                    double newY = (double)properties["Y"] + offset;

                    Canvas.SetLeft(newShape, newX);
                    Canvas.SetTop(newShape, newY);

                    RotateTransform rotateTransform = new RotateTransform((double)properties["RotationAngle"], newShape.Width / 2, newShape.Height / 2);
                    newShape.RenderTransform = rotateTransform;

                    OurCanvas.Children.Add(newShape);

                    var newShapeProperties = new Dictionary<string, object>(properties)
                    {
                        ["X"] = newX,
                        ["Y"] = newY
                    };

                    ShapesProperties[newShape] = newShapeProperties;

                    SelectedShapes.Add(newShape);
                    ResizeBorder.RemoveResizeRectangle();
                    ResizeBorder.ShowResizeRectangle(SelectedShapes);
                }
            }
        }

        public void ChangeCanvasBackground(Color color)
        {
            OurCanvas.Background = new SolidColorBrush(color);
        }

        public void CreateBasicEllipse()
        {
            double width = 80;
            double height = 60;
            double x = 50;
            double y = 100;
            Color fillColor = Colors.Red;
            double strokeWidth = 3;
            Color strokeColor = Colors.Green;
            double angle = 0;
            int ZIndex = 0;

            CreateEllipse(width, height, x, y, fillColor, strokeWidth, strokeColor, angle, ZIndex);
        }

        public void CreateEllipse(double width, double height, double left, double top, Color fillColor, double strokeWidth, Color strokeColor, double angle, int ZIndex)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(fillColor),
                Stroke = new SolidColorBrush(strokeColor),
                StrokeThickness = strokeWidth,
            };

            Canvas.SetLeft(ellipse, left);
            Canvas.SetTop(ellipse, top);

            OurCanvas.Children.Add(ellipse);

            RotateTransform rotateTransform = new RotateTransform(angle, width / 2, height / 2);
            ellipse.RenderTransform = rotateTransform;

            Panel.SetZIndex(ellipse, ZIndex);

            ShapesProperties[ellipse] = new Dictionary<string, object>
            {
                { "Type", "Ellipse" },
                { "Width", width },
                { "Height", height },
                { "X", left },
                { "Y", top },
                { "FillColor", fillColor },
                { "StrokeWidth", strokeWidth },
                { "StrokeColor", strokeColor },
                { "RotationAngle", angle },
                { "ZIndex", ZIndex }
            };
        }

        private void OurCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            StartPoint = e.GetPosition(OurCanvas);

            bool isSiftPressed = Keyboard.IsKeyDown(Key.LeftShift);
            Shape? clickedShape = null;

            foreach (Shape shape in OurCanvas.Children)
            {
                if (shape.IsMouseOver)
                {
                    if (shape.Name != "ResizeHandle" && shape.Name != "ResizeBorder" && shape.Name != "RotateHandle")
                    {
                        clickedShape = shape;
                        break;
                    }
                    return;
                }
            }

            if (clickedShape != null)
            {
                IsDragging = true;
                SelectedShape = clickedShape;
                SelectingShape.Invoke(SelectedShape, ShapesProperties[SelectedShape]);

                if (isSiftPressed)
                {
                    if (SelectedShapes.Contains(clickedShape)) SelectedShapes.Remove(clickedShape);
                    else SelectedShapes.Add(clickedShape);
                    ResizeBorder.ShowResizeRectangle(SelectedShapes);
                    return;
                }

                if (SelectedShapes.Contains(clickedShape)) return;

                SelectedShapes.Clear();
                SelectedShapes.Add(clickedShape);
                ResizeBorder.ShowResizeRectangle(SelectedShapes);

            }
            else
            {
                SelectedShapes.Clear();
                SelectedShape = null;
                ResizeBorder.RemoveResizeRectangle();
                SelectingShape.Invoke(null, null);
            }

        }

        private void OurCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging && SelectedShapes.Count > 0)
            {
                Point currentPoint = e.GetPosition(OurCanvas);

                double offsetX = currentPoint.X - StartPoint.X;
                double offsetY = currentPoint.Y - StartPoint.Y;

                foreach (Shape shape in SelectedShapes)
                {
                    double newLeft = Canvas.GetLeft(shape) + offsetX;
                    double newTop = Canvas.GetTop(shape) + offsetY;

                    Canvas.SetLeft(shape, newLeft);
                    Canvas.SetTop(shape, newTop);

                    if (ShapesProperties.ContainsKey(shape))
                    {
                        ShapesProperties[shape]["X"] = newLeft;
                        ShapesProperties[shape]["Y"] = newTop;
                        ShapePropertiesChanged?.Invoke(shape, ShapesProperties[shape]);
                    }
                }

                ResizeBorder.RemoveResizeRectangle();
                
                StartPoint = currentPoint;
            }
        }

        private void OurCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsDragging)
            {
                ResizeBorder.ShowResizeRectangle(SelectedShapes);
                IsDragging = false;
            }
        }

    }
}
