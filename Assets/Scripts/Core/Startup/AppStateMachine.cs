using KM.Features;
using System.Threading.Tasks;

namespace KM.Startup.StateMachine
{
    public class AppStateMachine
    {
        public IGameState State => _currentState;

        private IGameState _currentState;

        private readonly GameState _homeState = new GameState("Home");
        private readonly Feature[] _features;

        public AppStateMachine(Feature[] features)
        {
            _features = features;
        }

        public async void StartGame()
        {
            await SetState(new InitializationState());
            await SetState(new LoadHomeLocationState());
            await SetState(new CreateGameSystemsState(_features));
            await SetState(new ShowBaseScreensState());
            await SetState(_homeState);
        }

        public async void RestartGame()
        {
            await SetState(new InitializationState());
            await SetState(new LoadHomeLocationState());
            await SetState(new CreateGameSystemsState(_features));
            await SetState(new ShowBaseScreensState());
            await SetState(_homeState);
        }

        private async Task SetState(IGameState state)
        {
            _currentState = state;
            await _currentState.Execute();
        }

    }
}