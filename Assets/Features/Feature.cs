using UnityEngine;

namespace KM.Features
{
    public interface IFeature
    {
        void Initialize();
    }

    public abstract class Feature : ScriptableObject, IFeature
    {
        public abstract void Initialize();
    }
}