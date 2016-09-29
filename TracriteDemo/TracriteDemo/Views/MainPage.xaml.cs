using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;
using TracriteDemo.Views;
using Xamarin.Forms;

namespace TracriteDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            App.settingsSelected = new Models.Settings();

            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    var customer = new Customer();
                    var addCustomerPage = new AddCustomerPage(true);
                    addCustomerPage.BindingContext = customer;
                    Navigation.PushAsync(addCustomerPage);
                }, 0, 0);
                ToolbarItems.Add(tbi);
                tbi = new ToolbarItem("Set", null, () =>
                {
                    int settingsID = insertUpdateSettings();
                    var settings = new Settings();
                    settings = App.SettingsDatabase.getSettings(settingsID);
                    App.settingsSelected = settings;
                    var settingsPage = new SettingsPage();
                    settingsPage.BindingContext = settings;

                    ((App)App.Current).ResumeAtSettingsId = settings.ID;
                    Debug.WriteLine("setting ResumeAtSettingsId = " + settings.ID);

                    Navigation.PushAsync(settingsPage);
                }, 0, 0);
                ToolbarItems.Add(tbi);
            }
            if (Device.OS == TargetPlatform.Android)
            {
                tbi = new ToolbarItem("+", "plus", () =>
                  {
                      var customer = new Customer();
                      var addCustomerPage = new AddCustomerPage(true);
                      addCustomerPage.BindingContext = customer;
                      Navigation.PushAsync(addCustomerPage);
                  }, 0, 0);
                ToolbarItems.Add(tbi);

                tbi = new ToolbarItem("Set", "plus", () =>
                {
                    int settingsID = insertUpdateSettings();
                    var settings = new Settings();
                    settings = App.SettingsDatabase.getSettings(settingsID);
                    App.settingsSelected = settings;
                    var settingsPage = new SettingsPage();
                    settingsPage.BindingContext = settings;

                    ((App)App.Current).ResumeAtSettingsId = settings.ID;
                    Debug.WriteLine("setting ResumeAtSettingsId = " + settings.ID);

                    Navigation.PushAsync(settingsPage);
                }, 0, 0);
                ToolbarItems.Add(tbi);
            }
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
            {
                tbi = new ToolbarItem("Add", "add.png", () =>
                {
                    var customer = new Customer();
                    var addCustomerPage = new AddCustomerPage(true);
                    addCustomerPage.BindingContext = customer;
                    Navigation.PushAsync(addCustomerPage);
                }, 0, 0);
                ToolbarItems.Add(tbi);
                tbi = new ToolbarItem("Settings", "settings.png", () =>
                {
                    int settingsID = insertUpdateSettings();
                    var settings = new Settings();
                    settings = App.SettingsDatabase.getSettings(settingsID);
                    App.settingsSelected = settings;
                    var settingsPage = new SettingsPage();
                    settingsPage.BindingContext = settings;

                    ((App)App.Current).ResumeAtSettingsId = settings.ID;
                    Debug.WriteLine("setting ResumeAtSettingsId = " + settings.ID);

                    Navigation.PushAsync(settingsPage);
                }, 0, 0);
                ToolbarItems.Add(tbi);
            }

           
        }

        private int insertUpdateSettings()
        {
            return App.SettingsDatabase.insertSettings(new Settings
            {
                Server = "http://localhost:49290/api/todoitems{0}",
                User = "Xamarin",
                Password = "1234",
                LocalDatabase = true
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ((App)App.Current).ResumeAtCustomerId = -1;
            if (App.settingsSelected.LocalDatabase)
            {
                listView.ItemsSource = App.Database.getCustomers();
            }
            else
            {
                int settingsID = insertUpdateSettings();
                var settings = new Settings();
                settings = App.SettingsDatabase.getSettings(settingsID);
                App.settingsSelected = settings;
                listView.ItemsSource = await App.CustomerManager.GetTasksAsync();
            }
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
