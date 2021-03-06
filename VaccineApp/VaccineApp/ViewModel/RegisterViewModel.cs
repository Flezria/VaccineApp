﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaccineApp.Persistency;
using Xamarin.Forms;
using VaccineApp.Model;

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

        private String _mobileNr;
        public String MobileNr
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

        private string _passwordConfirm ;
        public string PasswordConfirm
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

        public static bool IsRegisterComplete { get; set; }

        public Webservice services { get; set; }

        #endregion

        public RegisterViewModel(INavigation navigation)
        {
            services = new Webservice();
            this.navigation = navigation;
            this.RegisterCommand = new Command(RegisterUser);
        }

        private async void RegisterUser()
        {
            if(await CheckUser() == true)
            {
                Users tempUser = new Users(0, Password, int.Parse(MobileNr.Trim()), FirstName.Trim(), SurName.Trim(), Email.Trim(), null);

                if(await services.AddUser(tempUser) == true)
                {
                    IsRegisterComplete = true;
                    await navigation.PopToRootAsync();

                }
            }
        }

        private async Task<bool> CheckUser()
        {
            if((String.IsNullOrWhiteSpace(Email)) || (String.IsNullOrWhiteSpace(FirstName)) || (String.IsNullOrWhiteSpace(SurName)) || (String.IsNullOrWhiteSpace(MobileNr)) || (String.IsNullOrWhiteSpace(Password)) || (String.IsNullOrWhiteSpace(PasswordConfirm))) 
            {
                ErrorMessage = "Du skal udfylde alle felter";
                return false;
            }

            if(Password != PasswordConfirm)
            {
                ErrorMessage = "Kodeord er ikke ens";
                return false;
            }

            if(!Email.Contains("@"))
            {
                ErrorMessage = "Du skal skrive en rigtig email";
                return false;
            }

            if (await services.CheckIfEmailIsTaken(Email.Trim()) == true)
            {
                ErrorMessage = "Denne email er allerede i brug";
                return false;
            }
            ErrorMessage = "";
            return true;
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
