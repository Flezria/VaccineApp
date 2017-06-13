using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaccineApp.View;
using Xamarin.Forms;
using VaccineApp.Model;
using VaccineApp.Services;
using System.Collections.ObjectModel;
using Org.Apache.Http.Client.Protocol;
using VaccineApp.Persistency;

namespace VaccineApp.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        #region Properties
        public Webservice Services { get; set; }
        public int SelectedChildId { get; set; }
        public ICommand DeleteChildCommand { get; set; }
    

        private ObservableCollection<UserChilds> _childList;
        public ObservableCollection<UserChilds> ChildList
        {
            get { return _childList; }
            set
            {
                _childList = value;
                OnPropertyChanged(nameof(ChildList));
                
            }
        }

        private int _selectedIndexChild;
        public int SelectedIndexChild
        {
            get { return _selectedIndexChild; }
            set
            {
                _selectedIndexChild = value;
                OnPropertyChanged(nameof(SelectedIndexChild));
                SetChildListIndex();
            }
        }

        #endregion

        public SettingsViewModel()
        {
            Services = new Webservice();
            DeleteChildCommand = new Command(DeleteMethod);

            LoadInCtor();  
        }

        private async void DeleteMethod()
        {
            if ((SelectedIndexChild > -1) && (ChildList.Count != 0))
            {
                SelectedChildId = ChildList[SelectedIndexChild].child_id;

                if (await Services.DeleteChild(SelectedChildId, (String)Application.Current.Properties["api_key"]))
                {
                    await App.Current.MainPage.DisplayAlert("Done", "Barn slettet", "OK");
                    LoadList();
                    MessagingCenter.Send<SettingsViewModel>(this, "delete");
                }
            }
        }

        private void SetChildListIndex()
        {
            if ((ChildList.Count != 0) && (SelectedIndexChild < 0))
            {

                    SelectedIndexChild = 0;

            }
        }

        private async void LoadInCtor()
        {
            if (await Services.CheckForChild((String)Application.Current.Properties["api_key"]))
            {
                LoadList();
            }
        }

        private async void LoadList()
        {
            ChildList = await Services.GetChild((String)Application.Current.Properties["api_key"]);
            SetChildListIndex();
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
