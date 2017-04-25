using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VaccineApp.ViewModel
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _surName;
        public string SurName
        {
            get { return _surName; }
            set { _surName = value; }
        }

        private int _mobileNr;
        public int MobileNr
        {
            get { return _mobileNr; }
            set { _mobileNr = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private int _passwordConfirm ;
        public int PasswordConfirm
        {
            get { return _passwordConfirm ; }
            set { _passwordConfirm  = value; }
        }

        private INavigation navigation;
          

        #endregion

        public RegisterViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
