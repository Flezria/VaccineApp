using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VaccineApp.Persistency;
using VaccineApp.View;
using VaccineApp.ViewModel;
using Xamarin.Forms;

namespace VaccineApp
{
    public partial class App : Application
    {
        private Webservice Services { get; set; }

        public App()
        {
            InitializeComponent();
            Services = new Webservice();
        }

        private async void StartingPoint(MasterDetailPage fpm)
        {
            if (await Services.CheckForChild((String)Application.Current.Properties["api_key"]) == false)
            {
                await fpm.Detail.Navigation.PushAsync(new AddChildPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if(Application.Current.Properties.ContainsKey("api_key"))
            {

                FrontPageMaster fpm = new FrontPageMaster();
                fpm.Master = new FrontPageMasterMenu();
                fpm.Detail = new NavigationPage(new FrontPageDetail())
                {
                    BarBackgroundColor = Color.FromHex("#016A6F"),
                };

                MainPage = fpm;

                StartingPoint(fpm);

            }
            else
            {
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.FromHex("#016A6F"),
                };
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

            if (Application.Current.Properties.ContainsKey("api_key"))
            {

                FrontPageMaster fpm = new FrontPageMaster();
                fpm.Master = new FrontPageMasterMenu();
                fpm.Detail = new NavigationPage(new FrontPageDetail())
                {
                    BarBackgroundColor = Color.FromHex("#016A6F"),
                };

                MainPage = fpm;

                StartingPoint(fpm);

            }
            else
            {
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.FromHex("#016A6F"),
                };
            }
        }
    }
}
