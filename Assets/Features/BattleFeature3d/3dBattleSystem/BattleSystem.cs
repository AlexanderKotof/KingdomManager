using KM.Startup;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static KM.Features.BattleFeature.BattleSystem3d.Unit;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public partial class BattleSystem : ISystem
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

        public BattleSystem()
        {
            battleState = BattleState.Home;

            allies = new List<Unit>();
            enemies = new List<Unit>();
        }

        public void Initialize()
        {

        }

        public void Destroy()
        {
            if (battleState == BattleState.InBattle)
                AppStartup.Instance.StopCoroutine(BattleProcess());
        }

        public void BeginBattle(BattleInfo battleInfo)
        {
            if (battleState == BattleState.InBattle)
                return;

            _battleInfo = battleInfo;

            allies = AppStartup.Instance.GetSystem<ArmyFeature.ArmyTacticSystem>().GuardUnits;
            SpawnEnemies(battleInfo);

            AppStartup.Instance.StartCoroutine(BattleProcess());

            BattleStarted?.Invoke(battleInfo);
        }


        public void SpawnUnit(IUnitPrototype prototype, Vector3 position, Fraction fraction)
        {
            var selectedList = fraction == Fraction.Fraction1 ? allies : enemies;
            var unit = SpawnUnitsUtils.SpawnUnit(prototype, position);

            unit.fraction = fraction;

            selectedList.Add(unit);

            OnUnitSpawned?.Invoke(unit);
        }

        private IEnumerator BattleProcess()
        {
            battleState = BattleState.InBattle;

            while (enemies.Count > 0)
            {
                Attack();
                yield return new WaitForSeconds(0.5f);
            }

            // Battle Ends
            var winer = allies.Count > 0 ? Fraction.Fraction1 : Fraction.Fraction2;
            EndBattle(winer);
        }

        private void Attack()
        {
            foreach(var unit in allies)
            {
                var target = enemies[UnityEngine.Random.Range(0, enemies.Count)];

                var prototype = ((BattleUnitEntity)unit.prototype);
                var damage = UnityEngine.Random.value <= prototype.AttackChance ? prototype.Attack : 0;

                target.TakeDamage(damage);

                if (target.health <= 0)
                    enemies.Remove(target);
            }
            foreach (var unit in enemies)
            {
                var target = allies[UnityEngine.Random.Range(0, allies.Count)];

                var prototype = ((BattleUnitEntity)unit.prototype);
                var damage = UnityEngine.Random.value <= prototype.AttackChance ? prototype.Attack : 0;

                target.TakeDamage(damage);

                if (target.health <= 0)
                    enemies.Remove(target);
            }
        }

        private void SpawnEnemies(BattleInfo battleInfo)
        {
            foreach (var enemiesToSpawn in battleInfo.enemies.units)
            {
                for(int i = 0; i < enemiesToSpawn.count; i++)
                    SpawnUnit(enemiesToSpawn.prototype, enemiesToSpawn.positions[i], Fraction.Fraction2);
            }
        }

        private void EndBattle(Fraction fractionWin)
        {
            Debug.Log("Battle ends, winner - " + fractionWin);
            battleState = BattleState.Home;

            BattleEnded?.Invoke(_battleInfo);
        }
    }
}
