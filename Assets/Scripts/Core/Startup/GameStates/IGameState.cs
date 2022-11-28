using System.Threading.Tasks;

namespace KM.Startup
{
    public interface IGameState
    {
        public Task Execute();
    }
}