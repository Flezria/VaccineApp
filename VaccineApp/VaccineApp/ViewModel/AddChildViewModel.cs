using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaccineApp.Persistency;
using VaccineApp.Model;
using Xamarin.Forms;
using System.ComponentModel;

namespace VaccineApp.ViewModel
{
    class AddChildViewModel : INotifyPropertyChanged
    {

        #region Properties

        private String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private DateTime _birthday = DateTime.Now;
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value.Date;
                OnPropertyChanged(nameof(Birthday));
            }
        }

        private String _gender;
        public String Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private String _errorMessage;
        public String ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private string _errorColor;
        public string ErrorColor
        {
            get { return _errorColor; }
            set
            {
                _errorColor = value;
                OnPropertyChanged(nameof(ErrorColor));
            }
        }
        public ICommand AddChildCommand { get; set; }
        public Webservice services { get; set; }

        private INavigation navigation;

        #endregion

        // Ctor
        public AddChildViewModel(INavigation navigation)
        {
            services = new Webservice();
            ErrorColor = "#E5E5E5";
            this.AddChildCommand = new Command(AddChild);
            this.navigation = navigation;
        }

        private async void AddChild()
        {

            var api_key = (string)Application.Current.Properties["api_key"];

            // temp placeholder for testing.
            UserChilds tempChild = new UserChilds(0, Name, Birthday, api_key, 17, 1, Gender);

            try
            {
                if (await services.AddChild(tempChild, api_key) == true)
                {
                    ErrorColor = "#4EC44E";
                    ErrorMessage = "Succces!";
                }
            }
            catch (Exception e)
            {
                ErrorColor = "#F56161";
                ErrorMessage = "Error : " + e;
            }

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
