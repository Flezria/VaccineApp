using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace VaccineApp.ViewModel
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        #region Properties

        //Command props
        public ICommand RegisterCommand { get; set; }

        private INavigation navigation;

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value;
                OnPropertyChanged(nameof(Email));
                }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                }
        }

        private string _surName;
        public string SurName
        {
            get { return _surName; }
            set { _surName = value;
                OnPropertyChanged(nameof(SurName));
                }
        }

        private int _mobileNr;
        public int MobileNr
        {
            get { return _mobileNr; }
            set { _mobileNr = value;
                OnPropertyChanged(nameof(MobileNr));
                }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged(nameof(Password));
                }
        }

        private int _passwordConfirm ;
        public int PasswordConfirm
        {
            get { return _passwordConfirm ; }
            set { _passwordConfirm  = value;
                OnPropertyChanged(nameof(PasswordConfirm));
                }
        }

        private String _errorMessage;
        public String ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                }
        }

        #endregion

        public RegisterViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            this.RegisterCommand = new Command(RegisterUser);
        }

        private void RegisterUser()
        {

        }

        private void CheckUser()
        {

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
