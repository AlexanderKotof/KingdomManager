using KM.Core;
using KM.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static KM.Features.BattleFeature.BattleSystem3d.Unit;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public class BattleSystem : ISystem
    {
        public event Action<Unit> OnUnitSpawned;

        public enum BattleState
        {
            Home,
            InBattle,
        }
        public BattleState battleState;

        public List<Unit> allies;
        public List<Unit> enemies;

        public event Action<BattleInfo> BattleStarted;
        public event Action<BattleInfo> BattleEnded;

        private BattleInfo _battleInfo;

        private CitadelSystem _citadelSystem;

        public BattleSystem()
        {
            battleState = BattleState.Home;

            allies = new List<Unit>();
            enemies = new List<Unit>();
        }

        public void Initialize()
        {
            _citadelSystem = GameSystems.GetSystem<CitadelSystem>();
        }

        public void Destroy()
        {
            if (battleState == BattleState.InBattle)
                Coroutines.Stop(BattleProcess());
        }

        public void BeginBattle(BattleInfo battleInfo)
        {
            if (battleState == BattleState.InBattle)
                return;

            _battleInfo = battleInfo;

            SetupUnits(battleInfo);

            Coroutines.Run(BattleProcess());

            BattleStarted?.Invoke(battleInfo);
        }

        private void SetupUnits(BattleInfo battleInfo)
        {
            allies = GameSystems.GetSystem<ArmyFeature.ArmyPlacementSystem>().GuardUnits;
            allies.Add(_citadelSystem.Citadel);

            SpawnEnemies(battleInfo);

            foreach (var unit in allies)
            {
                unit.enemies = enemies;
            }
        }
        private Unit SpawnUnit(IUnitPrototype prototype, Vector3 position, Fraction fraction)
        {
            var selectedList = fraction == Fraction.Fraction1 ? allies : enemies;
            var unit = SpawnUnitsUtils.SpawnUnit(prototype, position);

            unit.fraction = fraction;

            selectedList.Add(unit);

            OnUnitSpawned?.Invoke(unit);

            return unit;
        }

        private IEnumerator BattleProcess()
        {
            battleState = BattleState.InBattle;

            while (enemies.Count > 0)
            {
                yield return new WaitForSeconds(0.5f);
            }

            // Battle Ends
            var winer = allies.Count > 0 ? Fraction.Fraction1 : Fraction.Fraction2;
            EndBattle(winer);
        }

        

        private void SpawnEnemies(BattleInfo battleInfo)
        {
            foreach (var enemiesToSpawn in battleInfo.enemies.units)
            {
                for (int i = 0; i < enemiesToSpawn.count; i++)
                {
                    var unit = SpawnUnit(enemiesToSpawn.prototype, enemiesToSpawn.positions[i], Fraction.Fraction2);
                    unit.enemies = allies;
                }
            }
        }

        private void EndBattle(Fraction fractionWin)
        {
            Debug.Log("Battle ends, winner - " + fractionWin);
            battleState = BattleState.Home;

            BattleEnded?.Invoke(_battleInfo);
        }
    }

    public class AttackSystem : ISystem
    {
        private BattleSystem _battleSystem;

        private List<Unit> _unitsInBattle;

        public void Destroy()
        {
            _battleSystem.BattleStarted -= _battleSystem_BattleStarted;
        }

        public void Initialize()
        {
            _unitsInBattle = new List<Unit>();
            _battleSystem = GameSystems.GetSystem<BattleSystem>();
            _battleSystem.BattleStarted += _battleSystem_BattleStarted;
        }

        private void _battleSystem_BattleStarted(BattleInfo obj)
        {
            _unitsInBattle.AddRange(_battleSystem.allies);
            _unitsInBattle.AddRange(_battleSystem.enemies);

            Coroutines.Run(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            yield return null;
        }
/*
        private void Attack()
        {
            foreach (var unit in _battleSystem.allies)
            {
                if (enemies.Count == 0)
                    break;

                var target = enemies[UnityEngine.Random.Range(0, enemies.Count)];

                var prototype = ((BattleUnitEntity)unit.prototype);
                var damage = UnityEngine.Random.value <= prototype.AttackChance ? prototype.Attack : 0;

                target.TakeDamage(damage);

                if (target.health <= 0)
                    enemies.Remove(target);
            }

            foreach (var unit in enemies)
            {
                if (allies.Count == 0)
                    break;

                var target = allies[UnityEngine.Random.Range(0, allies.Count)];

                var prototype = ((BattleUnitEntity)unit.prototype);
                var damage = UnityEngine.Random.value <= prototype.AttackChance ? prototype.Attack : 0;

                target.TakeDamage(damage);

                if (target.health <= 0)
                    enemies.Remove(target);
            }
        }*/
    }
}
