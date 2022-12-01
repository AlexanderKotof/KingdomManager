using KM.Core.Features;
using KM.Systems;
using System.Threading.Tasks;

namespace KM.Startup
{
    public class CreateGameSystemsState : GameState
    {
        Feature[] _features;

        public CreateGameSystemsState() : base(typeof(CreateGameSystemsState).Name)
        {
        }

        public async override Task Execute()
        {
            _features = KM.Core.Features.Features.GetFeatures();

            CreateSystems();

            InitializeSystems();

            await Task.Delay(100);
        }

        private void InitializeSystems()
        {
            foreach (var system in GameSystems.Systems.Values)
            {
                system.Initialize();
            }
        }

        private void CreateSystems()
        {
            foreach(var feature in _features)
            {
                feature.Initialize();
            }
        }
    }
}