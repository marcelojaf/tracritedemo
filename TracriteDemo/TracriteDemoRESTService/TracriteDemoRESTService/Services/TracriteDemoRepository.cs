using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TracriteDemoRESTService.Models;

namespace TracriteDemoRESTService.Services
{
    public class TracriteDemoRepository : ITracriteDemoRepository
    {
        private List<Customer> _customersList;

        public TracriteDemoRepository()
        {
            InitializeData();
        }

        public IEnumerable<Customer> All
        {
            get
            {
                return _customersList;
            }
        }

        public bool DoesCustomerExist(string id)
        {
            return _customersList.Any(item => item.ID == id);
        }

        public Customer Find(string id)
        {
            return _customersList.Where(item => item.ID == id).FirstOrDefault();
        }

        public void Insert(Customer customer)
        {
            _customersList.Add(customer);
        }

        public void Update(Customer customer)
        {
            var todoItem = this.Find(customer.ID);
            var index = _customersList.IndexOf(todoItem);
            _customersList.RemoveAt(index);
            _customersList.Insert(index, customer);
        }

        private void InitializeData()
        {
            _customersList = new List<Customer>();

            var customer = new Customer
            {
                ID = "1",
                FirstName = "Marcelo",
                LastName = "Amador",
                CityOfBirth = "Goiania",
                CountryOfBirth = "Brazil"
            };

            var customer2 = new Customer
            {
                ID = "1",
                FirstName = "Carlos",
                LastName = "Oliveira",
                CityOfBirth = "Fronteira",
                CountryOfBirth = "Brazil"
            };

            _customersList.Add(customer);
            _customersList.Add(customer2);
        }
    }
}