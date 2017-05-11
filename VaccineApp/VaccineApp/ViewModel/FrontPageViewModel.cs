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

namespace VaccineApp.ViewModel
{
    class FrontPageViewModel : INotifyPropertyChanged
    {

        #region Properties

        public List<MasterMenuItem> HamburgerMenu { get; set; }
        public NavigationService NavService { get; set; }
        public MasterMenuItem Filler { get; set; }
        public Webservice Services { get; set; }

        public bool MessageCheck = false;
        public int ChildListCount;



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

             if(MessageCheck != true & ChildList != null)
                {
                    SelectedIndexChild = 0;
                };
          
            }
        }
        
        private int _selectedIndexChild;
        public int SelectedIndexChild
        {
            get { return _selectedIndexChild; }
            set { _selectedIndexChild = value;
                OnPropertyChanged(nameof(SelectedIndexChild));

                if (MessageCheck == true)
                {   
                    MessageCheck = false;
                    SelectedIndexChild = ChildListCount;   
                };

            }
        }


        #endregion

        private ObservableCollection<Vaccinations> _testList;

        public ObservableCollection<Vaccinations> TestList
        {
            get { return _testList; }
            set { _testList = value;
                OnPropertyChanged(nameof(TestList));
            }
        }


        public FrontPageViewModel()
        {
            SelectedMenuItem = new MasterMenuItem();

            //Der skal findes en bedre løsning end dette Filler object.
            Filler = new MasterMenuItem() { Title = "Fill", Icon = "icon.png", TargetPage = "Fill"};

            NavService = new NavigationService();

            Services = new Webservice();

            HamburgerMenu = new List<MasterMenuItem>();
            AddMenuItems();

            TestListLoad();

            LoadList();

            MessagingCenter.Subscribe<AddChildViewModel>(this, "update", (sender) => {
                LoadList();
                MessageCheck = true;
                ChildListCount = ChildList.Count() - 1;
            });
        }

        

        public async void LoadList()
        {
            ChildList = await Services.GetChild((String)Application.Current.Properties["api_key"]);
        }

        public void TestListLoad()
        {
            TestList = new ObservableCollection<Vaccinations>();
            TestList.Add(new Vaccinations(1, "Vaccine-1", 4, 1));
            TestList.Add(new Vaccinations(2, "Vaccine-2", 8, 1));
            TestList.Add(new Vaccinations(3, "Vaccine-3", 12, 1));
            TestList.Add(new Vaccinations(4, "Vaccine-4", 16, 1));
            TestList.Add(new Vaccinations(4, "Vaccine-5", 20, 1));
            TestList.Add(new Vaccinations(4, "Vaccine-6", 24, 1));


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
                case "MainPage":
                    await NavService.GotoPageAsync(NavigationService.AvailablePages.MainPage);
                    SelectedMenuItem = Filler;
                    break;
            }

            //For testing properties api_key
            if (SelectedMenuItem.Title == "Unknown")
            {
                Application.Current.Properties.Clear();
            }
        }

        private void AddMenuItems()
        {
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Opret barn", Icon = "icon.png", TargetPage = "AddChildPage" });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Indstillinger", Icon = "icon.png", TargetPage = "MainPage" });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Unknown", Icon = "icon.png", TargetPage = "Fill" });
            HamburgerMenu.Add(new MasterMenuItem() { Title = "Unknown", Icon = "icon.png", TargetPage = "Fill" });
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
