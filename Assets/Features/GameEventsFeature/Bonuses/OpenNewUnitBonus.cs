using KM.Features.ArmyFeature;
using KM.Startup;
using System;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Bonuses
{
    [CreateAssetMenu(menuName = "Game Entities/Events/Bonuses/Unlock Unit")]
    public class OpenNewUnitBonus : Bonus
    {
        public BattleUnitEntity openUnit;

        public BattleUnitEntity removesUnit;

        public static event Action<BattleUnitEntity> unitOpened;

        public override void Activate()
        {
            Debug.Log("Activate " + name);
            // add unit to ready for recruit
            AppStartup.Instance.GetSystem<ArmySystem>().ReadyToRecruit.Add(openUnit);

            if (removesUnit != null)
            {
                AppStartup.Instance.GetSystem<ArmySystem>().ReadyToRecruit.Remove(removesUnit);
            }

            unitOpened?.Invoke(openUnit);

        }

        public override void Deactivate()
        {
            AppStartup.Instance.GetSystem<ArmySystem>().ReadyToRecruit.Remove(openUnit);
            if (removesUnit != null)
            {
                AppStartup.Instance.GetSystem<ArmySystem>().ReadyToRecruit.Add(removesUnit);
            }
            unitOpened?.Invoke(openUnit);
        }

        public override string GetDescription()
        {
            return "Allows hire: " + openUnit.Name;
        }
    }
}
