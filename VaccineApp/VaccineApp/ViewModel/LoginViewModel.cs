using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using VaccineApp.View;
using System.ComponentModel;
using VaccineApp.Persistency;

namespace VaccineApp.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties
        //Navigation prop
        private INavigation navigation;

        //Command prop til navigation knap
        public ICommand NavToRegister { get; set; }
        public ICommand LoginCommand { get; set; }

        public Webservice services { get; set; }


        private String _email;
        public String Email
        {
            get { return _email; }
            set { _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private String _password;
        public String Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }



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

            this.services = new Webservice();
            this.LoginCommand = new Command(async() => await LoginUser());

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

        private async Task LoginUser()
        {

            if(await CheckLoginCredentials() == true)
            {
                FrontPageMaster fpm = new FrontPageMaster();
                fpm.Master = new FrontPageMasterMenu();
                fpm.Detail = new NavigationPage(new FrontPageDetail())
                {
                    BarBackgroundColor = Color.FromHex("#016A6F"),
                };

                Application.Current.MainPage = fpm;

                ResponseMessage = (String)Application.Current.Properties["api_key"];
                ResponseColor = "#F56161";


            }
        }

        private async Task<bool> CheckLoginCredentials()
        {
                if ((String.IsNullOrWhiteSpace(Email)) || (String.IsNullOrWhiteSpace(Password)))
                {
                    ResponseColor = "#F56161";
                    ResponseMessage = "Du skal udfylde begge felter";
                    return false;
                }

                String WSLogin = await services.Login(Email.Trim(), Password);

                //Dette kan eventuelt blive lavet til en switch i stedet.
                if ((WSLogin != null) && (WSLogin != "false"))
                {
                    if(!Application.Current.Properties.ContainsKey("api_key")) {
                        Application.Current.Properties["api_key"] = WSLogin;
                    }
                    ResponseMessage = "";
                    return true;
                }

                if (WSLogin == "false")
                {
                    ResponseColor = "#F56161";
                    ResponseMessage = "Email eller kodeord er forkert";
                    return false;
                }

            ResponseMessage = "Server error!";
            return false;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {   
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
