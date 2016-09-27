using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo;
using TracriteDemo.WinPhone;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_WinPhone81))]

namespace TracriteDemo.WinPhone
{
    public class SQLite_WinPhone81 : ISQLite
    {
        public SQLite_WinPhone81()
        {

        }

        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }
    }
}
