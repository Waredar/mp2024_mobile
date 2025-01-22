using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfVar4
{
    internal class ResizeBorder(Drawing draw)
    {
        private readonly Drawing drawing = draw;
        public Rectangle? resizeBorder;
        private RotateTransform? rotateTransform;
        public double offset = 5;


        private bool IsDragingHandle = false;
        private string? ActiveHandleType;
        private Ellipse? ActiveHandle;
        private Point StartPoint;

        private readonly List<Ellipse> handles = [];

        public void ShowResizeRectangle(List<Shape> SelectedShapes)
        {
            RemoveResizeRectangle();

            if (SelectedShapes.Count == 0) return;

            double minX, minY, maxX, maxY;
            (minX, minY, maxX, maxY) = GetMinMaxCoor(SelectedShapes);

            resizeBorder = new()
            {
                Name = "ResizeBorder",
                Width = maxX - minX + offset * 2, 
                Height = maxY - minY + offset * 2,
                Stroke = Brushes.Black,
                StrokeDashArray = [4],
                StrokeThickness = 1,
            };

            Canvas.SetLeft(resizeBorder, minX - offset);
            Canvas.SetTop(resizeBorder, minY - offset);

            double angle = 0;

            if (SelectedShapes.Count == 1)
            {

                if (SelectedShapes[0].RenderTransform is RotateTransform rotateTransform)
                {
                    angle = rotateTransform.Angle;
                }

            }

            rotateTransform = new RotateTransform(angle, resizeBorder.Width / 2, resizeBorder.Height / 2);
            resizeBorder.RenderTransform = rotateTransform;

            int maxZIndex = drawing.OurCanvas.Children.OfType<UIElement>().Max(child => Panel.GetZIndex(child));
            Panel.SetZIndex(resizeBorder, maxZIndex);

            drawing.OurCanvas.Children.Add(resizeBorder);

            CreateResizeHandles();
        }
        private (double, double, double, double) GetMinMaxCoor(List<Shape> SelectedShapes)
        {
            if (SelectedShapes.Count > 1) return GetMinMaxCoor2(SelectedShapes);
            double minX = double.MaxValue, minY = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue;


    
            foreach (var shape in SelectedShapes)
            {
                double width, height;
                if (shape is Polygon polygon)
                {
                    width = draw.GetTriangleWidth(polygon);
                    height = draw.GetTriangleHeight(polygon);
                }
                else
                {
                    width = shape.Width;
                    height = shape.Height;
                }

                minX = Math.Min(minX, Canvas.GetLeft(shape));
                minY = Math.Min(minY, Canvas.GetTop(shape));
                maxX = Math.Max(maxX, Canvas.GetLeft(shape) + width);
                maxY = Math.Max(maxY, Canvas.GetTop(shape) + height);
            }

            return (minX, minY, maxX, maxY);
        }

        private (double, double, double, double) GetMinMaxCoor2(List<Shape> SelectedShapes)
        {
            double minX = double.MaxValue, minY = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue;

            foreach (var shape in SelectedShapes)
            {
                double left = Canvas.GetLeft(shape);
                double top = Canvas.GetTop(shape);
                double width, height;
                if (shape is Polygon polygon)
                {
                    width = draw.GetTriangleWidth(polygon);
                    height = draw.GetTriangleHeight(polygon);
                }
                else
                {
                    width = shape.Width;
                    height = shape.Height;
                }

                List<Point> points = [ new(left, top), new(left + width, top), new(left, top + height), new(left + width, top + height) ];

                if (shape.RenderTransform is RotateTransform rotateTransform)
                {
                    double angle = rotateTransform.Angle;
                    double centerX = left + rotateTransform.CenterX;
                    double centerY = top + rotateTransform.CenterY;

                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i] = RotatePoint(points[i], centerX, centerY, angle);
                    }
                }

                foreach (var point in points)
                {
                    minX = Math.Min(minX, point.X);
                    minY = Math.Min(minY, point.Y);
                    maxX = Math.Max(maxX, point.X);
                    maxY = Math.Max(maxY, point.Y);
                }
            }

            return (minX, minY, maxX, maxY);
        }

        private Point RotatePoint(Point point, double centerX, double centerY, double angle)
        {
            double radians = angle * Math.PI / 180;

            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            double dx = point.X - centerX;
            double dy = point.Y - centerY;

            double rotatedX = centerX + (dx * cos - dy * sin);
            double rotatedY = centerY + (dx * sin + dy * cos);

            return new Point(rotatedX, rotatedY);
        }


        private (double, double) GetBorederAbsoluteCenter()
        {
            return (Canvas.GetLeft(resizeBorder) + resizeBorder.Width / 2, Canvas.GetTop(resizeBorder) + resizeBorder.Height / 2);
        }

        private void CreateResizeHandles()
        {
            AddHandle(Canvas.GetLeft(resizeBorder), Canvas.GetTop(resizeBorder), "Resize"); // Top-left
            AddHandle(Canvas.GetLeft(resizeBorder) + resizeBorder.Width, Canvas.GetTop(resizeBorder), "Resize"); // Top-right
            AddHandle(Canvas.GetLeft(resizeBorder), Canvas.GetTop(resizeBorder) + resizeBorder.Height, "Resize"); // Bottom-left
            AddHandle(Canvas.GetLeft(resizeBorder) + resizeBorder.Width, Canvas.GetTop(resizeBorder) + resizeBorder.Height, "Resize"); // Bottom-right
            AddHandle(Canvas.GetLeft(resizeBorder) + resizeBorder.Width / 2, Canvas.GetTop(resizeBorder) - 10, "Rotate"); // Ручка для вращения над рамкой
        }

        private void AddHandle(double x, double y, string handleType)
        {
            Ellipse handle = new()
            {
                Name = handleType + "Handle",
                Width = 10,
                Height = 10,
                Fill = Brushes.White,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(handle, x - handle.Width / 2);
            Canvas.SetTop(handle, y - handle.Height / 2);

            double centerX, centerY;

            (centerX, centerY) = GetAxisCenter(Canvas.GetLeft(handle), Canvas.GetTop(handle));

            RotateTransform transform = new RotateTransform(rotateTransform.Angle, centerX, centerY);
            handle.RenderTransform = transform;

            drawing.OurCanvas.MouseMove += Handle_MouseMove;
            drawing.OurCanvas.MouseLeftButtonDown += Handle_MouseLeftButtonDown;
            drawing.OurCanvas.MouseLeftButtonUp += Handle_MouseLeftButtonUp;

            int maxZIndex = drawing.OurCanvas.Children.OfType<UIElement>().Max(child => Panel.GetZIndex(child));
            Panel.SetZIndex(handle, maxZIndex);

            drawing.OurCanvas.Children.Add(handle);
            handles.Add(handle);
        }

        private (double, double) GetAxisCenter(double left, double top)
        {
            (double centerX, double centerY) = GetBorederAbsoluteCenter();
            
            return (centerX - left, centerY - top);
        }

        private void Handle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var handle in handles)
            {
                if (handle.IsMouseOver)
                {
                    StartPoint = e.GetPosition(drawing.OurCanvas);
                    IsDragingHandle = true;
                    ActiveHandle = handle;
                    ActiveHandleType = handle.Name.Contains("Resize") ? "Resize" : "Rotate";
                    break;
                }
            }
        }

        private void Handle_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragingHandle && ActiveHandle != null && drawing.SelectedShape != null)
            {
                Point currentPosition = e.GetPosition(drawing.OurCanvas);
                if (ActiveHandleType == "Resize")
                {
                    ResizeShapes(currentPosition, drawing.SelectedShapes);
                }
                else if (ActiveHandleType == "Rotate")
                {
                    RotateShapes(currentPosition, drawing.SelectedShapes);
                }
            }
        }

        private void Handle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDragingHandle = false;
            ActiveHandle = null;
            ActiveHandleType = null;
        }

        private void ResizeShapes(Point currentPosition, List<Shape> selectedShapes)
        {
            if (selectedShapes == null || selectedShapes.Count == 0 || ActiveHandle == null)
                return;

            (double groupMinX, double groupMinY, double groupMaxX, double groupMaxY) = GetMinMaxCoor(selectedShapes);
            double groupWidth = groupMaxX - groupMinX;
            double groupHeight = groupMaxY - groupMinY;

            double fixedX = groupMinX, fixedY = groupMinY;
            switch (ActiveHandle)
            {
                case var handle when handle == handles[0]: // Top-left
                    fixedX = groupMaxX;
                    fixedY = groupMaxY;
                    break;

                case var handle when handle == handles[1]: // Top-right
                    fixedX = groupMinX;
                    fixedY = groupMaxY;
                    break;

                case var handle when handle == handles[2]: // Bottom-left
                    fixedX = groupMaxX;
                    fixedY = groupMinY;
                    break;

                case var handle when handle == handles[3]: // Bottom-right
                    fixedX = groupMinX;
                    fixedY = groupMinY;
                    break;
            }

            Point delta = new(currentPosition.X - StartPoint.X, currentPosition.Y - StartPoint.Y);

            foreach (var shape in selectedShapes)
            {
                double width, height;
                if (shape is Polygon polygon)
                {
                    width = draw.GetTriangleWidth(polygon);
                    height = draw.GetTriangleHeight(polygon);
                }
                else
                {
                    width = shape.Width;
                    height = shape.Height;
                }

                ResizeShape(shape, delta, groupWidth, groupHeight, fixedX, fixedY, width, height);
            }

            UpdateRectangle(selectedShapes);
            StartPoint = currentPosition;
        }

        private void ResizeShape(Shape shape, Point delta, double groupWidth, double groupHeight, double fixedX, double fixedY, double width, double height)
        {
            RotateTransform? rotateTransform = shape.RenderTransform as RotateTransform;
            if (rotateTransform == null)
                return;

            Vector transformedDelta = RotateVector(delta, -rotateTransform.Angle);

            double scaleX = 1.0, scaleY = 1.0;

            switch (ActiveHandle)
            {
                case var handle when handle == handles[0]: // Top-left
                    scaleX = (groupWidth - transformedDelta.X) / groupWidth;
                    scaleY = (groupHeight - transformedDelta.Y) / groupHeight;
                    break;

                case var handle when handle == handles[1]: // Top-right
                    scaleX = (groupWidth + transformedDelta.X) / groupWidth;
                    scaleY = (groupHeight - transformedDelta.Y) / groupHeight;
                    break;

                case var handle when handle == handles[2]: // Bottom-left
                    scaleX = (groupWidth - transformedDelta.X) / groupWidth;
                    scaleY = (groupHeight + transformedDelta.Y) / groupHeight;
                    break;

                case var handle when handle == handles[3]: // Bottom-right
                    scaleX = (groupWidth + transformedDelta.X) / groupWidth;
                    scaleY = (groupHeight + transformedDelta.Y) / groupHeight;
                    break;
            }

            scaleX = Math.Max(scaleX, 0.1);
            scaleY = Math.Max(scaleY, 0.1);

            double newWidth = Math.Max(10, width * scaleX);
            double newHeight = Math.Max(10, height * scaleY);

            double left = Canvas.GetLeft(shape);
            double top = Canvas.GetTop(shape);

            double newLeft = fixedX + (left - fixedX) * scaleX;
            double newTop = fixedY + (top - fixedY) * scaleY;

            Canvas.SetLeft(shape, newLeft);
            Canvas.SetTop(shape, newTop);

            if (shape is Polygon polygon)
            {
                drawing.SetTriangleDimensions(polygon, newWidth, newHeight);
            }
            else
            {
                shape.Width = newWidth;
                shape.Height = newHeight;
            }

            UpdateShapeProperties(shape, newLeft, newTop, newWidth, newHeight, rotateTransform.Angle);
        }

        private void UpdateShapeProperties(Shape shape, double left, double top, double width, double height, double abgle)
        {
            if (drawing.ShapesProperties.ContainsKey(shape))
            {
                drawing.ShapesProperties[shape]["Width"] = width;
                drawing.ShapesProperties[shape]["Height"] = height;
                drawing.ShapesProperties[shape]["X"] = left;
                drawing.ShapesProperties[shape]["Y"] = top;
                drawing.ShapesProperties[shape]["RotationAngle"] = abgle;
                drawing.RaiseShapePropertiesChanged(shape, drawing.ShapesProperties[shape]);
            }
        }

        private static Vector RotateVector(Point vector, double angle)
        {
            double radians = angle * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            return new Vector(
                vector.X * cos - vector.Y * sin,
                vector.X * sin + vector.Y * cos
            );
        }


        private void RotateShapes(Point currentPosition, List<Shape> selectedShapes)
        {
            if (selectedShapes == null || selectedShapes.Count == 0 || resizeBorder == null)
                return;

            double groupCenterX = 0;
            double groupCenterY = 0;
            foreach (var shape in selectedShapes)
            {
                double width, height;
                if ( shape is Polygon polygon)
                {
                    width = drawing.GetTriangleWidth(polygon);
                    height = drawing.GetTriangleHeight(polygon);
                }    
                else
                {
                    width = shape.Width;
                    height = shape.Height;
                }
                double shapeCenterX = Canvas.GetLeft(shape) + width/ 2;
                double shapeCenterY = Canvas.GetTop(shape) + height / 2;
                groupCenterX += shapeCenterX;
                groupCenterY += shapeCenterY;
            }

            groupCenterX /= selectedShapes.Count;
            groupCenterY /= selectedShapes.Count;

            double newAngle = CalculateAngle(groupCenterX, groupCenterY, currentPosition);

            foreach (var shape in selectedShapes)
            {
                double width, height;
                if (shape is Polygon polygon)
                {
                    width = drawing.GetTriangleWidth(polygon);
                    height = drawing.GetTriangleHeight(polygon);
                }
                else
                {
                    width = shape.Width;
                    height = shape.Height;
                }
                double shapeCenterX = Canvas.GetLeft(shape) + width / 2;
                double shapeCenterY = Canvas.GetTop(shape) + height / 2;

                Vector offset = new Vector(shapeCenterX - groupCenterX, shapeCenterY - groupCenterY);

                double radiansDelta = (newAngle - (shape.RenderTransform as RotateTransform)?.Angle ?? 0) * Math.PI / 180;

                double rotatedX = groupCenterX + (offset.X * Math.Cos(radiansDelta) - offset.Y * Math.Sin(radiansDelta));
                double rotatedY = groupCenterY + (offset.X * Math.Sin(radiansDelta) + offset.Y * Math.Cos(radiansDelta));

                Canvas.SetLeft(shape, rotatedX - width / 2);
                Canvas.SetTop(shape, rotatedY - height / 2);

                RotateTransform shapeTransform = shape.RenderTransform as RotateTransform ?? new RotateTransform(0, width / 2, height / 2);
                shapeTransform.Angle = newAngle;
                shape.RenderTransform = shapeTransform;

                UpdateShapeProperties(shape, Canvas.GetLeft(shape), Canvas.GetTop(shape), width, height , newAngle);
            }

            UpdateRectangle(selectedShapes);
            UpdateHandles();

            StartPoint = currentPosition;
        }




        private double CalculateAngle(double centerX, double centerY, Point currentPoint)
        {
            double dx = currentPoint.X - centerX;
            double dy = currentPoint.Y - centerY;

            double angle = Math.Atan2(dy, dx) * (180 / Math.PI);
            return (angle + 450) % 360;
        }
        public void UpdateRectangle(List<Shape> selectedShapes)
        {
            if (selectedShapes == null || selectedShapes.Count == 0 || resizeBorder == null || rotateTransform == null)
                return;

            const double offset = 5;

            double minX, minY, maxX, maxY;
            (minX, minY, maxX, maxY) = GetMinMaxCoor(selectedShapes);

            resizeBorder.Width = maxX - minX + offset * 2;
            resizeBorder.Height = maxY - minY + offset * 2;

            Canvas.SetLeft(resizeBorder, minX - offset);
            Canvas.SetTop(resizeBorder, minY - offset);

            rotateTransform.CenterX = resizeBorder.Width / 2;
            rotateTransform.CenterY = resizeBorder.Height / 2;

            double angle = 0;

            if (selectedShapes.Count == 1 && selectedShapes[0].RenderTransform is RotateTransform singleRotateTransform)
            {
                angle = singleRotateTransform.Angle;
            }
            rotateTransform.Angle = angle;
            resizeBorder.RenderTransform = rotateTransform;

            UpdateHandles();
        }

        private void UpdateHandles()
        {
            if (resizeBorder == null) return;

            List<Point> points = new List<Point>
    {
        new Point(Canvas.GetLeft(resizeBorder), Canvas.GetTop(resizeBorder)),
        new Point(Canvas.GetLeft(resizeBorder) + resizeBorder.Width, Canvas.GetTop(resizeBorder)),
        new Point(Canvas.GetLeft(resizeBorder), Canvas.GetTop(resizeBorder) + resizeBorder.Height),
        new Point(Canvas.GetLeft(resizeBorder) + resizeBorder.Width, Canvas.GetTop(resizeBorder) + resizeBorder.Height),
        new Point(Canvas.GetLeft(resizeBorder) + resizeBorder.Width / 2, Canvas.GetTop(resizeBorder) - 10)
    };

            for (int i = 0; i < handles.Count; i++)
            {
                Canvas.SetLeft(handles[i], points[i].X - handles[i].Width / 2);
                Canvas.SetTop(handles[i], points[i].Y - handles[i].Height / 2);

                double centerX, centerY;
                (centerX, centerY) = GetAxisCenter(Canvas.GetLeft(handles[i]), Canvas.GetTop(handles[i]));

                RotateTransform transform = new RotateTransform(rotateTransform.Angle, centerX, centerY);
                handles[i].RenderTransform = transform;
            }
        }


        public void RemoveResizeRectangle()
        {
            drawing.OurCanvas.MouseMove -= Handle_MouseMove;
            drawing.OurCanvas.MouseLeftButtonDown -= Handle_MouseLeftButtonDown;
            drawing.OurCanvas.MouseLeftButtonUp -= Handle_MouseLeftButtonUp;
            drawing.OurCanvas.Children.Remove(resizeBorder);
            rotateTransform = null;
            resizeBorder = null;
            foreach (var handle in handles)
            {
                drawing.OurCanvas.Children.Remove(handle);
            }
            handles.Clear();
        }
    }
}
