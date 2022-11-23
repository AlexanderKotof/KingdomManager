using KM.UI;
using KM.UI.CarouselScreens;
using KM.UI.Screens.Loading;
using ScreenSystem;
using System.Threading.Tasks;

namespace KM.Startup
{
    public class ShowBaseScreensState : GameState
    {
        public ShowBaseScreensState(string name) : base(name)
        {
        }

        public ShowBaseScreensState() : base(typeof(ShowBaseScreensState).Name)
        {

        }

        public async override Task Execute()
        {
            ScreensManager.ShowScreen<CarouselScreen>();
            ScreensManager.ShowScreen<ResourcesScreen>();

            ScreensManager.HideScreen<LoadingScreen>();

            await Task.Yield();
        }
    }
}