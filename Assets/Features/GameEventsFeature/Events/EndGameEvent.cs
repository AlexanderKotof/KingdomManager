using KM.Startup;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events
{
    [CreateAssetMenu(menuName = "Events/End game")]
    public class EndGameEvent : GameplayEvent
    {
        public string message;

        public override void Activate()
        {
            Debug.LogWarning("Game End Event: " + message);
            AppStartupUtils.EndGame(message);
        }
    }
}