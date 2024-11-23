using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfComand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Command_New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("new...");
        }

        private void Command_Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("save...");
        }

        private void Command_Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = edName?.Text != "";
        }

        private void Command_Hello_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }
    }

    public class MyCommands
    {
        public static RoutedCommand CommandHello { get; set; } = new("Hello", typeof(MainWindow));

    }
}