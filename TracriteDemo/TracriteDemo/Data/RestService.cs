using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;

namespace TracriteDemo.Data
{
    public class RestService : IRestService
    {
        HttpClient client;

        public List<Customer> Customers { get; private set; }

        public RestService()
        {
            var authData = string.Format("{0}:{1}", App.settingsSelected.User, App.settingsSelected.Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<Customer>> RefreshDataAsync()
        {
            Customers = new List<Models.Customer>();
            var uri = new Uri(string.Format(App.settingsSelected.Server, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<Customer>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Customers;
        }

        public async Task InsertCustomerAsync(Customer customer, bool isNewCustomer = false)
        {
            var uri = new Uri(string.Format(App.settingsSelected.Server, customer.ID));

            try
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewCustomer)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Customer successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
    }
}
