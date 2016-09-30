using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TracriteDemo.Models;
using Xamarin.Forms;

namespace TracriteDemo.Views
{
    public partial class SettingsPage : ContentPage
    {
        HttpClient client;

        public SettingsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);

            var authData = string.Format("{0}:{1}", App.settingsSelected.User, App.settingsSelected.Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        private void btnUpdateSettings_click(object sender, EventArgs e)
        {
            var settings = (Settings)BindingContext;
            App.SettingsDatabase.insertSettings(settings);
            this.Navigation.PopAsync();
        }

        private async void localDatabaseSwitch_toggle(object sender, EventArgs e)
        {
            btnUpdateSettings.IsEnabled = false;
            if (!localDatabaseSwitch.IsToggled)
            {
                var uri = new Uri(string.Format(App.settingsSelected.Server, string.Empty));
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    else
                    {
                        localDatabaseSwitch.IsToggled = true;
                        await DisplayAlert("Alert", "The server is not responding: " + response.StatusCode.ToString(), "OK");
                    }
                }
                catch (Exception erro)
                {
                    localDatabaseSwitch.IsToggled = true;
                    await DisplayAlert("Alert", "The server is not responding: " + erro.Message, "OK");
                }
                
            }
            btnUpdateSettings.IsEnabled = true;
        }
    }
}
