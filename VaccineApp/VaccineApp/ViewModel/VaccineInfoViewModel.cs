using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaccineApp.Model;
using Xamarin.Forms;
using VaccineApp.Persistency;

namespace VaccineApp.ViewModel
{
    class VaccineInfoViewModel : INotifyPropertyChanged
    {

        #region Properties
        public ICommand ClosePopupCommand { get; set; }
        public Webservice Services { get; set; }


        private VaccineInfo _vacInfo;
        public VaccineInfo VacInfo
        {
            get { return _vacInfo; }
            set { _vacInfo = value;
                OnPropertyChanged(nameof(VacInfo));
            }
        }



        #endregion


        public VaccineInfoViewModel()
        {
            ClosePopupCommand = new Command(ClosePopup);

            Services = new Webservice();

            GetVacInfo();  
        }

        private async void GetVacInfo()
        {
            if (FrontPageViewModel._vacItemSelected != null)
            {
                VacInfo = await Services.GetVacInfo((String)Application.Current.Properties["api_key"], FrontPageViewModel._vacItemSelected.vaccine_id);
            }
        }

        private void ClosePopup()
        {
            PopupNavigation.PopAllAsync();
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
