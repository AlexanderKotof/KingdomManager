using KM.Features;
using KM.Systems;
using System.Threading.Tasks;

namespace KM.Startup
{
    public class CreateGameSystemsState : GameState
    {
        Feature[] _features;

        public CreateGameSystemsState(string name) : base(name)
        {
        }
        public CreateGameSystemsState(Feature[] features) : base(typeof(CreateGameSystemsState).Name)
        {
            _features = features;
        }

        public async override Task Execute()
        {
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