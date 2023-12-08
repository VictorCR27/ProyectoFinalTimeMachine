using Xamarin.Forms;
using ProyectoFinalTimeMachine.Views;

namespace ProyectoFinalTimeMachine
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new agendaListPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
