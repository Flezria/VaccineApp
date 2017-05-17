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

namespace VaccineApp.ViewModel
{
    class VaccineInfoViewModel : INotifyPropertyChanged
    {

        #region Properties
        public ICommand ClosePopupCommand { get; set; }

        private Vaccinations _derp;

        public Vaccinations Derp
        {
            get { return _derp; }
            set { _derp = value;
                OnPropertyChanged(nameof(Derp));
            }
        }

        #endregion

        public VaccineInfoViewModel()
        {
            ClosePopupCommand = new Command(ClosePopup);

            Derp = FrontPageViewModel._vacItemSelected;
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
