using UnityEngine;
using static KM.Startup.AppStartup;

namespace KM.Features.GameEventsFeature.Events
{
    [CreateAssetMenu(menuName = "Events/End game")]
    public class EndGameEvent : GameplayEvent
    {
        public string message;

        public override void Activate()
        {
            Debug.LogWarning("Game End Event: " + message);
            App.EndGame(message);
        }
    }
}