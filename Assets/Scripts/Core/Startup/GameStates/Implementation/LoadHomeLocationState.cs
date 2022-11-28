using System.Threading.Tasks;

namespace KM.Startup
{
    public class LoadHomeLocationState : GameState
    {
        public LoadHomeLocationState(string name) : base(name)
        {
        }
        public LoadHomeLocationState() : base(typeof(LoadHomeLocationState).Name)
        {
        }

        public async override Task Execute()
        {
            // Load saved data, show LoadingScreen

            var result = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Home");

            while (!result.isDone)
            {
                await Task.Delay(100);
            }
        }
    }
}