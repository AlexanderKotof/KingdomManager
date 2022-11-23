using KM.Startup;
using System.Collections.Generic;

namespace KM.Features.BuildingFeature
{
    public class BuildingFeature : Feature
    {
        public List<BuildingBase> ReadyToBuild;

        public override void Initialize()
        {
            AppStartup.Instance.RegisterSystem(new BuildingSystem(ReadyToBuild));
        }
    }
}
