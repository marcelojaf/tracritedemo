using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;
using Xamarin.Forms;

namespace TracriteDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    var customer = new Customer();
                    var addCustomerPage = new AddCustomerPage();
                    addCustomerPage.BindingContext = customer;
                    Navigation.PushAsync(addCustomerPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            {
                tbi = new ToolbarItem("+", "plus", () =>
                  {
                      var customer = new Customer();
                      var addCustomerPage = new AddCustomerPage();
                      addCustomerPage.BindingContext = customer;
                      Navigation.PushAsync(addCustomerPage);
                  }, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
            {
                tbi = new ToolbarItem("Add", "add.png", () =>
                {
                    var customer = new Customer();
                    var addCustomerPage = new AddCustomerPage();
                    addCustomerPage.BindingContext = customer;
                    Navigation.PushAsync(addCustomerPage);
                }, 0, 0);
            }

            ToolbarItems.Add(tbi);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((App)App.Current).ResumeAtCustomerId = -1;
            listView.ItemsSource = App.Database.getCustomers();
        }

        void listItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var customer = (Customer)e.SelectedItem;
            var addCustomerPage = new AddCustomerPage();
            addCustomerPage.BindingContext = customer;

            ((App)App.Current).ResumeAtCustomerId = customer.ID;
            Debug.WriteLine("setting ResumeAtCustomerId = " + customer.ID);

            Navigation.PushAsync(addCustomerPage);
        }
    }
}
