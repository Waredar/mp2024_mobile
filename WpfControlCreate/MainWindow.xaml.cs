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

namespace WpfControlCreate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.MouseDown += MainWindow_MouseDown;
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed) { }
            if (e.ChangedButton == MouseButton.Left)
            {
                var la = new Label();
                la.SetValue(Canvas.LeftProperty, e.GetPosition(this).X);
                la.SetValue(Canvas.TopProperty, e.GetPosition(this).Y);
                la.Content = $"{la.GetValue(Canvas.LeftProperty):0}:{la.GetValue(Canvas.TopProperty):0}";
                la.Background = Brushes.AliceBlue;
                main.Children.Add( la );
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                for (int i = 0; i < 10; i++)
                {
                    Random rnd = new Random();
                    var x = new Label();
                    var la = new Label();
                    la.SetValue(Canvas.LeftProperty, (double)rnd.Next(Convert.ToInt32(this.Width)));
                    la.SetValue(Canvas.TopProperty, (double)rnd.Next(Convert.ToInt32(this.Height)));
                    la.Content = $"{la.GetValue(Canvas.LeftProperty):0}:{la.GetValue(Canvas.TopProperty):0}";
                    la.Background = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255)));
                    main.Children.Add(la);
                }
            }

            if (e.ChangedButton == MouseButton.Middle)
            {
               main.Children.Clear();
            }
        }
    }
}