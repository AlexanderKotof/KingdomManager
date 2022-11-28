using KM.Features.GameEventsFeature.Events;
using KM.Systems;
using System.Collections.Generic;

namespace KM.Features.GameEventsFeature
{
    public class GameEventsFeature : Feature
    {
        public List<GameplayEvent> GameplayEvents = new List<GameplayEvent>();

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new GameEventsSystem(this));
        }
    }
}