using KM.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public class AttackSystem : ISystem, ISystemUpdate
    {
        private BattleSystem _battleSystem;

        private List<Unit> _unitsInBattle;

        public void Destroy()
        {
            _battleSystem.BattleStarted -= OnBattleStarted;
            _battleSystem.BattleEnded -= OnBattleEnded;
        }

        public void Initialize()
        {
            _battleSystem = GameSystems.GetSystem<BattleSystem>();
            _battleSystem.BattleStarted += OnBattleStarted;
            _battleSystem.BattleEnded += OnBattleEnded;
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
                return;

            for (int i = 0; i < _unitsInBattle.Count; i++)
            {
                Unit unit = _unitsInBattle[i];

                if (unit.IsDead || unit.target == null || unit.target.IsDead)
                    continue;

                var distance = (unit.target.transform.position - unit.transform.position).sqrMagnitude;

                if (distance > unit.AttackDistance * unit.AttackDistance)
                    continue;

                if (unit.lastAttackTime + 1f > Time.realtimeSinceStartup)
                    continue;

                AttackTarget(unit);
            }
        }

        private void AttackTarget(Unit unit)
        {
            unit.lastAttackTime = Time.realtimeSinceStartup;

            unit.target.TakeDamage(unit.Damage);
        }
    }
}
