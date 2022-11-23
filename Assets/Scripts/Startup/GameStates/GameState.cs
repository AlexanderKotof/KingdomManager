using System.Threading.Tasks;

namespace KM.Startup
{
    public class GameState : IGameState
    {
        public string name;

        public GameState(string name)
        {
            this.name = name;
        }

        public virtual Task Execute()
        {
            return Task.CompletedTask;
        }
    }
}