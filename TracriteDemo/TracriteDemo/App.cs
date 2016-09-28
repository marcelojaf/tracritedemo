using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Data;
using Xamarin.Forms;

namespace TracriteDemo
{
    public class App : Application
    {
        static CustomerDB database;

        public App()
        {
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new MainPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
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
