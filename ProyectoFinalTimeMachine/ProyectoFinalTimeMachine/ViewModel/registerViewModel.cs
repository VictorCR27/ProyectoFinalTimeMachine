using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using ProyectoFinalTimeMachine.Model;
using ProyectoFinalTimeMachine.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace ProyectoFinalTimeMachine.ViewModel
{
    public class registerViewModel : BaseViewModel
    {
        //Atributos
        public string correo;
        public string contrasena;
        public string nombre;
        public string usuario;


        //Propiedades
        public string Correo
        {
            get { return this.correo; }
            set { SetValue(ref this.correo, value); }
        }

        public string Contraseña
        {
            get { return this.contrasena; }
            set { SetValue(ref this.contrasena, value); }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }

        public string Usuario
        {
            get { return this.usuario; }
            set { SetValue(ref this.usuario, value); }
        }



        //Comandos
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(RegisterMethod);
            }
        }


        //Metodos
        private async void RegisterMethod()
        {
            if (string.IsNullOrEmpty(this.correo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.contrasena))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            }

            string WebAPIkey = "AIzaSyCgAJJZC9CCVIFlvJ85RStZUUhSDr1t4hU";

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Correo.ToString(), Contraseña.ToString());
                string gettoken = auth.FirebaseToken;

                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

        }
    }

}