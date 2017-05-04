using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineApp.View;
using Xamarin.Forms;

namespace VaccineApp.Services
{
    class NavigationService
    {
        //Enum for de pages man kan navigere til - skal være identisk med navn på .xaml filen.
        public enum AvailablePages
        {
            AddChildPage,
            MainPage,
        }

        public async Task GotoPageAsync(AvailablePages page)
        {
            MasterDetailPage mdPage = (MasterDetailPage)Application.Current.MainPage;

            switch(page)
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
                default:
                    await App.Current.MainPage.DisplayAlert("Error", "Noget gik galt", "ok");
                        break;
            }
        }

    }
}
