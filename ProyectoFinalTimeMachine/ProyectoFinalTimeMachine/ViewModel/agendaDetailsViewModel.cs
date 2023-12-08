using System;
using ProyectoFinalTimeMachine.Model;

namespace ProyectoFinalTimeMachine.ViewModel
{
    public class agendaDetailsViewModel
    {
        public string Tarea { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; } // Cambiado a DateTime
        public TimeSpan Hora { get; set; } // Cambiado a TimeSpan
        public bool IsCompleted { get; set; }

        public agendaDetailsViewModel()
        {

        }

        public void SetTarea(TimeMachineModel task)
        {
            Tarea = task.Tarea;
            Descripcion = task.Descripcion;
            Fecha = task.Fecha; // Ahora es del tipo DateTime
            Hora = task.Hora; // Ahora es del tipo TimeSpan
            IsCompleted = task.IsCompleted;
        }
    }
}
