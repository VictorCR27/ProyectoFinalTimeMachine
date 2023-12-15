using Firebase.Auth;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System;
using ProyectoFinalTimeMachine.Views;
using System.Linq;
using System.Threading.Tasks;


namespace ProyectoFinalTimeMachine.ViewModel
{
    public class loginViewModel : BaseViewModel
    {
        //Atributos
        private string correo;
        private string contraseña;
        public agendaEntryViewModel AgendaEntryViewModel { get; private set; }


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

            if (string.IsNullOrEmpty(this.correo) || string.IsNullOrEmpty(this.contraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un correo y una contraseña.",
                    "Aceptar");
                return;
            }

            string WebAPIkey = "AIzaSyCgAJJZC9CCVIFlvJ85RStZUUhSDr1t4hU";

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Correo.ToString(), Contraseña.ToString());

                // Almacena el UID del usuario y el token de actualización en las preferencias
                Preferences.Set("MyFirebaseUserId", auth.User?.LocalId);
                Preferences.Set("MyFirebaseRefreshToken", auth.FirebaseToken);


                AgendaEntryViewModel = new agendaEntryViewModel(auth.User?.LocalId);

                string mensajeBienvenida = $"Bienvenido, usuario con ID: {auth.User?.LocalId}";
                await App.Current.MainPage.DisplayAlert("Éxito", mensajeBienvenida, "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new agendaListPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Correo o Contraseña incorrectos", "OK");
            }
        }

        private async void NavigateToAgendaEntry(string userId)
        {
            // Crear una instancia de la página agendaEntry y pasar el userId al ViewModel
            var agendaEntryPage = new agendaEntry();
            var agendaEntryViewModel = new agendaEntryViewModel(userId);
            agendaEntryPage.BindingContext = agendaEntryViewModel;

            // Realizar la navegación a la página agendaEntry
            await Application.Current.MainPage.Navigation.PushAsync(agendaEntryPage);
        }



    }
}