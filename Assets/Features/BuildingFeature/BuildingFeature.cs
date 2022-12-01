using KM.Core.Features;
using KM.Systems;
using System.Collections.Generic;

namespace KM.Features.BuildingFeature
{
    public class BuildingFeature : Feature
    {
        public List<BuildingBase> ReadyToBuild;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new BuildingSystem(ReadyToBuild));
        }
    }
}
