using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemoRESTService.Models;

namespace TracriteDemoRESTService.Services
{
    public interface ITracriteDemoRepository
    {
        bool DoesCustomerExist(string id);
        IEnumerable<Customer> All { get; }
        Customer Find(string id);
        void Insert(Customer customer);
        void Update(Customer customer);
    }
}
