using System;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Core.Features
{
    public class Features : MonoBehaviour
    {
        public Feature[] features;

        private static Features _instance;

        private Dictionary<Type, IFeature> _typesToFeature;

        private void Awake()
        {
            _instance = this;
            _typesToFeature = new Dictionary<Type, IFeature>();

            foreach (var feature in features)
            {
                _typesToFeature.Add(feature.GetType(), feature);
            }
        }

        public static Feature[] GetFeatures()
        {
            return _instance.features;
        }

        public static T GetFeature<T>() where T : IFeature
        {
            if (_instance._typesToFeature.TryGetValue(typeof(T), out var feature))
                return (T)feature;

            Debug.LogError($"No Feature of type {typeof(T).Name} founded!");
            return default;
        }
    }
}