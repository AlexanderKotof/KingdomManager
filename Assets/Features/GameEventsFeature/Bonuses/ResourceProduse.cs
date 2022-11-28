using KM.Features.Resources;
using KM.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Bonuses
{
    [CreateAssetMenu(menuName = "Game Entities/Events/Bonuses/Resources Production")]
    public class ResourceProduse : Bonus
    {
        public ResourceStorage produseInADay;

        public override void Activate()
        {
            Debug.Log("Activate " + name);

            GameSystems.GetSystem<ResourcesDayChangeSystem>().ChangeBaseProduction(produseInADay);
        }

        public override void Deactivate()
        {
            GameSystems.GetSystem<ResourcesDayChangeSystem>().ChangeBaseProduction(produseInADay.Invert());
        }

        public override string GetDescription()
        {
            return "Produse " + produseInADay.ToString() + " in a day";
        }
    }
}
