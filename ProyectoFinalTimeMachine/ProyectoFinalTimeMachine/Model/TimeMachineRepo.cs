using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalTimeMachine.Model
{
    public class TimeMachineRepo
    {

        FirebaseClient firebaseClient = new FirebaseClient("https://timemachine-ea7d7-default-rtdb.firebaseio.com/");

        public async Task<bool> Save(TimeMachineModel task)
        {
            var data = await firebaseClient.Child(nameof(TimeMachineModel)).PostAsync(JsonConvert.SerializeObject(task));

            if (!string.IsNullOrEmpty(data.Key)) return true;

            return false;
        }

        public async Task<List<TimeMachineModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(TimeMachineModel)).OnceAsync<TimeMachineModel>()).Select(item => new TimeMachineModel
            {
                Descripcion = item.Object.Descripcion,
                Tarea = item.Object.Tarea,
                Fecha = item.Object.Fecha,
                Hora = item.Object.Hora,
                IsCompleted = item.Object.IsCompleted,
                Id = item.Key
            }).ToList();
        }

        public async Task<List<TimeMachineModel>> GetAllByName(string tarea)
        {
            return (await firebaseClient.Child(nameof(TimeMachineModel)).OnceAsync<TimeMachineModel>()).Select(item => new TimeMachineModel
            {
                Descripcion = item.Object.Descripcion,
                Tarea = item.Object.Tarea,
                Fecha = item.Object.Fecha,
                Hora = item.Object.Hora,
                IsCompleted = item.Object.IsCompleted,
                Id = item.Key
            }).Where(c => c.Tarea.ToLower().Contains(tarea.ToLower())).ToList();
        }

        public async Task<TimeMachineModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(TimeMachineModel) + "/" + id).OnceSingleAsync<TimeMachineModel>());
        }

        public async Task<bool> Update(TimeMachineModel task)
        {
            await firebaseClient.Child(nameof(TimeMachineModel) + "/" + task.Id).PutAsync(JsonConvert.SerializeObject(task));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(TimeMachineModel) + "/" + id).DeleteAsync();
            return true;
        }
    }
}