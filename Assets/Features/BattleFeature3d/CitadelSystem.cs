using KM.Features.BattleFeature.BattleSystem3d;
using KM.Startup;
using System;
using UnityEngine;

namespace KM.Features.BattleFeature
{
    public class CitadelSystem : ISystem
    {
        private BattleFeature _feature;

        private Citadel _citadel;

        public event Action CitadelDestroyed;

        public CitadelSystem(BattleFeature feature)
        {
            _feature = feature;
        }

        public void Initialize()
        {
            _citadel = UnityEngine.Object.Instantiate(_feature.citadelPrefab, _feature.citadelPosition, Quaternion.identity).GetComponent<Citadel>();
            _citadel.Destroyed += (c) => CitadelDestroyed?.Invoke();
        }

        public void Destroy()
        {
            _citadel.Destroyed -= (c) => CitadelDestroyed?.Invoke();
        }
    }
}