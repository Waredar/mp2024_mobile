

using System.Collections;

namespace wfaControlPuzzle
{
    public partial class wfaControlPuzzle : Form
    {
        private PictureBox[,] px;
        private readonly int numberOfmoving = 10;
        private const int STEP = 15;
        private (int Width, int Height, int startX, int startY) availableSpace;
        private Random rnd = new();

        public int Rows { get; private set; }
        public int Colums { get; private set; }
        public int cellWidth { get; set; }
        public int cellHeight { get; set; }

        public Point startMouseDown { get; private set; }

        public wfaControlPuzzle()
        {
            InitializeComponent();

            startGame();

            this.KeyDown += form1_KeyDown;
            this.ResizeEnd += OnResizeEnd;
            foreach (ToolStripMenuItem item in DifficulToolStripMenuItem.DropDown.Items)
                item.Click += DifficulToolStripMenuItem_Click;
        }



        private void DifficulToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem toolStripMenuItem)
            {
                if (toolStripMenuItem.Checked) return;

                var parent = toolStripMenuItem.GetCurrentParent();
                if (parent == null) return;

                foreach (ToolStripMenuItem item in parent.Items)
                    item.Checked = false;
                toolStripMenuItem.Checked = true;

                startGame();
            }
        }

        private void startGame()
        {
            if (EasyToolStripMenuItem.Checked)
            {
                Rows = 5; Colums = 5;
            }

            if (NormalToolStripMenuItem.Checked)
            {
                Rows = 8; Colums = 8;
            }

            if (HardToolStripMenuItem.Checked)
            {
                Rows = 10; Colums = 10;
            }
            
            if (px != null)
            {
                foreach (var item in px)
                    this.Controls.Remove(item);
                Array.Clear(px, 0, px.Length);
            }
            px = new PictureBox[Rows, Colums];
            GetEmptySpace();
            CreateCells();
            ResizeCells();
            StartPositionCells();
        }

        private void OnResizeEnd(object? sender, EventArgs e)
        {
            GetEmptySpace();
        }

        private void GetEmptySpace()
        {
            availableSpace.Width = this.ClientSize.Width;
            availableSpace.Height = this.ClientSize.Height - menuStrip1.Height - statusStrip1.Height;
            availableSpace.startX = 0;
            availableSpace.startY = menuStrip1.Height;
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
                    StartPositionCells();
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
                        availableSpace.startX + rnd.Next(availableSpace.Width + availableSpace.startX - cellWidth),
                        availableSpace.startY + rnd.Next(availableSpace.Height + availableSpace.startY - cellHeight)
                        );
                }
        }

        private void StartPositionCells()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Colums; j++)
                {
                    px[i, j].Location = new Point(j * cellWidth, i * cellHeight + availableSpace.startY);
                }
        }

        private void ResizeCells()
        {
            GetEmptySpace();
            cellWidth = availableSpace.Width / Colums;
            cellHeight = availableSpace.Height / Rows;

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Colums; j++)
                {
                    px[i, j].Width = cellWidth;
                    px[i, j].Height = cellHeight;
                    
                    if (px[i, j].Image != null)
                        px[i, j].Image.Dispose();
                    px[i, j].Image = new Bitmap(cellWidth, cellHeight);
                    var g = Graphics.FromImage(px[i, j].Image);

                    g.DrawImage(
                        new Bitmap(new MemoryStream(Properties.Resources.Puzzle)),
                        new Rectangle(0, 0, cellWidth, cellHeight),
                        new Rectangle(j * cellWidth, i * cellHeight, cellWidth, cellHeight),
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

                    this.Controls.Add(px[i, j]);
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
                            var devX = Math.Abs(j * cellWidth + availableSpace.startX - l.X);
                            var devY = Math.Abs(i * cellHeight + availableSpace.startY - l.Y);

                            if (devX < STEP && devY < STEP)
                            {
                                l = new Point(j * cellWidth + availableSpace.startX, i * cellHeight + availableSpace.startY);
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
                    int offSetX = v.Location.X + e.Location.X - startMouseDown.X;
                    int offSetY = v.Location.Y + e.Location.Y - startMouseDown.Y;
                    if (offSetY < availableSpace.startY) offSetY = availableSpace.startY;
                    if (offSetX < availableSpace.startX) offSetX = availableSpace.startX;
                    if (offSetY > availableSpace.startY + availableSpace.Height - cellHeight)
                        offSetY = availableSpace.startY + availableSpace.Height - cellHeight;
                    if (offSetX > availableSpace.startX + availableSpace.Width - cellWidth)
                        offSetX = availableSpace.startX + availableSpace.Width - cellWidth;
                    v.Location = new Point(offSetX, offSetY);
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
