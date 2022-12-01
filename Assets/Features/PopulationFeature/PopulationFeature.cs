using KM.Core.Features;
using KM.Systems;

namespace KM.Features.Population
{
    public class PopulationFeature : Feature
    {
        public int PeoplesOnStart = 10;
        public int BaseDayGrowth = 1;

        public PopulationData[] populations;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new PopulationSystem(this));
        }
    }
}