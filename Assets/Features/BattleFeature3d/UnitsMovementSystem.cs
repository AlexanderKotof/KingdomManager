using KM.Features.BattleFeature.BattleSystem3d;
using KM.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.BattleFeature
{
    public class UnitsMovementSystem : ISystem, ISystemUpdate
    {
        private BattleSystem _battleSystem;

        public void Initialize()
        {
            _battleSystem = GameSystems.GetSystem<BattleSystem>();
        }

        public void Destroy()
        {
            
        }

        public void Update()
        {
            if (_battleSystem.battleState == BattleSystem.BattleState.Home)
                return;
        }
    }
}