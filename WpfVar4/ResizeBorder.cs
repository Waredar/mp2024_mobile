using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfVar4
{
    internal class ResizeBorder(Canvas newCanvas, Drawing draw)
    {
        public Rectangle? resizeRectangle;
        private readonly List<Ellipse> resizeHandles = [];
        private bool IsResizing = false;
        private Ellipse? ActiveResizeHandle;
        private Point StartPoint;
        private readonly Canvas canvas = newCanvas;
        private Shape? templShape;
        private Drawing drawing = draw;

        public void ShowResizeRectangle(Shape selectedShape)
        {
            RemoveResizeRectangle();
            templShape = selectedShape;
            var bounds = GetShapeBounds(selectedShape);

            resizeRectangle = new Rectangle
            {
                Name = "ResizeBorder",
                Stroke = Brushes.Black,
                StrokeDashArray = [4],
                StrokeThickness = 1,
                Width = selectedShape.Width + 10,
                Height = selectedShape.Height + 10
            };

            double left = Canvas.GetLeft(selectedShape) - 5;
            double top = Canvas.GetTop(selectedShape) - 5;

            Canvas.SetLeft(resizeRectangle, left);
            Canvas.SetTop(resizeRectangle, top);

            int maxZIndex = canvas.Children.OfType<UIElement>().Max(child => Panel.GetZIndex(child));
            Panel.SetZIndex(resizeRectangle, maxZIndex);

            canvas.Children.Add(resizeRectangle);

            CreateResizeHandles();
        }

        private void CreateResizeHandles()
        {
            AddResizeHandle(Canvas.GetLeft(resizeRectangle) - 5, Canvas.GetTop(resizeRectangle) - 5); // Top-left
            AddResizeHandle(Canvas.GetLeft(resizeRectangle) + resizeRectangle.Width - 5, Canvas.GetTop(resizeRectangle) - 5); // Top-right
            AddResizeHandle(Canvas.GetLeft(resizeRectangle) - 5, Canvas.GetTop(resizeRectangle) + resizeRectangle.Height - 5); // Bottom-left
            AddResizeHandle(Canvas.GetLeft(resizeRectangle) + resizeRectangle.Width - 5, Canvas.GetTop(resizeRectangle) + resizeRectangle.Height - 5); // Bottom-right
        }

        private void AddResizeHandle(double x, double y)
        {
            Ellipse handle = new()
            {
                Name = "Handle",
                Width = 10,
                Height = 10,
                Fill = Brushes.White,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(handle, x);
            Canvas.SetTop(handle, y);

            canvas.MouseMove += Handle_MouseMove;
            canvas.MouseLeftButtonDown += ResizeHandle_MouseLeftButtonDown;
            canvas.MouseLeftButtonUp += ResizeHandle_MouseLeftButtonUp;

            int maxZIndex = canvas.Children.OfType<UIElement>().Max(child => Panel.GetZIndex(child));
            Panel.SetZIndex(handle, maxZIndex);

            canvas.Children.Add(handle);
            resizeHandles.Add(handle);
        }

        private void ResizeHandle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var handle in resizeHandles) {
                if (handle.IsMouseOver) {
                    StartPoint = e.GetPosition(canvas);
                    IsResizing = true;
                    ActiveResizeHandle = handle;
                }
            }

        }

        private void Handle_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsResizing && ActiveResizeHandle != null && templShape != null)
            {
                Point currentPosition = e.GetPosition(canvas);
                ResizeShape(currentPosition, templShape);
            }
        }

        public void ResizeHandle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsResizing = false;
            ActiveResizeHandle = null;
        }

        private void ResizeShape(Point currentPosition, Shape selectedShape)
        {
            if (selectedShape == null || ActiveResizeHandle == null)
            {
                return;
            }

            double deltaX = currentPosition.X - StartPoint.X;
            double deltaY = currentPosition.Y - StartPoint.Y;

            double newWidth = Math.Max(10, selectedShape.Width + deltaX);
            double newHeight = Math.Max(10, selectedShape.Height + deltaY);

            selectedShape.Width = newWidth;
            selectedShape.Height = newHeight;

            if (drawing.ShapesProperties.ContainsKey(selectedShape))
            {
                drawing.ShapesProperties[selectedShape]["Width"] = newWidth;
                drawing.ShapesProperties[selectedShape]["Height"] = newHeight;
                drawing.RaiseShapePropertiesChanged(selectedShape, drawing.ShapesProperties[selectedShape]);
            }

            UpdateSelectionRectangle(selectedShape);
            UpdateResizeHandles(selectedShape);

            StartPoint = currentPosition;
        }

        public void UpdateSelectionRectangle(Shape selectedShape)
        {
            if (resizeRectangle == null || selectedShape == null)
            {
                return;
            }

            double left = Canvas.GetLeft(selectedShape) - 5;
            double top = Canvas.GetTop(selectedShape) - 5;

            resizeRectangle.Width = selectedShape.Width + 10;
            resizeRectangle.Height = selectedShape.Height + 10;

            Canvas.SetLeft(resizeRectangle, left);
            Canvas.SetTop(resizeRectangle, top);
        }

        public void UpdateResizeHandles(Shape selectedShape)
        {
            if (resizeHandles == null || resizeHandles.Count != 4)
            {
                return;
            }

            double left = Canvas.GetLeft(selectedShape);
            double top = Canvas.GetTop(selectedShape);
            double width = selectedShape.Width;
            double height = selectedShape.Height;

            Canvas.SetLeft(resizeHandles[0], left - 10); // Top-left
            Canvas.SetTop(resizeHandles[0], top - 10);

            Canvas.SetLeft(resizeHandles[1], left + width); // Top-right
            Canvas.SetTop(resizeHandles[1], top - 10);

            Canvas.SetLeft(resizeHandles[2], left - 10); // Bottom-left
            Canvas.SetTop(resizeHandles[2], top + height);

            Canvas.SetLeft(resizeHandles[3], left + width); // Bottom-right
            Canvas.SetTop(resizeHandles[3], top + height);
        }

        public void RemoveResizeRectangle()
        {
            canvas.MouseMove -= Handle_MouseMove;
            canvas.MouseLeftButtonDown -= ResizeHandle_MouseLeftButtonDown;
            canvas.MouseLeftButtonUp -= ResizeHandle_MouseLeftButtonUp;
            canvas.Children.Remove(resizeRectangle);
            resizeRectangle = null;
            foreach (var Handle in resizeHandles)
            {
                canvas.Children.Remove(Handle);
            }

            resizeHandles.Clear();
            templShape = null;
        }

        private Rect GetShapeBounds(Shape shape)
        {
            double left = Canvas.GetLeft(shape);
            double top = Canvas.GetTop(shape);
            double width = shape.RenderSize.Width;
            double height = shape.RenderSize.Height;

            return new Rect(left, top, width, height);
        }
    }
}
