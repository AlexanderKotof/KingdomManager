using KM.UI.Screens.Loading;
using ScreenSystem;
using System.Threading.Tasks;
using UnityEngine;

namespace KM.Startup
{
    public class InitializationState : GameState
    {
        public InitializationState(string name) : base(name)
        {
        }
        public InitializationState() : base(typeof(InitializationState).Name)
        {
        }

        public async override Task Execute()
        {
            ScreensManager.ShowScreen<LoadingScreen>();
            await Task.CompletedTask;
        }
    }
}