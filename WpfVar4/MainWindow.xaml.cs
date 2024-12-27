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
            Properties = new ObservableCollection<PropertyItem>();
            Properties.Add(new PropertyItem ("Новый параметр", "Значение" ));
            Properties.Add(new PropertyItem("Новый параметр", "Значение"));
            DataContext = this;

            Drawing = new(Draw);

            Drawing.ShapePropertiesChanged += (shape, properties) =>
            {
                Properties.Clear();
                foreach (var kvp in properties)
                    Properties.Add(new PropertyItem(kvp.Key, kvp.Value.ToString()));
            };

            Drawing.SelectShape += (shape, properties) =>
            {
                Properties.Clear();
                if (shape != null) {
                    foreach (var kvp in properties)
                        Properties.Add(new PropertyItem(kvp.Key, kvp.Value.ToString()));
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

            }
        }

    } 

}