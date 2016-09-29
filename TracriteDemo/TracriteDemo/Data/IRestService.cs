using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;

namespace TracriteDemo.Data
{
    public interface IRestService
    {
        Task<List<Customer>> RefreshDataAsync();

        Task InsertCustomerAsync(Customer customer, bool isNewCustomer);
    }
}
