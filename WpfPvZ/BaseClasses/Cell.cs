using System;
using System.Numerics;
using System.Drawing;

namespace WpfPvZ.BaseClasses
{
    public enum CellType
    {
        Ground,
        Water,
        Road
    }

    public class Cell
    {
        public Point Position { get; }
        public float Width { get; }
        public float Height { get; }
        public CellType Type { get; }
        public List<uint> PlantIds { get; private set; } = [];

        public event Action<Cell, uint>? OnPlantAdded;
        public event Action<Cell, uint>? OnPlantRemoved;

        public Cell(int x, int y, CellType type, float width, float height)
        {
            if (width <= 0) throw new ArgumentException("Width must be greater than zero.", nameof(width));
            if (height <= 0) throw new ArgumentException("Height must be greater than zero.", nameof(height));

            Position = new Point(x, y);
            Type = type;
            Width = width;
            Height = height;
        }

        public bool ContainsPoint(float x, float y)
        {
            float cellLeft = Position.X * Width;
            float cellRight = (Position.X + 1) * Width;
            float cellTop = Position.Y * Height;
            float cellBottom = (Position.Y + 1) * Height;
            return x >= cellLeft && x < cellRight && y >= cellTop && y < cellBottom;
        }
        public Vector2 GetCenter()
        {
            float centerX = Position.X * Width + Width / 2;
            float centerY = Position.Y * Height + Height / 2;
            return new Vector2(centerX, centerY);
        }

        public void AddPlant(uint plantId)
        {
            if (plantId <= 0)
                throw new ArgumentException("Plant ID must be positive.", nameof(plantId));

            if (!PlantIds.Contains(plantId))
            {
                PlantIds.Add(plantId);
                OnPlantAdded?.Invoke(this, plantId);
            }
        }

        public void RemovePlant(uint plantId)
        {
            if (PlantIds.Remove(plantId))
            {
                OnPlantRemoved?.Invoke(this, plantId); // Уведомление об удалении
            }
        }

        public void RemoveAllPlants()
        {
            foreach (var plantId in PlantIds)
            {
                OnPlantRemoved?.Invoke(this, plantId);
            }
            PlantIds.Clear();
        }
    }
}
