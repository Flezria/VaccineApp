using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
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
    class VaccineInfoViewModel : INotifyPropertyChanged
    {

        #region Properties
        public ICommand ClosePopupCommand { get; set; }
        #endregion

        public VaccineInfoViewModel()
        {
            ClosePopupCommand = new Command(ClosePopup);
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
