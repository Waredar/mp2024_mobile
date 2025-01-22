using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfPvZ.BaseClasses;

namespace WpfPvZ
{
    public class Render
    {
        private GameManager GameManager;
        private Canvas BackgroundLayer;
        private Canvas PlantsLayer;
        private Canvas ZombiesLayer;
        private Canvas ProjectilesLayer;
        private double marginLeft;
        private double marginRight;
        private double marginBottom;
        private double scaleX = 1.0;
        private double scaleY = 1.0;

        public Render(GameManager gameManager, Canvas rootCanvas)
        {
            if (gameManager == null) throw new ArgumentNullException(nameof(gameManager));
            if (rootCanvas == null) throw new ArgumentNullException(nameof(rootCanvas));

            this.GameManager = gameManager;

            BackgroundLayer = new Canvas();
            PlantsLayer = new Canvas();
            ZombiesLayer = new Canvas();
            ProjectilesLayer = new Canvas();

            rootCanvas.Children.Add(BackgroundLayer);
            rootCanvas.Children.Add(PlantsLayer);
            rootCanvas.Children.Add(ZombiesLayer);
            rootCanvas.Children.Add(ProjectilesLayer);
            marginLeft = GameManager.gameField.Cells[0, 0].Width * 0.3;
            marginRight = GameManager.gameField.Cells[0, 0].Width * 0.5;
            marginBottom = GameManager.gameField.Cells[0, 0].Width * 0.3;
            DrawGameField();
        }

        public void OnRender()
        {
            DrawPlants();
            DrawZombies();
            DrawProjectiles();
        }

        private void DrawGameField()
        {
            BackgroundLayer.Children.Clear();

            for (int y = 0; y < GameManager.gameField.Rows; y++)
            {
                for (int x = 0; x < GameManager.gameField.Columns; x++)
                {
                    var cell = GameManager.gameField.Cells[y, x];

                    var rectangle = new Rectangle
                    {
                        Width = (cell.Width - marginRight / GameManager.gameField.Columns) * scaleX,
                        Height = (cell.Height - marginBottom / GameManager.gameField.Rows) * scaleY,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };

                    Canvas.SetLeft(rectangle, rectangle.Width * x + marginLeft * scaleX);
                    Canvas.SetTop(rectangle, rectangle.Height * y);

                    BackgroundLayer.Children.Add(rectangle);
                }
            }
        }

        private void DrawPlants()
        {
            PlantsLayer.Children.Clear();
            foreach (var plant in GameManager.GetPlants())
            {
                if (plant.CurrentCell != null)
                {
                    var ellipse = new Ellipse
                    {
                        Width = (30 - marginRight / GameManager.gameField.Columns) * scaleX,
                        Height = (30 - marginBottom / GameManager.gameField.Rows) * scaleY,
                        Fill = Brushes.Yellow
                    };

                    var center = plant.CurrentCell.GetCenter();

                    Canvas.SetLeft(ellipse, (center.X - 15 + marginLeft) * scaleX);
                    Canvas.SetTop(ellipse, (center.Y - 15) * scaleX);

                    PlantsLayer.Children.Add(ellipse);
                }
            }
        }

        private void DrawZombies()
        {
            ZombiesLayer.Children.Clear();
            foreach (var zombie in GameManager.GetZombies())
            {
                var ellipse = new Ellipse
                {
                    Width = (30 - marginRight / GameManager.gameField.Columns) * scaleX,
                    Height = (30 - marginBottom / GameManager.gameField.Rows) * scaleY,
                    Fill = Brushes.Red
                };

                Canvas.SetLeft(ellipse, (zombie.Position.X - 15 + marginLeft) * scaleX);
                Canvas.SetTop(ellipse, (zombie.Position.Y - 15) * scaleY);

                ZombiesLayer.Children.Add(ellipse);
            }
        }

        private void DrawProjectiles()
        {
            ProjectilesLayer.Children.Clear();
            foreach (var projectile in GameManager.GetProjectiles())
            {
                var ellipse = new Ellipse
                {
                    Width = (30 - marginRight / GameManager.gameField.Columns) * scaleX,
                    Height = (30 - marginBottom / GameManager.gameField.Rows) * scaleY,
                    Fill = Brushes.Blue
                };

                Canvas.SetLeft(ellipse, (projectile.Position.X - 15 + marginLeft) * scaleX);
                Canvas.SetTop(ellipse, (projectile.Position.Y - 15) * scaleY);

                ProjectilesLayer.Children.Add(ellipse);
            }
        }

        private void SetScale(double newScaleX, double newScaleY)
        {
            scaleX = newScaleX;
            scaleY = newScaleY;
            DrawGameField();
            OnRender();
        }

        public void AdjustScaleToFit(Size windowSize)
        {
            double widthScale = windowSize.Width / GameManager.gameField.FieldWidth;
            double heightScale = windowSize.Height / GameManager.gameField.FieldHeight;
            SetScale(widthScale, heightScale);
        }


    }
}
