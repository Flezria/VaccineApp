using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using VaccineApp.Model;
using VaccineApp.Persistency;
using Xamarin.Forms;

namespace VaccineApp.ViewModel
{
    class HistorikViewModel : INotifyPropertyChanged
    {
        #region Properties

        public ObservableCollection<Vaccinations> VacHistorik { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        public Webservice Services { get; set; }

        private String _selectedChildName;

        public String SelectedChildName
        {
            get { return _selectedChildName; }
            set
            {
                _selectedChildName = value; 
                OnPropertyChanged(nameof(SelectedChildName));
            }
        }
        #endregion
        public HistorikViewModel()
        {

            ClosePopupCommand = new Command(ClosePopup);
            Services = new Webservice();

            LoadHistorik();
        }

        private async void LoadHistorik()
        {
            if (FrontPageViewModel.CurrentChild != null)
            {
                VacHistorik = new ObservableCollection<Vaccinations>(await Services.GetVacHistorik((String)Application.Current.Properties["api_key"], FrontPageViewModel.CurrentChild.program_id, FrontPageViewModel.CurrentChild.child_id));
                if (VacHistorik.Count == 0)
                {
                    SelectedChildName = FrontPageViewModel.CurrentChild.name + " har ikke fuldført nogle vacciner";
                }
                else
                {
                    SelectedChildName = "Historik for " + FrontPageViewModel.CurrentChild.name;
                }
            }
        }

        private async void ClosePopup()
        {
            await PopupNavigation.PopAllAsync();
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
