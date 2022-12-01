using KM.Core.Features;
using KM.Systems;

namespace KM.Features.Resources
{
    public class ResourcesFeature : Feature
    {
        public ResourceStorage resources = new ResourceStorage();

        public ResourceStorage baseDayChange = new ResourceStorage();

        public float foodProductionByFarmersModifier;
        public float goldProductionModifier;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new ResourcesSystem(this));
            GameSystems.RegisterSystem(new ResourcesDayChangeSystem(this));
        }
    }
}