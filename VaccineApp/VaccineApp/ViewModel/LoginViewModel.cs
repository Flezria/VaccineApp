using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using VaccineApp.View;

namespace VaccineApp.ViewModel
{
    class LoginViewModel
    {
        #region Properties
        //Navigation prop
        private INavigation navigation;
        //Command prop til navigation knap
        public ICommand NavToRegister { get; set; }

        #endregion

        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            this.NavToRegister = new Command(async() => await NavigateToRegister());
        }

        private async Task NavigateToRegister()
        {
            await navigation.PushAsync(new RegisterPage());
        }
    }
}
