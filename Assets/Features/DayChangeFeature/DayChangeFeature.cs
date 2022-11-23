using KM.Startup;

namespace KM.Features.DayChange
{
    public class DayChangeFeature : Feature
    {
        public float secondsInDay = 10;

        public override void Initialize()
        {
            AppStartup.Instance.RegisterSystem(new DayChangeSystem(secondsInDay, 0));
        }
    }
}