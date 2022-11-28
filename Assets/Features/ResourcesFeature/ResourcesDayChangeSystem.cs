using KM.Features.ArmyFeature;
using KM.Features.DayChange;
using KM.Features.Population;
using KM.Systems;
using UnityEngine;

namespace KM.Features.Resources
{
    public class ResourcesDayChangeSystem : ISystem
    {
        public ResourceStorage BaseDayChange { get; private set; }
        public ResourceStorage NextDayChange => _dayChange;

        private ResourceStorage _dayChange;

        private ResourcesSystem _resourceSystem;
        private PopulationSystem _populationSystem;
        private DayChangeSystem _dayChangeSystem;
        private ArmySystem _armySystem;

        private float _foodProductionModifier;
        private float _goldProductionModifier;

        public ResourcesDayChangeSystem(ResourcesFeature feature)
        {
            BaseDayChange = new ResourceStorage(feature.baseDayChange);
            _foodProductionModifier = feature.foodProductionByFarmersModifier;
            _goldProductionModifier = feature.goldProductionModifier;
        }

        public void Initialize()
        {
            _resourceSystem = GameSystems.GetSystem<ResourcesSystem>();
            _dayChangeSystem = GameSystems.GetSystem<DayChangeSystem>();
            _populationSystem = GameSystems.GetSystem<PopulationSystem>();
            _armySystem = GameSystems.GetSystem<ArmySystem>();

            UpdateDayChange();

            _dayChangeSystem.NewDayCome += OnNewDayCome;
        }

        public void Destroy()
        {
            _dayChangeSystem.NewDayCome -= OnNewDayCome;
        }

        private void OnNewDayCome(int day)
        {
            _resourceSystem.ChangeResources(_dayChange);

            UpdateDayChange();
        }

        private void UpdateDayChange()
        {
            _dayChange = new ResourceStorage(BaseDayChange);

            _dayChange.ChangeResources(GetDayProduction());

            _dayChange.ChangeResources(GetDayConsuming());
        }

        public ResourceStorage GetDayProduction()
        {
            var dayChange = new ResourceStorage();

            var farmersCount = _populationSystem.GetPopulation(PopulationType.Farmers).Count;
            dayChange.resources[(int)ResourceType.Food] = Mathf.FloorToInt( farmersCount * _foodProductionModifier + Random.value);

            var workersCount = _populationSystem.GetPopulation(PopulationType.Builders).Count + _populationSystem.GetPopulation(PopulationType.Scientists).Count;
            dayChange.resources[(int)ResourceType.Gold] = Mathf.FloorToInt(workersCount * _goldProductionModifier + Random.value);

            return dayChange;
        }

        public ResourceStorage GetDayConsuming()
        {
            var dayChange = new ResourceStorage();

            var armiesCount = _armySystem.GetAllUnitsCount();
            var peoplesCount = _populationSystem.People;

            dayChange.resources[(int)ResourceType.Food] -= Mathf.FloorToInt(peoplesCount + armiesCount);
            dayChange.resources[(int)ResourceType.Gold] -= Mathf.FloorToInt(armiesCount);

            return dayChange;
        }

        public void ChangeBaseProduction(ResourceStorage resources)
        {
            BaseDayChange.ChangeResources(resources);
        }
    }
}