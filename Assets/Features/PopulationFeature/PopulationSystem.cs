using KM.Features.DayChange;
using KM.Features.Resources;
using KM.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.Population
{
    public enum PopulationType
    {
        Builders,
        Farmers,
        Scientists,
    }

    public class PopulationSystem : ISystem
    {
        public int Peoples { get; private set; }
        public int DayGrowth { get; private set; }
        public int FreePeoples
        {
            get
            {
                var populationsCount = 0;
                foreach (var population in _populationsByTypes.Values)
                {
                    populationsCount += population.Count;
                }

                return Peoples - populationsCount;
            }
        }
        public int BusyPeoples
        {
            get
            {
                var busyCount = 0;
                foreach (var pop in _populationsByTypes.Values)
                {
                    busyCount += pop.Count;
                }
                return busyCount;
            }
        }

        private readonly Dictionary<PopulationType, PopulationData> _populationsByTypes;

        private const float _bornChance = 0.3f;
        private const float _dieChance = 0.2f;

        private DayChangeSystem _dayChangeSystem;
        private ResourcesSystem _resourceSystem;

        public event Action PopulationChanged;

        public PopulationSystem(PopulationFeature feature)
        {
            Peoples = feature.PeoplesOnStart;
            DayGrowth = feature.BaseDayGrowth;

            _populationsByTypes = new Dictionary<PopulationType, PopulationData>();
            for (int i = 0; i < feature.populations.Length; i++)
            {
                _populationsByTypes.Add(feature.populations[i].type, new PopulationData(feature.populations[i]));
            }
        }
        public void Initialize()
        {
            _dayChangeSystem = GameSystems.GetSystem<DayChangeSystem>();
            _resourceSystem = GameSystems.GetSystem<ResourcesSystem>();

            _dayChangeSystem.NewDayCome += OnNewDayCome;
        }

        public void Destroy()
        {
            _dayChangeSystem.NewDayCome -= OnNewDayCome;
        }

        public void ChangePeoples(int delta)
        {
            Peoples += delta;
            Peoples = Mathf.Clamp(Peoples, 0, int.MaxValue);

            PopulationChanged?.Invoke();
        }

        public void ChangePopulation(PopulationType type, int delta)
        {
            var population = GetPopulation(type);

            if (delta > FreePeoples)
            {
                return;
            }

            population.Count += delta;

            PopulationChanged?.Invoke();
        }

        public PopulationData GetPopulation(PopulationType type)
        {
            if (_populationsByTypes.TryGetValue(type, out var population))
                return population;

            Debug.LogError("Population not founded!");
            return null;
        }

        private void OnNewDayCome(int day)
        {
            var resources = new ResourceStorage(Peoples - (int)(FreePeoples * 0.5f));
            if (_resourceSystem.Resources.HasResources(resources))
            {
                var peoplesDelta = UnityEngine.Random.value < _bornChance ? DayGrowth : 0;
                ChangePeoples(peoplesDelta);
            }
            else
            {
                var peoplesDelta = UnityEngine.Random.value < _dieChance ? -DayGrowth : 0;
                ChangePeoples(peoplesDelta);
            }
        }
    }
}