using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemoRESTService.Models;

namespace TracriteDemoRESTService.Services
{
    public interface ITracriteDemoService
    {
        bool DoesCustomerExist(string id);
        Customer Find(string id);
        IEnumerable<Customer> GetData();
        void InsertData(Customer customer);
        void UpdateData(Customer customer);
    }
}
