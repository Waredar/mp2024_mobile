using System;

namespace WpfPvZ.BaseClasses
{
    public class GameField
    {
        public int Rows { get; }
        public int Columns { get; }
        public Cell[,] Cells { get; }

        public float FieldWidth => Cells?[0, 0]?.Width * Columns ?? 0;
        public float FieldHeight => Cells?[0, 0]?.Height * Rows ?? 0;

        public GameField(int columns, int rows, float cellWidth, float cellHeight, CellType defaultCellType = CellType.Ground)
        {
            if (columns <= 0) throw new ArgumentException("Columns must be greater than zero.", nameof(columns));
            if (rows <= 0) throw new ArgumentException("Rows must be greater than zero.", nameof(rows));
            if (cellWidth <= 0) throw new ArgumentException("Cell width must be greater than zero.", nameof(cellWidth));
            if (cellHeight <= 0) throw new ArgumentException("Cell height must be greater than zero.", nameof(cellHeight));

            Rows = rows;
            Columns = columns;
            Cells = new Cell[rows, columns];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Cells[y, x] = new Cell(x, y, defaultCellType, cellWidth, cellHeight);
                }
            }
        }

        public Cell? GetCell(int x, int y)
        {
            if (x >= 0 && x < Columns && y >= 0 && y < Rows)
            {
                return Cells[y, x];
            }
            return null;
        }

        public bool AddPlantToCell(int x, int y, uint plantId)
        {
            var cell = GetCell(x, y);
            if (cell != null && cell.PlantIds.Count == 0)
            {
                cell.AddPlant(plantId);
                return true;
            }
            return false;
        }

        public bool RemovePlantFromCell(int x, int y, uint plantId)
        {
            var cell = GetCell(x, y);
            if (cell != null && cell.PlantIds.Contains(plantId))
            {
                cell.RemovePlant(plantId);
                return true;
            }
            return false;
        }
    }

}
