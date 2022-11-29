using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.ArmyFeature
{
    [Serializable]
    public class ArmyUnitData
    {
        public BattleUnitEntity prototype;
        public List<Vector2> positions;
        public int count;

        public List<Modificator> modificators = new List<Modificator>();
    }

    [Serializable]
    public class Army
    {
        public General general;
        public List<ArmyUnitData> units;

        public int GetUnitsCount(int index)
        {
            return units[index].count;
        }
        public BattleUnitEntity GetUnit(int index)
        {
            return units[index].prototype;
        }

        public int UnitTypesCount
        {
            get
            {
                int n = 0;

                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i] != null && units[i].count > 0)
                        n++;
                }
                return n;
            }
        }

        public int GetAllUnitsCount()
        {
            int count = 0;
            foreach(var unit in units)
            {
                count += units.Count;
            }
            return count;
        }

        public Army()
        {
            units = new List<ArmyUnitData>();
        }

        public Army(Army Units)
        {
            units = new List<ArmyUnitData>();

            for (int i = 0; i < Units.units.Count; i++)
            {
                AddUnits(Units.units[i].prototype, Units.units[i].count);
            }
        }

        public bool IsUnitDead(int index)
        {
            return units[index].count <= 0;
        }

        public void AddUnit(BattleUnitEntity unit, Vector3 position)
        {
            int index = GetUnitIndex(unit);

            if (index >= 0)
            {
                units[index].count += 1;
                units[index].positions.Add(position);
            }
            else
            {
                units.Add(new ArmyUnitData()
                {
                    prototype = unit,
                    count = 1,
                    positions = new List<Vector2>() { position },
                });
            }
        }

        public void AddUnits(BattleUnitEntity unit, int count)
        {
            int index = GetUnitIndex(unit);

            if (index >= 0)
            {
                units[index].count += count;
                units[index].positions.Add(Vector3.zero);
            }
            else
            {
                units.Add(new ArmyUnitData()
                {
                    prototype = unit,
                    count = count,
                    positions = new List<Vector2>() { Vector3.zero },
                });
            }
        }

        public void AddUnits(Army units)
        {
            for (int i = 0; i < units.UnitTypesCount; i++)
            {
                AddUnits(units.GetUnitByIndex(i, out int count), count);
            }
        }

        public void RemoveUnits(BattleUnitEntity unit, int count)
        {
            int index = GetUnitIndex(unit);

            if (index < 0)
                return;

            units[index].count -= count;

            if (units[index].count <= 0)
            {
                units.RemoveAt(index);
            }
        }

        public Army GetDelta(Army battleUnits)
        {
            var deltaUnits = new Army(this);

            for (int i = 0; i < battleUnits.units.Count; i++)
            {
                deltaUnits.RemoveUnits(battleUnits.units[i].prototype, battleUnits.units[i].count);
            }

            return deltaUnits;
        }

        public int GetUnitIndex(BattleUnitEntity unit)
        {
            if (!unit)
                return -1;

            int unitHash = unit.Hash;
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] != null && units[i].prototype != null && units[i].prototype.Hash == unitHash)
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetUnitCount(BattleUnitEntity unit)
        {
            int index = GetUnitIndex(unit);
            if (index >= 0)
                return units[index].count;
            else
                return 0;
        }


        public BattleUnitEntity GetUnitByIndex(int index)
        {
            if (units.Count <= index || index < 0)
                return null;

            return units[index].prototype;
        }
        public BattleUnitEntity GetUnitByIndex(int index, out int count)
        {
            if (units.Count <= index || index < 0)
            {
                count = 0;
                return null;
            }

            count = units[index].count;
            return units[index].prototype;
        }
        /*
        /// <summary>
        /// Returns left units count
        /// </summary>
        /// <param name="unitIndex"></param>
        /// <param name="damage"></param>
        /// <returns></returns>
        public int TakeDamage(int unitIndex, int damage)
        {
            var unit = units[unitIndex].prototype;

            unit.CurrentHealth -= damage;

            if (unit.CurrentHealth <= 0)
            {
                var killedCount = Mathf.Clamp((-unit.CurrentHealth + unit.Health) / unit.Health, 1, 10000);

                unit.CurrentHealth += unit.Health * killedCount;

                units[unitIndex].count -= killedCount;

                if (units[unitIndex].count < 0)
                    units[unitIndex].count = 0;
            }

            return units[unitIndex].count;
        }
        */
        public int AllUnitsCount()
        {
            int count = 0;

            foreach (var unit in units)
            {
                count += unit.count;
            }

            return count;
        }
        /*
        public void RemoveDead()
        {
            for (int i = 0; i < UnitTypesCount; i++)
            {
                var unit = units[i].prototype;

                if (unit.CurrentHealth <= 0)
                {
                    var killedCount = Mathf.Clamp((-unit.CurrentHealth + unit.Health) / unit.Health, 1, 10000);

                    unit.CurrentHealth += unit.Health * killedCount;

                    RemoveUnits(unit, killedCount);

                    //units[1].AddUnits(unit, killedCount);
                }
            }
        }*/
    }
}