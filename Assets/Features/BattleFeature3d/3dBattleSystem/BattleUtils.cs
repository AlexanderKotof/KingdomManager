using KM.Features.ArmyFeature;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public static class BattleUtils
    {
        public static int GetUnitInitiative(this Army team, int index)
        {
            var unit = team.GetUnitByIndex(index);
            if (!unit)
                return -1;

            if (team.general != null)
                return unit.Initiative + team.general.Initiative;

            return unit.Initiative;
        }

        public static int GetUnitBaseAttackDamage(this Army team, int index)
        {
            var unit = team.GetUnitByIndex(index);
            if (!unit)
                return -1;

            var damage = Random.Range(unit.AttackDamage, unit.AttackDamage + (int)unit.AttackType + 1);

            if (team.general != null)
                return damage + team.general.Strenght;

            return damage;
        }

        public static int GetUnitBaseDefence(this Army team, int index)
        {
            var unit = team.GetUnitByIndex(index);
            if (!unit)
                return -1;

            var defence = unit.Defence + (int)unit.DefenceType;

            if (team.general != null)
                return defence + team.general.Defence;

            return defence;
        }


        public static int CalculateDamage(BattleUnitEntity to, BattleUnitEntity from)
        {
            bool attacks = Random.value < from.AttackChance;

            if (attacks)
            {
                int damage = Random.Range(from.AttackDamage - (int)to.DefenceType, from.AttackDamage + (int)from.AttackType + 1);
                damage -= to.Defence;
                return Mathf.Clamp(damage, 1, 9999);
            }
            else
                return 0;
        }



        /// <summary>
        /// Sort all units indexes by initiative 
        /// </summary>
        /// <returns></returns>
        public static int[] CreateBattleQueue(Army playerTeam, Army enemyTeam)
        {
            List<int> sortedIndexes = new List<int>();

            for (int i = 0; i < playerTeam.units.Count; i++)
            {
                if (playerTeam.GetUnitByIndex(i) == null)
                    continue;

                bool added = false;

                var newUnit = playerTeam.GetUnitInitiative(i);

                for (int n = 0; n < sortedIndexes.Count; n++)
                {
                    var current = playerTeam.GetUnitInitiative(sortedIndexes[n]);

                    if (newUnit > current)
                    {
                        sortedIndexes.Insert(n, i);
                        added = true;
                        break;

                    }

                }
                if (!added)
                {
                    sortedIndexes.Add(i);
                }
            }
            for (int i = 0; i < enemyTeam.UnitTypesCount; i++)
            {
                if (enemyTeam.GetUnitByIndex(i) == null)
                    continue;

                bool added = false;

                var newUnit = enemyTeam.GetUnitByIndex(i).Initiative;

                for (int n = 0; n < sortedIndexes.Count; n++)
                {
                    int current = 0;

                    if (sortedIndexes[n] >= 6)
                    {
                        current = enemyTeam.GetUnitByIndex(sortedIndexes[n] - 6).Initiative;
                    }
                    else
                    {
                        current = playerTeam.GetUnitByIndex(sortedIndexes[n]).Initiative;
                    }


                    if (newUnit > current)
                    {
                        sortedIndexes.Insert(n, i + 6);
                        added = true;
                        break;
                    }
                    else if (newUnit == current && Random.value > 0.5f)
                    {

                        sortedIndexes.Insert(n, i + 6);
                        added = true;
                        break;

                    }

                }
                if (!added)
                {
                    sortedIndexes.Add(i + 6);
                }
            }
            return sortedIndexes.ToArray();
        }
    }
}