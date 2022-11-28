using System;
using UnityEngine;

namespace KM.Features.Population
{
    [Serializable]
    public class PopulationData
    {
        public PopulationType type;

        [SerializeField]
        private int _count;
        public int maxCount;

        public event Action<int> PopulationChanged;

        public int Count
        {
            get
            {
                return _count;
            }

        }

        public void SetCount(int count)
        {
            var newCount = Mathf.Clamp(count, 0, maxCount);
            if (newCount != _count)
            {
                _count = newCount;
                PopulationChanged?.Invoke(_count);
            }
        }

        public PopulationData(PopulationData data)
        {
            this.type = data.type;
            this._count = data._count;
            this.maxCount = data.maxCount;
        }
    }
}