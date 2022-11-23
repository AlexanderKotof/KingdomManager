using KM.Startup;

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
            AppStartup.Instance.RegisterSystem(new ResourcesSystem(this));
            AppStartup.Instance.RegisterSystem(new ResourcesDayChangeSystem(this));
        }
    }
}