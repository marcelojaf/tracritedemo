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
            NavigationPage.SetHasNavigationBar(this, true);
        }

        private void btnInsertCustomer_click(object sender, EventArgs e)
        {
            var customer = (Customer)BindingContext;
            App.Database.insertCustomers(customer);
            this.Navigation.PopAsync();
        }
    }
}
