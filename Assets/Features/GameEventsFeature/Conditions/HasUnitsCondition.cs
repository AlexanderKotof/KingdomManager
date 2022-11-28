using KM.Features.ArmyFeature;
using KM.Systems;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Conditions
{
    [CreateAssetMenu(menuName = "Events/Conditions/Has Units")]
    public class HasUnitsCondition : Condition
    {
        public Army NeededUnits;

        public enum CheckIn
        {
            Free,
            Defence,
            All,
        }
        public CheckIn checkInArmy;

        ArmySystem _armySystem;

        public override void Initialize()
        {
            _armySystem = GameSystems.GetSystem<ArmySystem>();
            _armySystem.ArmyUpdated += OnArmiesUpdated;
        }

        private void OnArmiesUpdated()
        {
            Army army;
            switch (checkInArmy)
            {
                case CheckIn.Free:
                    army = _armySystem.freeArmy;
                    break;
                case CheckIn.Defence:
                    army = _armySystem.guardArmy;
                    break;
                case CheckIn.All:
                    army = new Army(_armySystem.freeArmy);
                    army.AddUnits(_armySystem.guardArmy);
                    break;
                default:
                    return;
            }

            for (int i = 0; i < NeededUnits.units.Count; i++)
            {
                var unit = NeededUnits.units[i];
                int neededCount = unit.count;

                if (army.GetUnitCount(unit.prototype) < neededCount)
                {
                    return;
                }
            }

            _armySystem.ArmyUpdated -= OnArmiesUpdated;
            Satisfy();
        }
    }
}
