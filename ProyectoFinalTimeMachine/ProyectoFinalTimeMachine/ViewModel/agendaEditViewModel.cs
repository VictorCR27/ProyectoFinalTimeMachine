using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ProyectoFinalTimeMachine.Model;

namespace ProyectoFinalTimeMachine.ViewModel
{
    public class agendaEditViewModel
    {
        private readonly TimeMachineRepo _tareaRepository = new TimeMachineRepo();

        public TimeMachineModel Task { get; set; }
        public Command CommandUpdate { get; set; }

        public agendaEditViewModel(TimeMachineModel task)
        {
            Task = task;
            CommandUpdate = new Command(UpdateTarea);
        }

        private async void UpdateTarea()
        {


            bool isUpdated = await _tareaRepository.Update(Task);

            if (isUpdated)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
                UserDialogs.Instance.Alert("Tarea actualizada", "Importante", "Ok");
            }
            else
            {
                UserDialogs.Instance.Alert("Tarea no actualizada", "Importante", "Ok");
            }
        }
    }
}
