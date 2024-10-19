namespace wfaSearchCitiy
{
    public partial class Form1 : Form
    {
        private string[] cities;

        public Form1()
        {
            InitializeComponent();

            this.Text = $"{Application.ProductName}";

            cities = Properties.Resources.Cities.Split("\n");

            edSearch.Focus();
            edSearch.TextChanged += EdSearch_TextChanged;
            EdSearch_TextChanged(this, EventArgs.Empty);
        }

        private void EdSearch_TextChanged(object? sender, EventArgs e)
        {
            var r = cities.Where(x => x.ToUpper().Contains(edSearch.Text.ToUpper()))
                .Where(x => x != "")
                .ToArray();
            edResult.Text = String.Join("\n", r);
            this.Text = $"{Application.ProductName} | {edSearch.Text} | {r.Count()}";
        }
    }
}
