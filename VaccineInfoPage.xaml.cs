using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaccineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VaccineInfoPage : PopupPage
    {
        public VaccineInfoPage()
        {
            InitializeComponent();

            BindingContext = new ViewModel.VaccineInfoViewModel();

        }
    }
}
