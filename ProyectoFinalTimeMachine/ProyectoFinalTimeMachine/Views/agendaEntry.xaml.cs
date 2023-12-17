using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalTimeMachine.Model;
using ProyectoFinalTimeMachine.ViewModel;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;

namespace ProyectoFinalTimeMachine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class agendaEntry : ContentPage
    {
        public agendaEntry()
        {
            InitializeComponent();

            string userId = Preferences.Get("MyFirebaseUserId", ""); 

            this.BindingContext = new agendaEntryViewModel(userId);
        }
    }
}