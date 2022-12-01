using KM.Features.DayChange;
using KM.Features.Population;
using KM.Systems;
using KM.UI.CarouselScreen.Components;
using KM.UI.Components;
using ScreenSystem;
using ScreenSystem.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KM.UI.CarouselScreens
{
    public class BuildingsScreen : GameScreen
    {
        public ProductionViewComponent buildingProgress;

        public ListComponent buildingsList;
        List<BuildingBase> allBuildings;

        public ProfessionPopulationPanelComponent buildersPanel;

        BuildingSystem _buildingSystem;
        PopulationSystem _populationSystem;
        DayChangeSystem _dayChangeSystem;

        protected void Start()
        {
            _buildingSystem = GameSystems.GetSystem<BuildingSystem>();
            _populationSystem = GameSystems.GetSystem<PopulationSystem>();
            _dayChangeSystem = GameSystems.GetSystem<DayChangeSystem>();

            CreateBuildingsButtons();

            SetUpBuilders();

            buildingProgress.speedupButton.onClick.AddListener(_buildingSystem.SpeedUp);
            buildingProgress.Hide();

            _buildingSystem.BuildingBegins += OnStartBuilding;
            _buildingSystem.BuildingProgress += OnBuildingProgress;
            _buildingSystem.Builded += OnBuilded;

            _dayChangeSystem.NewDayCome += OnNewDayComes;
        }



        private void SetUpBuilders()
        {
            buildersPanel.SetInfo(_populationSystem.GetPopulation(PopulationType.Builders));
        }


        private void OnStartBuilding(BuildingBase building)
        {
            buildingProgress.Show();
            buildingProgress.SetInfo(building.Icon, $"", 0);
        }

        private void OnBuildingProgress(string timeLeft, float progress)
        {
            buildingProgress.UpdateProgress( $"{timeLeft} days left", progress);
        }

        private void OnBuilded(BuildingBase obj)
        {
            CreateBuildingsButtons();
     
            buildingProgress.Hide();
        }

        void CreateBuildingsButtons()
        {
            allBuildings = new List<BuildingBase>(_buildingSystem.BuildedBuildings);
            allBuildings.AddRange(_buildingSystem.ReadyToBuild);

            buildingsList.SetItems<EntityButton>(allBuildings.Count, (item, param) =>
            {
                var building = allBuildings[param.index];

                var isBuilded = _buildingSystem.IsBuilded(building);

                item.SetBuildingInfo(building, (ent) => {
                    ScreensManager.ShowScreen<EntityInfoScreen>().ShowBuildingUI(building, isBuilded);
                }, isBuilded);
            });

        }



        private void OnDestroy()
        {

            _dayChangeSystem.NewDayCome -= OnNewDayComes;

            _buildingSystem.BuildingBegins -= OnStartBuilding;
            _buildingSystem.BuildingProgress -= OnBuildingProgress;
            _buildingSystem.Builded -= OnBuilded;
        }

        private void OnNewDayComes(int obj)
        {

        }
    }
}
