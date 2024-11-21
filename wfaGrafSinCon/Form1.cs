namespace wfaGrafSinCon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            this.BackgroundImageLayout = ImageLayout.None;
            this.Text += " : (sin - синий, cos - зелёный, tg - красный)";

            DrawAll();
            this.Resize += (s, e) => DrawAll();

        }

        private void DrawAll()
        {
            MyGraphic myGraphic = new(this.ClientSize.Width, this.ClientSize.Height);

            myGraphic.DrawAxes();
            myGraphic.DrawSin(Color.Blue);
            myGraphic.DrawCos(Color.Green);
            myGraphic.DrawTan(Color.Red);

            this.BackgroundImage = myGraphic.Bitmap;
        }
    }
}
