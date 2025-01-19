namespace WfaPaint
{
    enum MyDrawMode
    {
        Pen,
        Line,
        Ellipse,
        Rectangle,
        Star,
    }
    public partial class WfaPaint : Form
    {
        private MyDrawMode myDrawMode;

        public WfaPaint()
        {
            InitializeComponent();

            btnPen.Click += BtnDrawClick;
            btnEllipse.Click += BtnDrawClick;
            btnLine.Click += BtnDrawClick;
            btnRec.Click += BtnDrawClick;
            btnStar.Click += BtnDrawClick;
        }

        private void BtnDrawClick(object? sender, EventArgs e)
        {
            if (sender is Button b)
            {
                btnPen.Enabled = true;
                btnClear.Enabled = true; 
                btnEllipse.Enabled = true;
                btnLine.Enabled = true;
                btnRec.Enabled = true;
                btnSave.Enabled = true;
                btnStar.Enabled = true;
                switch (b.Text)
                {
                    case ("Карандаш"):
                        btnPen.Enabled = false;
                        myDrawMode = MyDrawMode.Pen;
                        break;
                    case ("Линия"):
                        btnLine.Enabled = false;
                        myDrawMode = MyDrawMode.Pen;
                        break;
                    case ("Эллипс"):
                        btnEllipse.Enabled= false;
                        myDrawMode = MyDrawMode.Pen;
                        break;
                    case ("Прямоугольник"):
                        btnRec.Enabled= false;
                        myDrawMode = MyDrawMode.Pen;
                        break;
                    case ("Звёздочки"):
                        btnStar.Enabled= false;
                        myDrawMode = MyDrawMode.Pen;
                        break;
                    default:
                        break;

                }
            }
        }

        private void PxImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (myDrawMode)
                {

                }
            }
        }

    }
}
