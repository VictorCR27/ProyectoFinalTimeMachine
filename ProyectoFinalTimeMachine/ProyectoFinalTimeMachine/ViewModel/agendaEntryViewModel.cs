using Acr.UserDialogs;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProyectoFinalTimeMachine.Model;
using System;

namespace ProyectoFinalTimeMachine.ViewModel
{
    public class agendaEntryViewModel
    {
        TimeMachineRepo repository = new TimeMachineRepo();

        public TimeMachineModel task { get; set; }
        public Command commandSave { get; set; }


        public agendaEntryViewModel()
        {
            task = new TimeMachineModel();

            commandSave = new Command(saveTarea);
        }

        private async void saveTarea()
        {
            /*string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;*/

            if (Validar(task))
            {
                var isSaved = await repository.Save(task);

                if (isSaved)
                {
                    //await DisplayAlert("Información", "Registro ha sido almacenado", "Ok");
                    UserDialogs.Instance.Alert("Tarea ha sido almacenada", "Información", "Ok");
                    Clear();
                }
                else
                {
                    //await DisplayAlert("Erro", "Error al almacenar", "Ok");
                }

            }
        }

        private void Clear()
        {
            task.Tarea = string.Empty;
            task.Descripcion = string.Empty;
            task.Fecha = DateTime.Today; 
            task.Hora = TimeSpan.Zero; 
            task.IsCompleted = false;
        }

        private bool Validar(TimeMachineModel task)
        {
            bool respuesta = true;
            string mensaje = "";

            if (string.IsNullOrEmpty(task.Tarea))
            {
                mensaje += "El nombre es requerido.\n";
            }
            if (string.IsNullOrEmpty(task.Descripcion))
            {
                mensaje += "La descripción es requerida.\n";
            }
            if (task.Fecha == DateTime.MinValue) 
            {
                mensaje += "La fecha de vencimiento es requerida.\n";
            }
            if (task.Hora == TimeSpan.Zero) 
            {
                mensaje += "La hora es requerida.\n";
            }

            if (mensaje.Length > 0)
            {
                respuesta = false;
                UserDialogs.Instance.Alert(mensaje, "Alerta", "Ok");
            }

            return respuesta;
        }

    }
}
