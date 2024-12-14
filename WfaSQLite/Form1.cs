using SQLite;

namespace WfaSQLite
{
    public partial class Form1 : Form
    {
        private readonly SQLiteConnection db;
        public Form1()
        {
            InitializeComponent();

            db = new SQLiteConnection("myDB.db");

            db.CreateTable<Logs>();
            db.CreateTable<City>();
            db.CreateTable<User>();

            db.Insert(new Logs() { DateTime = DateTime.Now });
            lvLogs.Columns.Add("ДатаВремя", 150);
            lvLogs.View = View.Details;

            foreach (var log in db.Table<Logs>())
            {
                lvLogs.Items.Add(log.DateTime.ToString());
            }

            buCityAdd.Click += (s, e) => db.Insert(new City { Name = edCityName.Text});
            buCity.Click += (s, e) => dataGridView1.DataSource = db.Table<City>().ToList();

            byShowQuery.Click += (s, e) => MessageBox.Show(db.ExecuteScalar<int>(tbQuery.Text).ToString());
        }
    }
}
