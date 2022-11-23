using KM.Features.Population;
using KM.Startup;
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
            _populationSystem = AppStartup.Instance.GetSystem<PopulationSystem>();
            _populationSystem.PopulationChanged += PopulationCondition_PopulationChanged;
        }

        private void PopulationCondition_PopulationChanged()
        {
            if(lessOrMore == LessMore.LessThan && _populationSystem.Peoples < PopulationRequired || 
                lessOrMore == LessMore.MoreThan && _populationSystem.Peoples > PopulationRequired)
            {
                _populationSystem.PopulationChanged -= PopulationCondition_PopulationChanged;
                Satisfy();
            }
        }
    }
}