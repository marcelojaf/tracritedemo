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
        bool isNewCustomer;

        public AddCustomerPage(bool isNew = false)
        {
            InitializeComponent();
            isNewCustomer = isNew;
            NavigationPage.SetHasNavigationBar(this, true);
        }

        private void btnInsertCustomer_click(object sender, EventArgs e)
        {
            if ((txtFirstName.Text == "") || (txtLastName.Text == ""))
            {
                DisplayAlert("Alert", "First Name and Last Name cannot be empty", "OK");
                if (txtFirstName.Text == "") txtFirstName.Focus();
                else txtLastName.Focus();
            }
            else
            {
                if (App.settingsSelected.LocalDatabase)
                {
                    var customer = (Customer)BindingContext;
                    App.Database.insertCustomers(customer);
                    this.Navigation.PopAsync();
                }
                else
                {
                    var customer = (Customer)BindingContext;
                    App.CustomerManager.InsertCustomerAsync(customer, isNewCustomer);
                    this.Navigation.PopAsync();
                }
            }
        }
    }
}
