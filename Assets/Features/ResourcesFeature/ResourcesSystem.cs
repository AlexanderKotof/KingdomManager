using KM.Startup;
using System;

namespace KM.Features.Resources
{
    public class ResourcesSystem : ISystem
    {
        public ResourceStorage Resources { get; private set; }

        public event Action<ResourceStorage> ResourcesUpdated;

        public ResourcesSystem(ResourcesFeature feature)
        {
            Resources = new ResourceStorage(feature.resources);
        }

        public void Initialize()
        {

        }

        public void Destroy()
        {

        }

        public void ChangeResources(ResourceStorage resources)
        {
            Resources.ChangeResourcesClamped(resources);

            ResourcesUpdated?.Invoke(resources);
        }
    }
}