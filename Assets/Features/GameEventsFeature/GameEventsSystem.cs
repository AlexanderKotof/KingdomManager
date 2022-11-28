using KM.Features.GameEventsFeature.Events;
using KM.Features.GameEventsFeature.Events.Conditions;
using KM.Systems;
using System;

namespace KM.Features.GameEventsFeature
{
    [Serializable]
    public class GameplayEventCondition
    {
        public Condition[] conditions;
        public GameplayEvent[] activateEvents;
    }

    public class GameEventsSystem : ISystem
    {
        public GameplayEvent[] GameplayEvents = new GameplayEvent[0];

        public GameEventsSystem(GameEventsFeature gameplayEventsFeature)
        {
            GameplayEvents = gameplayEventsFeature.GameplayEvents.ToArray();
        }

        public void Initialize()
        {
            foreach(var @event in GameplayEvents)
            {
                @event.Initialize();
            }
        }

        public void Destroy()
        {

        }
    }
}