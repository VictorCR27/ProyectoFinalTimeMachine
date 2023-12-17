using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProyectoFinalTimeMachine.Model
{
    public class TimeMachineModel : INotifyPropertyChanged
    {
        private string id;
        private string tarea;
        private string descripcion;
        private DateTime _fecha;
        private TimeSpan _hora;
        public bool isCompleted;
        

        //private string usuario;
        //private string clave;
        //private string nombre;
        //private string correo;
        //private string IdUsuario;

        private string idUsuario;
        public string IdUsuario
        {
            get { return idUsuario; }
            set
            {
                idUsuario = value;
                OnPropertyChanged();
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string Tarea
        {
            get => tarea;
            set
            {
                tarea = value;
                OnPropertyChanged();
            }
        }
        public string Descripcion
        {
            get => descripcion;
            set
            {
                descripcion = value;
                OnPropertyChanged();
            }
        }

        // Constructor que inicializa la fecha con la fecha actual al crear una nueva instancia
        public TimeMachineModel()
        {
            Fecha = DateTime.Now;
        }
        public DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                if (_fecha != value)
                {
                    _fecha = value;
                    OnPropertyChanged(nameof(Fecha));
                   
                }
            }
        }

        
        public TimeSpan Hora
        {
            get { return _hora; }
            set
            {
                if (_hora != value)
                {
                    _hora = value;
                    OnPropertyChanged(nameof(Hora));
                    
                }
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                OnPropertyChanged();
            }
        }


        // Propiedades del ViewModel Registro      

        //public string UsuarioId
        //{
        //    get => IdUsuario;
        //    set
        //    {
        //        IdUsuario = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public string Usuario
        //{
        //    get => usuario;
        //    set
        //    {
        //        usuario = value;
        //        OnPropertyChanged();
        //    }
        //}


        //public string Clave
        //{
        //    get => clave;
        //    set
        //    {
        //        clave = value;
        //        OnPropertyChanged();
        //    }
        //}


        //public string Nombre
        //{
        //    get => nombre;
        //    set
        //    {
        //        nombre = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public string Correo
        //{
        //    get => correo;
        //    set
        //    {
        //        correo = value;
        //        OnPropertyChanged();
        //    }
        //}

        /***** NOTIFICAR LA decimalERFAZ ****/
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
