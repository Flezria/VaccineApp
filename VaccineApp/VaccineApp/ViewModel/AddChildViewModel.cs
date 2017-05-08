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
using System.Diagnostics;
using VaccineApp.Services;

namespace VaccineApp.ViewModel
{
    class AddChildViewModel : INotifyPropertyChanged
    {

        #region Properties

        public ICommand AddChildCommand { get; set; }
        private Webservice Services { get; set; }
        private NavigationService NavService { get; set; }

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
        #endregion

        // Ctor
        public AddChildViewModel()
        {
            Services = new Webservice();
            NavService = new NavigationService();
            ErrorColor = "#E5E5E5";
            this.AddChildCommand = new Command(AddChild);
        }

        private async void AddChild()
        {

            var api_key = (String)Application.Current.Properties["api_key"];

            // temp placeholder for testing.
            UserChilds tempChild = new UserChilds(0, Name, Birthday, api_key, 17, 1, Gender);

            try
            {
                if (await Services.AddChild(tempChild, api_key) == true)
                {
                    MessagingCenter.Send<AddChildViewModel>(this, "update");
                    await NavService.PopToRoot();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
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
