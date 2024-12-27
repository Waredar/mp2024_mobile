using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfVar4
{
    internal class ResizeBorder
    {
        public Border? resizeRectangle;
        private Rectangle? _resizeHandle;
        private bool _isResizing;

        public void ShowResizeRectangle(Canvas canvas, UIElement selectedElement)
        {
            var bounds = GetElementBounds(selectedElement);

            resizeRectangle = new Border
            {
                Name = "ResizeRectangle",
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Width = bounds.Width,
                Height = bounds.Height,
            };

            Canvas.SetLeft(resizeRectangle, Canvas.GetLeft(selectedElement));
            Canvas.SetTop(resizeRectangle, Canvas.GetTop(selectedElement));

            _resizeHandle = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black,
                Cursor = Cursors.SizeNWSE
            };

            _resizeHandle.MouseDown += ResizeHandle_MouseDown;

            canvas.Children.Add(resizeRectangle);
            canvas.Children.Add(_resizeHandle);
        }

        private void ResizeHandle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isResizing = true;
            _lastMousePosition = e.GetPosition(myCanvas);
            _resizeHandle.CaptureMouse();
        }

        public void RemoveResizeRectangle(Canvas canvas)
        {
            var resizeRect = canvas.Children.OfType<Border>().FirstOrDefault(b => b.Name == "ResizeRectangle");
            if (resizeRect != null)
            {
                canvas.Children.Remove(resizeRect);
            }
        }

        private Rect GetElementBounds(UIElement element)
        {
            double left = Canvas.GetLeft(element);
            double top = Canvas.GetTop(element);
            double width = element.RenderSize.Width;
            double height = element.RenderSize.Height;

            return new Rect(left, top, width, height);
        }
    }
}
