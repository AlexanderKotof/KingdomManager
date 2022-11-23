using UnityEngine;

namespace KM.Features.Resources
{
    public static class ResourcesUtils
    {
        public static bool HasResources(this ResourceStorage resources, ResourceStorage required)
        {
            for (int i = 0; i < resources.resourcesCount; i++)
            {
                if (required.resources[i] != 0 && resources.resources[i] < required.resources[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static ResourceStorage Invert(this ResourceStorage resources)
        {
            var inverted = new ResourceStorage();
            for (int i = 0; i < resources.resourcesCount; i++)
            {
                inverted.resources[i] = -resources.resources[i];
            }
            return inverted;
        }


        public static void ChangeResources(this ResourceStorage resources, ResourceStorage change)
        {
            for (int i = 0; i < resources.resourcesCount; i++)
            {
                resources.resources[i] += change.resources[i];
            }
        }

        public static void ChangeResourcesClamped(this ResourceStorage resources, ResourceStorage change)
        {
            for (int i = 0; i < resources.resourcesCount; i++)
            {
                resources.resources[i] += change.resources[i];
                resources.resources[i] = Mathf.Max(resources.resources[i], 0);
            }
        }
        public static void Clear(this ResourceStorage resources)
        {
            for (int i = 0; i < resources.resourcesCount; i++)
            {
                resources.resources[i] = 0;
            }
        }
    }
}