using KM.Features.Population;
using KM.Startup;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Bonuses
{
    [CreateAssetMenu(menuName = "Game Entities/Events/Bonuses/Change Population Max")]
    public class PopulationMaxCountChange : Bonus
    {
        public PopulationType type;
        public int maxValueChange;

        public override void Activate()
        {
            AppStartup.Instance.GetSystem<PopulationSystem>().GetPopulation(type).maxCount += maxValueChange;
        }

        public override void Deactivate()
        {
            AppStartup.Instance.GetSystem<PopulationSystem>().GetPopulation(type).maxCount -= maxValueChange;
        }

        public override string GetDescription()
        {
            return $"Increase max populaion of {type} by {maxValueChange}";
        }
    }
}
