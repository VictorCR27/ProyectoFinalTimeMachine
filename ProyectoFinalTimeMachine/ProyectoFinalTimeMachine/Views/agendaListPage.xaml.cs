using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoFinalTimeMachine.Model;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace ProyectoFinalTimeMachine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class agendaListPage : ContentPage
    {
        TimeMachineRepo tareaRepository = new TimeMachineRepo();
        public agendaListPage()
        {
            InitializeComponent();

            TareaListView.RefreshCommand = new Command(() => {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            // Obtén el ID del usuario actual
            string userId = Preferences.Get("MyFirebaseUserId", string.Empty);

            // Verifica que el ID del usuario no esté vacío
            if (!string.IsNullOrEmpty(userId))
            {
                // Obtén las tareas que pertenecen al usuario actual
                var tasks = await tareaRepository.GetAll(userId);

                // Establece las tareas como origen de datos para la lista
                TareaListView.ItemsSource = tasks;
            }
            else
            {
                // Si el ID del usuario está vacío, muestra un mensaje de error o realiza alguna acción adecuada
                await DisplayAlert("Error", "No se pudo obtener el ID del usuario actual.", "OK");
            }

            TareaListView.IsRefreshing = false;
        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new agendaEntry());
        }

        private void TareaListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            /*
             if (e.Item == null) return;
            */

            var task = e.Item as TimeMachineModel;
            //Navigation.PushModalAsync(new agendaDetails(task));
            ((ListView)sender).SelectedItem = null;
        }

        private async void DeleteTap_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Delete", "Do you want to delete?", "Yes", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await tareaRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "Task has been deleted.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Task deleted failed.", "Ok");
                }
            }
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var task = await tareaRepository.GetById(id);
            if (task == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            task.Id = id;
            await Navigation.PushModalAsync(new agendaEdit(task));

        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var tasks = await tareaRepository.GetAllByName(searchValue);
                TareaListView.ItemsSource = null;
                TareaListView.ItemsSource = tasks;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = txtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var tasks = await tareaRepository.GetAllByName(searchValue);
                TareaListView.ItemsSource = null;
                TareaListView.ItemsSource = tasks;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var task = await tareaRepository.GetById(id);
            if (task == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            task.Id = id;
            await Navigation.PushModalAsync(new agendaEdit(task));
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Advertencia", "Quiere borrar este estudiante?", "Si", "No");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await tareaRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Importante", "El estudiante ha sido eliminado", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Student deleted failed.", "Ok");
                }
            }
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var task = await tareaRepository.GetById(id);
            if (task == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            task.Id = id;
            await Navigation.PushModalAsync(new agendaEdit(task));
        }

        //Manual
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManualUsuario());
        }
    }
}