﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaccineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrontPageMaster : MasterDetailPage
    {
        public FrontPageMaster()
        {
            InitializeComponent();

            BindingContext = new ViewModel.FrontPageViewModel();

        }
    }
}
