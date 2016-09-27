using System;
using TracriteDemo;
using Xamarin.Forms;
using TracriteDemo.Droid;
using System.IO;


[assembly: Dependency (typeof (SQLite_Android))]

namespace TracriteDemo.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {

        }

        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TracriteDemoSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }
    }
}