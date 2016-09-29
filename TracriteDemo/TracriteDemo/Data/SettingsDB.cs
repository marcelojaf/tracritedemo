using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;
using Xamarin.Forms;

namespace TracriteDemo.Data
{
    public class SettingsDB
    {
        static object locker = new object();

        SQLiteConnection database;

        public SettingsDB()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Settings>();
        }

        public Settings getSettings(int id)
        {
            lock (locker)
            {
                return database.Table<Settings>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int insertSettings(Settings settings)
        {
            lock (locker)
            {
                if (settings.ID != 0)
                {
                    database.Update(settings);
                    return settings.ID;
                }
                else
                {
                    return database.Insert(settings);
                }
            }
        }
    }
}
