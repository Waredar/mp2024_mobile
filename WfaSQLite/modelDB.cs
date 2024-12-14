using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WfaSQLite
{
    public class Logs
    {
        public DateTime DateTime { get; set; }
    }

    public class City
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
