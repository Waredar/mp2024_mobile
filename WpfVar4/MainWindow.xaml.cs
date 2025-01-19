using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                        var prop = new PropertyItem(kvp.Key, kvp.Value.ToString());
                        Properties.Add(prop);
                    }
                }
            };

            Drawing.SelectShape += (shape, properties) =>
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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var type = button.Content.ToString();
                if (type == "Круг")
                {
                    Drawing.CreateEllipse();
                }

                if (type == "Прямоугольник")
                {
                    Drawing.CreateRectangle();
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