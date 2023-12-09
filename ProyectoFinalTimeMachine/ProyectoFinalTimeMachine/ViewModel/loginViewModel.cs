using Firebase.Auth;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System;
using ProyectoFinalTimeMachine.Views;


namespace ProyectoFinalTimeMachine.ViewModel
{
    public class loginViewModel : BaseViewModel
    {
        //Atributos
        private string correo;
        private string contraseña;
      

        //Propiedades
        public string Correo
        {
            get { return this.correo; }
            set { SetValue(ref this.correo, value); }
        }

        public string Contraseña
        {
            get { return this.contraseña; }
            set { SetValue(ref this.contraseña, value); }
        }
        

        //Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(LoginMethod);
            }
            set
            {

            }
        }


        //Metodos
        public async void LoginMethod()
        {
            if (string.IsNullOrEmpty(this.correo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tiene que ingresar un correo.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.contraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tiene que ingresar una contraseña.",
                    "Ok");
                return;
            }

            string WebAPIkey = "AIzaSyCgAJJZC9CCVIFlvJ85RStZUUhSDr1t4hU";

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Correo.ToString(), Contraseña.ToString());
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);

                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);

                await App.Current.MainPage.DisplayAlert("Exito", "Bienvenido", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new agendaListPage());

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Correo o Contraseña incorrectos", "OK");
            }

        }
    }
}