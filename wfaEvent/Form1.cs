namespace wfaEvent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += Button1_Click;

            button2.Click += (s, e) => MessageBox.Show("Вах");

            button3.Click += delegate
            {
                MessageBox.Show("Способ 3");
            };

            button4.Click += (s, e) => MessageBox.Show("Вах");
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Способ 1");
        }
    }
}
