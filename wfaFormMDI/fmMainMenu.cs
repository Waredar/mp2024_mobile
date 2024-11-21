namespace wfaFormMDI
{
    public partial class FormMDI : Form
    {
        public FormMDI()
        {
            InitializeComponent();

            btnNote.Click += BtnNote_Click;

        }

        private void BtnNote_Click(object? sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fmAbout = new fmAbout();
            fmAbout.ShowDialog();
        }
    }
}
