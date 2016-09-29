using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TracriteDemoRESTService.Attributes;
using TracriteDemoRESTService.Models;
using TracriteDemoRESTService.Services;

namespace TracriteDemoRESTService.Controllers
{
    public class CustomersController : BaseApiController
    {
        static readonly ITracriteDemoService tracriteDemoService = new TracriteDemoService(new TracriteDemoRepository());

        [HttpGet]
        [BasicAuthentication(RequireSsl = false)]
        public HttpResponseMessage Get()
        {
            return base.BuildSuccessResult(HttpStatusCode.OK, tracriteDemoService.GetData());
        }

        [HttpPost]
        [BasicAuthentication(RequireSsl = false)]
        public HttpResponseMessage Create(Customer customer)
        {
            try
            {
                if (customer == null ||
                    string.IsNullOrWhiteSpace(customer.FullName) ||
                    string.IsNullOrWhiteSpace(customer.CityOfBirth) ||
                    string.IsNullOrWhiteSpace(customer.CountryOfBirth))
                {
                    return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CustomerFirstNameAndLastNameRequired.ToString());
                }

                // Determine if the ID already exists
                var itemExists = tracriteDemoService.DoesCustomerExist(customer.ID);
                if (itemExists)
                {
                    return base.BuildErrorResult(HttpStatusCode.Conflict, ErrorCode.CustomerIDInUse.ToString());
                }
                tracriteDemoService.InsertData(customer);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CouldNotCreateItem.ToString());
            }

            return base.BuildSuccessResult(HttpStatusCode.Created);
        }

        [HttpPut]
        [BasicAuthentication(RequireSsl = false)]
        public HttpResponseMessage Edit(string id, Customer customer)
        {
            try
            {
                if (customer == null ||
                    string.IsNullOrWhiteSpace(customer.FullName) ||
                    string.IsNullOrWhiteSpace(customer.CityOfBirth) ||
                    string.IsNullOrWhiteSpace(customer.CountryOfBirth))
                {
                    return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CustomerFirstNameAndLastNameRequired.ToString());
                }

                var tracriteDemoItem = tracriteDemoService.Find(id);
                if (tracriteDemoItem != null)
                {
                    tracriteDemoService.UpdateData(customer);
                }
                else
                {
                    return base.BuildErrorResult(HttpStatusCode.NotFound, ErrorCode.RecordNotFound.ToString());
                }
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CouldNotUpdateItem.ToString());
            }

            return base.BuildSuccessResult(HttpStatusCode.NoContent);
        }
    }
}