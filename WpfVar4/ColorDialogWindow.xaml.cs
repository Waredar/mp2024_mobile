using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace WpfVar4
{
    public partial class ColorPickerWindow : Window
    {
        public Color SelectedColor { get; private set; }

        public ColorPickerWindow(Color initialColor)
        {
            InitializeComponent();
            SetSliders(initialColor);
            UpdatePreview();
        }

        private void SetSliders(Color color)
        {
            RedSlider.Value = color.R;
            GreenSlider.Value = color.G;
            BlueSlider.Value = color.B;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            var color = Color.FromRgb(
                (byte)RedSlider.Value,
                (byte)GreenSlider.Value,
                (byte)BlueSlider.Value
            );
            SelectedColor = color;
            DataContext = new { SelectedColorPreview = new SolidColorBrush(color) };
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

