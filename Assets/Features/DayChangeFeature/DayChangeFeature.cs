using KM.Systems;

namespace KM.Features.DayChange
{
    public class DayChangeFeature : Feature
    {
        public float secondsInDay = 10;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new DayChangeSystem(secondsInDay, 0));
        }
    }
}