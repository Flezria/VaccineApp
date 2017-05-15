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
                    mdPage.IsPresented = false;
                    await mdPage.Detail.Navigation.PushAsync(new MainPage());
                    break;
                case AvailablePages.VaccineInfoPage:
                    mdPage.IsPresented = false;
                    PopupPage qrv = new VaccineInfoPage();
                     await PopupNavigation.PushAsync(qrv);
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
