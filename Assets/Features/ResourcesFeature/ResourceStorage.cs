using System;
using System.Collections;
using System.Collections.Generic;

namespace KM.Features.Resources
{
    public enum ResourceType
    {
        Food,
        Timber,
        Rock,
        Metal,
        Gold,
        Gems,
    }

    [System.Serializable]
    public class ResourceStorage
    {
        public int[] resources;

        public readonly int resourcesCount = (typeof(ResourceType)).GetEnumNames().Length;

        public System.Action onResourcesChanged;
        public System.Action<ResourceStorage> ResourcesChanged;

        public ResourceStorage()
        {
            resources = new int[resourcesCount];
        }

        /// <summary>
        /// Creates with resources counts Food, Timber, Rock, Metal, Gold, Gems counts
        /// </summary>
        /// <param name="counts"></param>
        public ResourceStorage(params int[] counts)
        {
            resources = new int[resourcesCount];
            for (int i = 0; i < resourcesCount && i < counts.Length; i++)
                AddResource(i, counts[i]);
        }

        public ResourceStorage(ResourceStorage data)
        {
            resources = new int[resourcesCount];
            for (int i = 0; i < resourcesCount; i++)
            {
                resources[i] = data.resources[i];
            }
        }

        public bool HasResource(int type, int count)
        {
            return resources[type] >= count;
        }

        public void AddResource(int type, int count)
        {
            resources[type] += count;
        }

        public void RemoveResource(int type, int count)
        {
            resources[type] -= count;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < resourcesCount; i++)
            {
                if (resources[i] > 0)
                {
                    result += (ResourceType)i + ": " + resources[i] + "  ";
                }
            }

            return result;
        }
    }
}