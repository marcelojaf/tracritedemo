using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;
using Xamarin.Forms;

namespace TracriteDemo
{
    public partial class AddCustomerPage : ContentPage
    {
        public AddCustomerPage()
        {
            InitializeComponent();
        }

        private void btnInsertCustomer_click(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer();
            newCustomer.FirstName = txtFirstName.Text;
            newCustomer.LastName = txtLastName.Text;
            newCustomer.CityOfBirth = txtCityOfBirth.Text;
            newCustomer.CountryOfBirth = txtCountryOfBirth.Text;
        }
    }
}
