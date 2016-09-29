using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Data;
using TracriteDemo.Models;
using Xamarin.Forms;

namespace TracriteDemo
{
    public class App : Application
    {
        static CustomerDB database;
        static SettingsDB settingsDatabase;
        public static Settings settingsSelected;
        public static CustomerManager CustomerManager { get; private set; }

        public App()
        {
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new MainPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;

            int settingsID = insertUpdateSettings();
            var settings = new Settings();
            settings = App.SettingsDatabase.getSettings(settingsID);
            App.settingsSelected = settings;

            CustomerManager = new CustomerManager(new RestService());

            MainPage = nav;
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

        public static CustomerDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new CustomerDB();
                }
                return database;
            }
        }

        public int ResumeAtCustomerId { get; set; }

        public static SettingsDB SettingsDatabase
        {
            get
            {
                if (settingsDatabase == null)
                {
                    settingsDatabase = new SettingsDB();
                }
                return settingsDatabase;
            }
        }

        public int ResumeAtSettingsId { get; set; }

        protected override void OnStart()
        {
            Debug.WriteLine("OnStart");

            // always re-set when the app starts
            // users expect this (usually)
            //			Properties ["ResumeAtCustomerId"] = "";
            if (Properties.ContainsKey("ResumeAtCustomerId"))
            {
                var rati = Properties["ResumeAtCustomerId"].ToString();
                Debug.WriteLine("   rati=" + rati);
                if (!String.IsNullOrEmpty(rati))
                {
                    Debug.WriteLine("   rati = " + rati);
                    ResumeAtCustomerId = int.Parse(rati);

                    if (ResumeAtCustomerId >= 0)
                    {
                        var CustomerPage = new AddCustomerPage();
                        CustomerPage.BindingContext = Database.getCustomer(ResumeAtCustomerId);

                        MainPage.Navigation.PushAsync(
                            CustomerPage,
                            false); // no animation
                    }
                }
            }
        }
        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep saving ResumeAtTodoId = " + ResumeAtCustomerId);
            // the app should keep updating this value, to
            // keep the "state" in case of a sleep/resume
            Properties["ResumeAtTodoId"] = ResumeAtCustomerId;
        }
        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
            //			if (Properties.ContainsKey ("ResumeAtTodoId")) {
            //				var rati = Properties ["ResumeAtTodoId"].ToString();
            //				Debug.WriteLine ("   rati="+rati);
            //				if (!String.IsNullOrEmpty (rati)) {
            //					Debug.WriteLine ("   rati = " + rati);
            //					ResumeAtTodoId = int.Parse (rati);
            //
            //					if (ResumeAtTodoId >= 0) {
            //						var todoPage = new TodoItemPage ();
            //						todoPage.BindingContext = Database.GetItem (ResumeAtTodoId);
            //
            //						MainPage.Navigation.PushAsync (
            //							todoPage,
            //							false); // no animation
            //					}
            //				}
            //			}
        }
    }
}
