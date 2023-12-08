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
    public partial class agendaDetails : ContentPage
    {
        private readonly agendaDetailsViewModel viewModel;
        public agendaDetails(TimeMachineModel task)
        {
            InitializeComponent();
            viewModel = new agendaDetailsViewModel();
            viewModel.SetTarea(task);
            BindingContext = viewModel;
        }
    }
}