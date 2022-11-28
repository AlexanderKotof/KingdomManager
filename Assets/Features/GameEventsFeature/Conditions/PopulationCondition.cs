using KM.Features.Population;
using KM.Systems;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Conditions
{
    [CreateAssetMenu(menuName = "Events/Conditions/Population")]
    public class PopulationCondition : Condition
    {
        public LessMore lessOrMore;
        public int PopulationRequired = 10;
        public enum LessMore
        {
            LessThan,
            MoreThan,
        }

        private PopulationSystem _populationSystem;

        public override void Initialize()
        {
            _populationSystem = GameSystems.GetSystem<PopulationSystem>();
            _populationSystem.PopulationChanged += PopulationCondition_PopulationChanged;
        }

        private void PopulationCondition_PopulationChanged()
        {
            if(lessOrMore == LessMore.LessThan && _populationSystem.People < PopulationRequired || 
                lessOrMore == LessMore.MoreThan && _populationSystem.People > PopulationRequired)
            {
                _populationSystem.PopulationChanged -= PopulationCondition_PopulationChanged;
                Satisfy();
            }
        }
    }
}