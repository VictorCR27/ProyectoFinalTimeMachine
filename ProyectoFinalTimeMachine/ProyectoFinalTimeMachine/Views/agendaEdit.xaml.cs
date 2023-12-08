using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalTimeMachine.Model;
using ProyectoFinalTimeMachine.ViewModel;

namespace ProyectoFinalTimeMachine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class agendaEdit : ContentPage
    {

        TimeMachineRepo tareaRepository = new TimeMachineRepo();

        public agendaEdit(TimeMachineModel task)
        {
            InitializeComponent();
            BindingContext = new agendaEditViewModel(task);
        }
    }
}