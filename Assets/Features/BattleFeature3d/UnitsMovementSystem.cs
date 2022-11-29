using KM.Features.BattleFeature.BattleSystem3d;
using KM.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.BattleFeature
{
    public class UnitsMovementSystem : ISystem, ISystemUpdate
    {
        private BattleSystem _battleSystem;
        private List<Unit> _unitsInBattle;

        public void Initialize()
        {
            _battleSystem = GameSystems.GetSystem<BattleSystem>();

            _battleSystem.BattleStarted += OnBattleStarted;
            _battleSystem.BattleEnded += OnBattleEnded;
        }


        public void Destroy()
        {
            _battleSystem.BattleStarted -= OnBattleStarted;
            _battleSystem.BattleEnded -= OnBattleEnded;
        }

        private void OnBattleEnded(BattleInfo obj)
        {
            _unitsInBattle.Clear();
        }

        private void OnBattleStarted(BattleInfo obj)
        {
            _unitsInBattle = _battleSystem.GetAllUnitsInBattle();
        }


        public void Update()
        {
            if (_unitsInBattle == null || _unitsInBattle.Count == 0)
            {
                // Move to positions
                return;
            }

            foreach(var unit in _unitsInBattle)
            {
                if (unit.IsDead || unit.target == null || unit.target.IsDead)
                    continue;

                var direction = unit.target.transform.position - unit.transform.position;
                var sqrDistance = direction.sqrMagnitude;

                if (sqrDistance >= unit.AttackDistance * unit.AttackDistance)
                {
                    unit.transform.Translate(direction.normalized * 0.5f * Time.deltaTime);
                }
            }
        }
    }
}