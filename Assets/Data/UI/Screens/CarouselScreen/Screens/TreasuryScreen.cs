using KM.Features.DayChange;
using KM.Features.Population;
using KM.Features.Resources;
using KM.Systems;
using KM.UI.CarouselScreen.Components;
using UnityEngine.UI;

namespace KM.UI.CarouselScreens
{
    public class TreasuryScreen : GameScreen
    {
        public Text CurrentDay;
        public Text PeoplesCount;
        public Text DayProduce;

        public ProfessionPopulationPanelComponent buildersSelector;
        public ProfessionPopulationPanelComponent farmersSelector;

        DayChangeSystem _dayChangeSystem;
        PopulationSystem _populationManager;
        ResourcesDayChangeSystem _resourcesSystem;

        protected void Start()
        {
            _populationManager = GameSystems.GetSystem<PopulationSystem>();
            _dayChangeSystem = GameSystems.GetSystem<DayChangeSystem>();
            _resourcesSystem = GameSystems.GetSystem<ResourcesDayChangeSystem>();

            CurrentDay.text = "0";
            PeoplesCount.text = _populationManager.Peoples.ToString();

            _dayChangeSystem.NewDayCome += NewDayComes;

            buildersSelector.SetInfo(_populationManager.GetPopulation(PopulationType.Builders));
            farmersSelector.SetInfo(_populationManager.GetPopulation(PopulationType.Farmers));
        }

        protected void OnDestroy()
        {
            _dayChangeSystem.NewDayCome -= NewDayComes;
        }

        private void NewDayComes(int obj)
        {
            CurrentDay.text = obj.ToString();
            PeoplesCount.text = _populationManager.Peoples.ToString();
        }

        public void Update()
        {
            string dayProduceMessage = "";
            var dayChange = _resourcesSystem.NextDayChange;
            for (int i = 0; i < dayChange.resourcesCount; i++)
            {
                dayProduceMessage += $"{(ResourceType)i}: {dayChange.resources[i]}\n";
            }

            DayProduce.text = dayProduceMessage;
        }
    }
}
