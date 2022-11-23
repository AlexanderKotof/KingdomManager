using KM.Startup;

namespace KM.Features.Population
{
    public class PopulationFeature : Feature
    {
        public int PeoplesOnStart = 10;
        public int BaseDayGrowth = 1;

        public PopulationData[] populations;

        public override void Initialize()
        {
            AppStartup.Instance.RegisterSystem(new PopulationSystem(this));
        }
    }
}