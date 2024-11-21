

using System.Collections;

namespace wfaControlPuzzle
{
    public partial class wfaControlPuzzle : Form
    {
        private PictureBox[,] px;
        private readonly int numberOfmoving = 10;
        private const int STEP = 15;
        private Random rnd;

        public int Rows { get; }
        public int Colums { get; }
        public int cellWidth { get; set; }
        public int cellHeight { get; set; }
        public Point startMouseDown { get; private set; }

        public wfaControlPuzzle()
        {
            InitializeComponent();

            Rows = 6; Colums = 4;
            px = new PictureBox[Rows, Colums];

            CreateCells();
            ResizeCells();
            StartPositionCells();

            this.KeyDown += form1_KeyDown;

        }

        private void form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    StartPositionCells();
                    break;
                case Keys.F2:
                    ResizeCells();
                    break;
                case Keys.F3:
                    RandomLocationCells();
                    break;
                case Keys.F4:
                    WrongLocationCells();
                    break;
            }
        }

        private void WrongLocationCells()
        {
            StartPositionCells();
            for (int n = 0; n < numberOfmoving; n++)
            {
                var r1 = rnd.Next(Rows);
                var r2 = rnd.Next(Rows);
                var c1 = rnd.Next(Colums);
                var c2 = rnd.Next(Colums);

                (px[r1, c1].Location, px[r2, c2].Location) = (px[r2, c2].Location, px[r1, c1].Location); 
            }
        }

        private void RandomLocationCells()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Colums; j++)
                {
                    px[i, j].Location = new Point(
                        rnd.Next(this.ClientSize.Width - cellWidth),
                        rnd.Next(this.ClientSize.Height - cellHeight)
                        );
                }
        }

        private void StartPositionCells()
        {
            for (int i = 0; i < Rows; i++) 
                for (int j = 0; j < Colums; j++)
                {
                    px[i, j].Location = new Point(i * cellWidth, j * cellHeight);
                }
        }

        private void ResizeCells()
        {
           cellWidth = this.ClientSize.Width / Colums;
           cellHeight = this.ClientSize.Height / Rows;

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Colums; j++)
                {
                    px[i, j].Width = cellWidth;
                    px[i, j].Height = cellHeight;

                    px[i, j].Image = new Bitmap(cellWidth, cellHeight);
                    var g = Graphics.FromImage(px[i, j].Image);

                    g.DrawImage(
                        new Bitmap(new MemoryStream(Properties.Resources.Puzzle)),
                        new Rectangle(0, 0, cellWidth, cellHeight),
                        new Rectangle(i * cellWidth, j * cellHeight, cellWidth, cellHeight),
                        GraphicsUnit.Pixel
                    );
                    
                    g.Dispose();
                }
        }

        private void CreateCells()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Colums; j++)
                {
                    px[i, j] = new PictureBox();
                    px[i, j].BorderStyle = BorderStyle.FixedSingle;
                    px[i, j].MouseUp += Cell_MouseUp;
                    px[i, j].MouseMove += Cell_MouseMove;
                    px[i, j].MouseDown += Cell_MouseDown;


                    this.Controls.Add(px[i,j]);
                }
        }

        private void Cell_MouseUp(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox v)
            {
                if (e.Button == MouseButtons.Left)
                {
                    v.Cursor = Cursors.Default;

                    var l = v.Location;

                    for (int i = 0; i < Rows; i++)
                        for (int j = 0; j < Colums; j++)
                        {
                            var devX = Math.Abs(i * cellWidth - l.X);
                            var devY = Math.Abs(j * cellHeight - l.Y);

                            if (devX < STEP && devY < STEP)
                            {
                                l = new Point(i * cellWidth, j * cellHeight);
                                break;
                            }
                        }

                    v.Location = l;
                }
            }
        }

        private void Cell_MouseMove(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox v)
            {
                if (e.Button == MouseButtons.Left)
                {
                    v.Location = new Point(v.Location.X + e.Location.X - startMouseDown.X,
                        v.Location.Y + e.Location.Y - startMouseDown.Y);
                }
            }
        }

        private void Cell_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox v)
            {
                if (e.Button == MouseButtons.Left)
                {
                    startMouseDown = e.Location;
                    v.BringToFront();
                    v.Cursor = Cursors.SizeAll;
                }
            }
        }
    }
}
