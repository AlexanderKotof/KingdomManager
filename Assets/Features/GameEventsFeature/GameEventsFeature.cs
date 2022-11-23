using KM.Features.GameEventsFeature.Events;
using KM.Startup;
using System.Collections.Generic;

namespace KM.Features.GameEventsFeature
{
    public class GameEventsFeature : Feature
    {
        public List<GameplayEvent> GameplayEvents = new List<GameplayEvent>();

        public override void Initialize()
        {
            AppStartup.Instance.RegisterSystem(new GameEventsSystem(this));
        }
    }
}