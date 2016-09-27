using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracriteDemo.Models
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CityOfBirth { get; set; }

        public string CountryOfBirth { get; set; }
    }
}
