using KM.Features.GameEventsFeature.Events;
using KM.Features.GameEventsFeature.Events.Bonuses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KM.Features.GameEventsFeature.Events
{
    [CreateAssetMenu(menuName = "Events/Bonus")]
    public class BonusesEvent : GameplayEvent
    {
        public Bonus[] bonuses;

        public override void Activate()
        {
            foreach(var bonus in bonuses)
                bonus.Activate();
        }
    }
}
