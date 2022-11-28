using KM.Systems;
using UnityEngine;

namespace KM.Features.BattleFeature
{
    public class BattleFeature : Feature
    {
        public GameObject citadelPrefab;
        public Vector3 citadelPosition;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new BattleSystem3d.BattleSystem());
            GameSystems.RegisterSystem(new CitadelSystem(this));
        }
    }
}