namespace wfaGrafSinCon
{
    internal class MyGraphic
    {
        public int Width { get; }
        public int Height { get; }

        private readonly Bitmap b;
        private readonly Graphics g;
        private readonly int grShaftY;
        private int countWave = 5;

        public Bitmap Bitmap { get { return b; } }

        public MyGraphic(int newWidth, int newHeight) {
            
            Width = newWidth;
            Height = newHeight;

            b = new Bitmap(Width, Height);
            g = Graphics.FromImage(b);

            grShaftY = b.Height / 2;

        }

        public void DrawAxes()
        {
            g.DrawLine(new Pen(Color.Black), 0, grShaftY, b.Width, grShaftY);

            g.DrawLine(new Pen(Color.Black, 2), 0, 0 , 0, b.Height);
        }

        public void DrawSin(Color color)
        {
            double x, y;
            Point[] points= new Point[Width];


            for (int i = 0; i < Width; i++)
            {
                x = i;
                y = - Math.Sin(x * countWave * (Math.PI / b.Width)) * grShaftY + grShaftY + 2;

                Point p = new((int)x, (int)y);
                points[i] = p;
            }

            g.DrawLines(new Pen(color, 2), points);
        }

        public void DrawCos(Color color)
        {
            double x, y;
            Point[] points = new Point[Width];


            for (int i = 0; i < Width; i++)
            {
                x = i;
                y = -Math.Cos(x * countWave * (Math.PI / b.Width)) * grShaftY + grShaftY + 2;

                Point p = new((int)x, (int)y);
                points[i] = p;
            }

            g.DrawLines(new Pen(color, 2), points);
        }

        public void DrawTan(Color color)
        {
            double x, y;
            Point[] points = new Point[Width];

            // надо доделать
            for (int i = 0; i < Width; i++)
            {
                x = i;
                y = -Math.Tan(x * countWave * (Math.PI / b.Width)) * grShaftY + grShaftY + 2;

                Point p = new((int)x, (int)y);
                if (y > 0 && y < b.Height) 
                    points[i] = p;
            }

            g.DrawLines(new Pen(color, 2), points);
        }
    }
}