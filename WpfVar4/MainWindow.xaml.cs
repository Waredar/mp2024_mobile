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
            Properties.Add(new PropertyItem { Name = "Новый параметр", Value = "Значение" });
            Properties.Add(new PropertyItem { Name = "Новый параметр", Value = "Значение" });
            DataContext = this;

            Drawing = new(Draw);
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



    public class PropertyItem : INotifyPropertyChanged
    {
        private string _name;
        private string _value;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}