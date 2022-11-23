using System;
using UnityEngine;

namespace KM.Features.Population
{
    [Serializable]
    public class PopulationData
    {
        public PopulationType type;

        [SerializeField]
        private int count;
        public int maxCount;

        public event Action<int> PopulationChanged;

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                var newCount = Mathf.Clamp(value, 0, maxCount);
                if (newCount != count)
                {
                    count = newCount;
                    PopulationChanged?.Invoke(count);
                }
            }
        }

        public PopulationData(PopulationData data)
        {
            this.type = data.type;
            this.count = data.count;
            this.maxCount = data.maxCount;
        }
    }
}