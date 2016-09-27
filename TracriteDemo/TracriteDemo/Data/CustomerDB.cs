using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using TracriteDemo.Models;

namespace TracriteDemo.Data
{
    public class CustomerDB
    {
        static object locker = new object();

        SQLiteConnection database;

        public CustomerDB()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Customer>();
        }

        public IEnumerable<Customer> getCustomers()
        {
            lock (locker)
            {
                return (from i in database.Table<Customer>() select i).ToList();
            }
        }

        public int insertCustomers(Customer customer)
        {
            lock (locker)
            {
                if (customer.ID != 0)
                {
                    database.Update(customer);
                    return customer.ID;
                }
                else
                {
                    return database.Insert(customer);
                }
            }
        }
    }
}
