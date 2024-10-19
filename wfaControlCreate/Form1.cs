namespace wfaControlCreate
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();

            this.MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var x = new Label();
                x.Location = e.Location;
                x.Text = $"{e.Location.X}:{e.Location.Y}";
                x.BackColor = Color.LemonChiffon;
                x.AutoSize = true;
                this.Controls.Add(x);
            }

            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < 10; i++)
                {
                    var x = new Label();
                    x.Location = new Point(
                        rnd.Next(this.ClientSize.Width),
                        rnd.Next(this.ClientSize.Height)
                        );
                    x.Text = $"{e.Location.X}:{e.Location.Y}";
                    x.BackColor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                    x.AutoSize = true;
                    this.Controls.Add(x);
                }
            }

            if (e.Button == MouseButtons.Middle)
            {
                this.Controls.Clear();
            }
            this.Text = $"{Application.ProductName} {this.Controls.Count}";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
