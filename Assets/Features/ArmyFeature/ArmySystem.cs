using KM.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.ArmyFeature
{
    public class ArmySystem : ISystem
    {
        public Army freeArmy;

        public Army guardArmy;

        public List<General> availableGenerals;

        public event Action ArmyUpdated;

        public ArmySystem()
        {
            freeArmy = new Army();
            guardArmy = new Army();
        }

        public int GetAllUnitsCount()
        {
            return freeArmy.GetAllUnitsCount() + guardArmy.GetAllUnitsCount();
        }

        public void Initialize()
        {

        }

        public void Destroy()
        {

        }

        public void AddFreeUnits(BattleUnitEntity unit, int count)
        {
            freeArmy.AddUnits(unit, count);
            ArmyUpdated?.Invoke();
        }

        public void MoveToDefence(BattleUnitEntity selectedUnit, Vector3 position)
        {
            freeArmy.RemoveUnits(selectedUnit, 1);
            guardArmy.AddUnit(selectedUnit, position);

            ArmyUpdated?.Invoke();
        }

        public void MoveToArmy(BattleUnitEntity selectedUnit, int count)
        {
            freeArmy.AddUnits(selectedUnit, count);
            guardArmy.RemoveUnits(selectedUnit, count);

            ArmyUpdated?.Invoke();
        }

        public int GetGoldConsuming()
        {
            int price = 0;
            foreach (var unit in freeArmy.units)
                price += unit.count;

            return price;
        }
    }
}