using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaccineApp.View;
using Xamarin.Forms;
using VaccineApp.Common;

namespace VaccineApp.ViewModel
{
    class FrontPageMenuViewModel : INotifyPropertyChanged
    {

        #region Properties
        public INavigation Navigation { get; set; }
        public List<MasterMenuItem> HamburgerMenu { get; set; }

        private MasterMenuItem _selectedMenuItem;
        public MasterMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { _selectedMenuItem = value;
                OnPropertyChanged(nameof(SelectedMenuItem));
                //Navigate efter hvad der er selected
                DoNavigation();
            }
        }

        #endregion

        public FrontPageMenuViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
            HamburgerMenu = new List<MasterMenuItem>();
            AddMenuItems();

        }

        private void DoNavigation()
        {
            //For testing properties api_key
            if(SelectedMenuItem.Title == "Tilføj barn")
            {
                Application.Current.Properties.Clear();
            }
        }

        private async void AddChildNav()
        {
            await Navigation.PushAsync(new AddChildPage());
        }

        private void AddMenuItems()
        {
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Tilføj barn", Icon = "icon.png", TargetPage = typeof(AddChildPage)});
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Indstillinger", Icon = "icon.png", TargetPage = typeof(AddChildPage) });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "DerpTown", Icon = "icon.png", TargetPage = typeof(AddChildPage) });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Unknown", Icon = "icon.png", TargetPage = typeof(AddChildPage) });
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
