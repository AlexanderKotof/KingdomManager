using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Bonuses
{
    public abstract class Bonus : ScriptableObject
    {
        public abstract void Activate();
        public abstract void Deactivate();

        public abstract string GetDescription();
    }
}
