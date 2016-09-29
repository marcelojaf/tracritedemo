using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;

namespace TracriteDemo.Data
{
    public class CustomerManager
    {
        IRestService restService;

        public CustomerManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<Customer>> GetTasksAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task InsertCustomerAsync(Customer customer, bool isNewCustomer = false)
        {
            return restService.InsertCustomerAsync(customer, isNewCustomer);
        }

    }
}
