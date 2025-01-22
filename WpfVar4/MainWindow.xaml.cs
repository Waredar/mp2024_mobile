using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace WpfVar4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        private Drawing Drawing;
        public ObservableCollection<PropertyItem> Properties { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Properties = [];
            DataContext = this;

            Drawing = new(Draw);

            Drawing.ShapePropertiesChanged += (shape, properties) =>
            {
                Properties.Clear();
                if (shape != null)
                {
                    foreach (var kvp in properties)
                    {
                        object value = kvp.Value;
                        if (value is double doubleValue)
                        {
                            value = Math.Round(doubleValue);
                        }
                        else if (value is float floatValue)
                        {
                            value = Math.Round(floatValue);
                        }
                        var prop = new PropertyItem(kvp.Key, value.ToString());
                        Properties.Add(prop);
                    }
                }
            };

                Drawing.SelectingShape += (shape, properties) =>
            {
                Properties.Clear();
                if (shape != null) {
                    foreach (var kvp in properties)
                    {
                        var prop = new PropertyItem(kvp.Key, kvp.Value.ToString());
                        Properties.Add(prop);
                    }
                }
            };

            this.KeyDown += (s, e) => { if (e.Key == Key.R) Drawing.DuplicateSelectedShapes(); };

            this.KeyDown += (s, e) => { if (e.Key == Key.Delete) Drawing.RemoveShape(); };
        }


        private void ColorPickerButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Parent is Grid grid)
                {
                    if (grid.Children[1] is TextBox textBox)
                    {

                        Color initialColor = (Color)ColorConverter.ConvertFromString(textBox.Text);
                        var colorPickerWindow = new ColorPickerWindow(initialColor);

                        if (colorPickerWindow.ShowDialog() == true)
                        {
                            var selectedColor = colorPickerWindow.SelectedColor;

                            textBox.Text = selectedColor.ToString();
                        }
                    }

                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var type = button.Content.ToString();
                if (type == "Круг")
                {
                    Drawing.CreateBasicEllipse();
                }

                if (type == "Прямоугольник")
                {
                    Drawing.CreateBasicRectangle();
                }
                
                if (type == "Треугольник")
                    Drawing.CreateBasicTriangle();

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                DefaultExt = "json",
                Title = "Сохранить файл"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    Drawing.SaveShapesToFile(saveFileDialog.FileName);
                    MessageBox.Show("Файл успешно сохранён!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                DefaultExt = "json",
                Title = "Открыть файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Drawing.LoadShapesFromFile(openFileDialog.FileName);
                    MessageBox.Show("Файл успешно загружен!", "Открытие", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Parent is Grid grid)
                {
                    if (grid.Children[0] is TextBlock textBlock)
                    {
                        Dictionary<string, object> dict = [];
                        dict.Add(textBlock.Text, textBox.Text);
                        if (Drawing.SelectedShape != null)
                        {
                            Drawing.EditShape(Drawing.SelectedShape, dict);
                        }
                    }
                }

            }
        }
    } 

}