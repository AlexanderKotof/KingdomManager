using UnityEngine;

namespace KM.Core.Features
{

    public abstract class Feature : ScriptableObject, IFeature
    {
        public abstract void Initialize();
    }
}