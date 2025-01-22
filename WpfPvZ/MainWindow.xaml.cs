using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Numerics;
using System.Runtime.InteropServices;
using WpfPvZ.BaseClasses;
using WpfPvZ.ZombieEffects;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Input;

namespace WpfPvZ
{
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            AllocConsole();
        }
    }
    public partial class MainWindow : Window
    {
        private const int Columns = 9;
        private const int Rows = 5;
        private const float CellSize = 100;
        private GameField gameField;
        private GameManager gameManager;
        private Render render;
        private DispatcherTimer gameTimer;
        private TimeSpan lastRenderTime = TimeSpan.Zero;
        public MainWindow()
        {
            InitializeComponent();
            SetImage();
            InitializeGame();
            CompositionTarget.Rendering += OnRendering;
            GameCanvas.SizeChanged += GameCanvas_SizeChanged;
        }

        private void GameCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var newSize = e.NewSize;
            render.AdjustScaleToFit(newSize);
        }

        private void InitializeGame()
        {
            gameField = new GameField(Columns, Rows, CellSize, CellSize);
            gameManager = new GameManager(gameField);

            var plant1 = new PeaShotterPlant();

            gameManager.AddPlant(plant1, 0, 0);

            var zombie1 = new TestZombie(Zombie.ZombieType.Walker, "Walker", "Slow but strong", 100, 10, 0.5f, 100f, new(-1, 0), new(gameField.FieldWidth - 100, 50), 30f);
            var zombie2 = new TestZombie(Zombie.ZombieType.Walker, "Walker", "Slow but strong", 100, 10, 0.5f, 100f, new(-1, 0), new(gameField.FieldWidth - 50, 50), 30f);

            gameManager.AddZombie(zombie1);
            gameManager.AddZombie(zombie2);
            render = new(gameManager, GameCanvas);
        }

        private void OnRendering(object sender, EventArgs e)
        {
            var renderingEventArgs = (RenderingEventArgs)e;

            var currentRenderTime = renderingEventArgs.RenderingTime;
            float deltaTime = (float)(currentRenderTime - lastRenderTime).TotalSeconds;

            gameManager.Update(deltaTime);

            render.OnRender();

            lastRenderTime = currentRenderTime;
        }

        private void SetImage()
        {
            using (MemoryStream memoryStream = new MemoryStream(Properties.Resources.BackgroundDay1))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = bitmapImage,
                    Stretch = Stretch.Fill
                };

                RectangleGeometry clip = new RectangleGeometry
                {
                    Rect = new Rect(380, 0, 1350, 1028)
                };
                imageBrush.Viewbox = clip.Rect;
                imageBrush.ViewboxUnits = BrushMappingMode.Absolute;

                this.Background = imageBrush;
            }
        }

    }

    public class TestZombie : Zombie
    {
        public TestZombie(ZombieType type,
            string name,
            string description,
            int health,
            int damage,
            float atackSpeed,
            float moveSpeed,
            Vector2 direction,
            Vector2 position,
            float collisionRadius)
            : base(type, name, description, health, damage, atackSpeed, moveSpeed, direction, position, collisionRadius)
        {
        }
    }
}
