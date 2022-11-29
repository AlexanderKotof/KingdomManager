using KM.Systems;
using System.Collections.Generic;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public class TargetSelectionSystem : ISystem, ISystemUpdate
    {
        private BattleSystem _battleSystem;
        private List<Unit> _unitsInBattle;

        public void Initialize()
        {
            _battleSystem = GameSystems.GetSystem<BattleSystem>();
            _battleSystem.BattleStarted += OnBattleStarted;
            _battleSystem.BattleEnded += OnBattleEnded;

            _unitsInBattle = new List<Unit>();
        }

        public void Destroy()
        {
            _battleSystem.BattleEnded -= OnBattleEnded;
            _battleSystem.BattleStarted -= OnBattleStarted;
        }

        private void OnBattleStarted(BattleInfo obj)
        {
            _unitsInBattle = _battleSystem.GetAllUnitsInBattle();
        }

        private void OnBattleEnded(BattleInfo obj)
        {
            _unitsInBattle.Clear();
        }

        public void Update()
        {
            if (_unitsInBattle.Count == 0)
                return;

            foreach (var unit in _unitsInBattle)
            {
                if (unit.IsDead || unit.target != null && !unit.target.IsDead)
                {
                    continue;
                }

                var minDistance = float.MaxValue;

                foreach (var enemy in unit.enemies)
                {
                    var sqrDistance = (enemy.transform.position - unit.transform.position).sqrMagnitude;

                    if (sqrDistance > minDistance)
                        continue;

                    minDistance = sqrDistance;
                    unit.target = enemy;
                    enemy.target = unit;
                }
            }
        }
    }
}
