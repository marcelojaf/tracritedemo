using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TracriteDemoRESTService.Models;

namespace TracriteDemoRESTService.Services
{
    public class TracriteDemoService : ITracriteDemoService
    {
        private readonly ITracriteDemoRepository _repository;

        public TracriteDemoService(ITracriteDemoRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }


        public bool DoesCustomerExist(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(id);
            }

            return _repository.DoesCustomerExist(id);
        }

        public Customer Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            return _repository.Find(id);
        }

        public IEnumerable<Customer> GetData()
        {
            return _repository.All;
        }

        public void InsertData(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            _repository.Insert(customer);
        }

        public void UpdateData(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            _repository.Update(customer);
        }
    }
}