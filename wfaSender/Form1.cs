namespace wfaSender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += ButtonAll_Click;
            button2.Click += ButtonAll_Click;
            button3.Click += ButtonAll_Click;
            checkBox1.Click += ButtonAll_Click;
        }

        private void ButtonAll_Click(object? sender, EventArgs e)
        {
            if (sender is Control c)
                MessageBox.Show(c.Text);
        }
    }
}
