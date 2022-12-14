using KM.Core.Features;
using KM.Features.BattleFeature.BattleSystem3d;
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
            GameSystems.RegisterSystem(new BattleSystem());
            GameSystems.RegisterSystem(new CitadelSystem(this));
            GameSystems.RegisterSystem(new TargetSelectionSystem());
            GameSystems.RegisterSystem(new UnitsMovementSystem());
            GameSystems.RegisterSystem(new AttackSystem());
        }
    }
}