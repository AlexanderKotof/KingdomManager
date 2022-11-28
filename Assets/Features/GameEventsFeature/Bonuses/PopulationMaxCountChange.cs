using KM.Features.Population;
using KM.Systems;
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
            GameSystems.GetSystem<PopulationSystem>().GetPopulation(type).maxCount += maxValueChange;
        }

        public override void Deactivate()
        {
            GameSystems.GetSystem<PopulationSystem>().GetPopulation(type).maxCount -= maxValueChange;
        }

        public override string GetDescription()
        {
            return $"Increase max populaion of {type} by {maxValueChange}";
        }
    }
}
