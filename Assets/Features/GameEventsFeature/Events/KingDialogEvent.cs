using KM.Features.GameEventsFeature.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events
{
    [CreateAssetMenu(menuName = "Events/King Dialog")]
    public class KingDialogEvent : GameplayEvent
    {
        public Dialog dialog;

        public static event System.Action<Dialog> OnDialogActivated;

        public override void Activate()
        {
            OnDialogActivated?.Invoke(dialog);
        }


    }
}