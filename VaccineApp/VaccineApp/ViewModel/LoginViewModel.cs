using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using VaccineApp.View;
using System.ComponentModel;

namespace VaccineApp.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties
        //Navigation prop
        private INavigation navigation;

        //Command prop til navigation knap
        public ICommand NavToRegister { get; set; }

        private String _responseMessage;
        public String ResponseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
                }
        }

        private String _responseColor;
        public String ResponseColor
        {
            get { return _responseColor; }
            set { _responseColor = value;
                OnPropertyChanged(nameof(ResponseColor));
            }
        }


        #endregion

        public LoginViewModel(INavigation navigation)
        {
            ResponseColor = "#E5E5E5";
            this.navigation = navigation;
            this.NavToRegister = new Command(async() => await NavigateToRegister());

            //Check hvis vi kommer fra RegisterPage og har lavet en ny bruger
            if (RegisterViewModel.IsRegisterComplete == true)
            {
                ResponseColor = "#4EC44E";
                ResponseMessage = "Oprettelse fuldført! Du kan nu logge ind";
                RegisterViewModel.IsRegisterComplete = false;
            }
        }

        private async Task NavigateToRegister()
        {
            await navigation.PushAsync(new RegisterPage());

        }



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
