using System.Threading.Tasks;

namespace KM.Startup.StateMachine
{
    public class AppStateMachine
    {
        private static AppStateMachine _instance;
        public IGameState State => _currentState;

        private IGameState _currentState;

        private readonly GameState _loadedState = new GameState("Loaded");

        public AppStateMachine()
        {
            _instance = this;
        }

        public static async void StartGame()
        {
            await _instance.SetState(new InitializationState());
            await _instance.SetState(new LoadHomeLocationState());
            await _instance.SetState(new CreateGameSystemsState());
            await _instance.SetState(new ShowBaseScreensState());
            await _instance.SetState(_instance._loadedState);
        }

        public static async void RestartGame()
        {
            await _instance.SetState(new InitializationState());
            await _instance.SetState(new LoadHomeLocationState());
            await _instance.SetState(new CreateGameSystemsState());
            await _instance.SetState(new ShowBaseScreensState());
            await _instance.SetState(_instance._loadedState);
        }

        private async Task SetState(IGameState state)
        {
            _currentState = state;
            await _currentState.Execute();
        }

    }
}