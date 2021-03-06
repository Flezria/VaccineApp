﻿using System;
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
using VaccineApp.Persistency;
using Xamarin.Forms.Xaml;

namespace VaccineApp.ViewModel
{
    class FrontPageViewModel : INotifyPropertyChanged
    {

        #region Properties

        public List<MasterMenuItem> HamburgerMenu { get; set; }
        public NavigationService NavService { get; set; }
        public MasterMenuItem Filler { get; set; }
        public Webservice Services { get; set; }
        public ICommand OnItemTapCommand { get; set; }
        public static UserChilds CurrentChild { get; set; }

        public bool MessageCheck { get; set; }
        public int ChildListCount { get; set; }

        private string _selectedChildsName;
        public string SelectedChildsName
        {
            get { return _selectedChildsName; }
            set { _selectedChildsName = value;
                OnPropertyChanged(nameof(SelectedChildsName));                
            }
        }

        private MasterMenuItem _selectedMenuItem;
        public MasterMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { _selectedMenuItem = value;
                OnPropertyChanged(nameof(SelectedMenuItem));

                if(SelectedMenuItem != null)
                    DoNavigation();
            }
        }

        private ObservableCollection<UserChilds> _childList;
        public ObservableCollection<UserChilds> ChildList
        {
            get { return _childList; }
            set { _childList = value;
                OnPropertyChanged(nameof(ChildList));
            }
        }
        
        private int _selectedIndexChild;
        public int SelectedIndexChild
        {
            get { return _selectedIndexChild; }
            set { _selectedIndexChild = value;
                OnPropertyChanged(nameof(SelectedIndexChild));
                SelectedIndexMethod();
            }
        }

        private ObservableCollection<Vaccinations> _vacProgramList;
        public ObservableCollection<Vaccinations> VacProgramList
        {
            get { return _vacProgramList; }
            set
            {
                _vacProgramList = value;
                OnPropertyChanged(nameof(VacProgramList));
            }
        }

        public static Vaccinations _vacItemSelected;
        public Vaccinations VacItemSelected
        {
            get { return _vacItemSelected; }
            set { _vacItemSelected = value;
                OnPropertyChanged(nameof(VacItemSelected));
                ItemSelectedMethod();
            }
        }


        #endregion


        public FrontPageViewModel()
        {
            OnItemTapCommand = new Command<Vaccinations>(OnItemTapMethod);

            SelectedMenuItem = new MasterMenuItem();

            //Der skal findes en bedre løsning end dette Filler object.
            Filler = new MasterMenuItem() { Title = "Fill", Icon = "icon.png", TargetPage = "Fill"};

            NavService = new NavigationService();

            Services = new Webservice();

            HamburgerMenu = new List<MasterMenuItem>();
            AddMenuItems();

            LoadList();

            MessagingCenter.Subscribe<AddChildViewModel>(this, "update", (sender) =>
            {
                LoadList();
            });

            MessagingCenter.Subscribe<SettingsViewModel>(this, "delete", (sender) =>
            {
                LoadList();
                DeleteLastEntry();
            });
        }

        #region Setter Methods

        //Method til SelectedIndex setter
        private void SelectedIndexMethod()
        {

            if ((ChildList.Count != 0) && (SelectedIndexChild < 0) || (ChildList.Count - 1 < SelectedIndexChild))
            {
                    SelectedIndexChild = 0;
            }

            if ((ChildList.Count != 0) && (SelectedIndexChild != -1))
            {
                LoadDisplayVacProgram();
                CurrentChild = ChildList[SelectedIndexChild];
                SelectedChildsName = "Vaccine program for " + CurrentChild.name;
            }
        }
        //ItemSelect method
        private async void ItemSelectedMethod()
        {
            if(VacItemSelected != null) {
            await NavService.GotoPageAsync(NavigationService.AvailablePages.VaccineInfoPage);
            VacItemSelected = null;
            }
        }

        #endregion

        #region Load Lists Methods
        private async void LoadList()
        {
            if (await Services.CheckForChild((String)Application.Current.Properties["api_key"]) == true)
            {
                ChildList = await Services.GetChild((String)Application.Current.Properties["api_key"]);
                SelectedIndexMethod();
            }
        }

        private async void DeleteLastEntry()
        {
            if (await Services.CheckForChild((String)Application.Current.Properties["api_key"]) == false)
            {
                if (ChildList.Count == 1)
                {
                    ChildList.Clear();
                    VacProgramList.Clear();
                    SelectedChildsName = "Opret et barn for at se vaccineprogram";
                }
            }
        }

        private async void LoadDisplayVacProgram()
        {
            //VacProgramList = new ObservableCollection<Vaccinations>(await Services.GetVacProgram((String)Application.Current.Properties["api_key"], ChildList[SelectedIndexChild].program_id, ChildList[SelectedIndexChild].child_id));

            VacProgramList = new ObservableCollection<Vaccinations>(await Services.GetVacProgram((String)Application.Current.Properties["api_key"], ChildList[SelectedIndexChild].program_id, ChildList[SelectedIndexChild].child_id));
        }

        #endregion

        private async void OnItemTapMethod(Vaccinations VacForHistorik)
        {
            if (VacForHistorik != null)
            {
                if (await Services.UpdateVacAsDone((String) Application.Current.Properties["api_key"],
                    VacForHistorik.vaccine_id, CurrentChild.child_id))
                {
                    LoadDisplayVacProgram();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Noget gik galt", "OK");

                }
            }
        }

        #region Navigation
        private async void DoNavigation()
        {
            switch(SelectedMenuItem.TargetPage)
            {
                case "AddChildPage":
                    await NavService.GotoPageAsync(NavigationService.AvailablePages.AddChildPage);
                    SelectedMenuItem = Filler;
                    break;
                case "VaccineInfoPage":
                    await NavService.GotoPageAsync(NavigationService.AvailablePages.VaccineInfoPage);
                    SelectedMenuItem = Filler;
                    break;
                case "MainPage":
                    await NavService.GotoPageAsync(NavigationService.AvailablePages.MainPage);
                    break;
                case "SettingsPage":
                    await NavService.GotoPageAsync(NavigationService.AvailablePages.SettingsPage);
                    SelectedMenuItem = Filler;
                    break;
                case "HistorikPage":
                    await NavService.GotoPageAsync(NavigationService.AvailablePages.HistorikPage);
                    SelectedMenuItem = Filler;
                    break;

            }
        }

        private void AddMenuItems()
        {
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Opret barn", Icon = "addChild.png", TargetPage = "AddChildPage" });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Indstillinger", Icon = "settings.png", TargetPage = "SettingsPage" });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Historik", Icon = "historik.png", TargetPage = "HistorikPage" });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Logud", Icon = "logud.png", TargetPage = "MainPage" });
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
