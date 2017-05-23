using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineApp.View;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

namespace VaccineApp.Services
{
    class NavigationService
    {

        private MasterDetailPage mdPage { get; set; }
        //Enum for de pages man kan navigere til - skal være identisk med navn på .xaml filen.
        public enum AvailablePages
        {
            AddChildPage,
            MainPage,
            VaccineInfoPage,
            SettingsPage,
        }

        public NavigationService()
        {
        }

        public async Task GotoPageAsync(AvailablePages page)
        {
            mdPage = (MasterDetailPage)Application.Current.MainPage;

            switch (page)
            {
                case AvailablePages.AddChildPage:
                    mdPage.IsPresented = false;
                    var AddChild = new AddChildPage();
                    await mdPage.Detail.Navigation.PushAsync(AddChild);
                    break;
                case AvailablePages.MainPage:
                    Application.Current.Properties.Clear();
                    await PopToRoot();
                    Application.Current.MainPage = new NavigationPage(new MainPage())
                    {
                        BarBackgroundColor = Color.FromHex("#016A6F"),
                    };
                    break;
                case AvailablePages.VaccineInfoPage:
                    mdPage.IsPresented = false;
                    PopupPage vacInfoPopUp = new VaccineInfoPage();
                     await PopupNavigation.PushAsync(vacInfoPopUp);
                    break;
                case AvailablePages.SettingsPage:
                    mdPage.IsPresented = false;
                    var SettingsPage = new SettingsPage();
                    await mdPage.Detail.Navigation.PushAsync(SettingsPage);
                    break;
                default:
                    await App.Current.MainPage.DisplayAlert("Error", "Noget gik galt", "ok");
                        break;
            }
        }



        public async Task PopToRoot()
        {
            mdPage = (MasterDetailPage)Application.Current.MainPage;

            mdPage.IsPresented = false;
            await mdPage.Detail.Navigation.PopToRootAsync();
        }

    }
}
