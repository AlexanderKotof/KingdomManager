﻿using KM.Features.DayChange;
using KM.Features.Population;
using KM.Startup;
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
            _buildingSystem = AppStartup.Instance.GetSystem<BuildingSystem>();
            _populationSystem = AppStartup.Instance.GetSystem<PopulationSystem>();
            _dayChangeSystem = AppStartup.Instance.GetSystem<DayChangeSystem>();

            CreateBuildingsButtons();

            SetUpBuilders();

            buildingProgress.speedupButton.onClick.AddListener(_buildingSystem.SpeedUp);
            buildingProgress.Hide();

            _buildingSystem.onStartBuilding += OnStartBuilding;
            _buildingSystem.onBuildingProgress += OnBuildingProgress;
            _buildingSystem.onBuilded += OnBuilded;

            _dayChangeSystem.NewDayCome += OnNewDayComes;
        }



        private void SetUpBuilders()
        {
            buildersPanel.SetInfo(_populationSystem.GetPopulation(PopulationType.Builders));
        }


        private void OnStartBuilding(BuildingBase building)
        {
            buildingProgress.Show();
            buildingProgress.SetInfo(building.Icon, $"{building.ProduseTimeSec}s left", 0);
        }

        private void OnBuildingProgress(float timeLeft, float progress)
        {
            buildingProgress.UpdateProgress( $"{timeLeft}s left", progress);
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

            _buildingSystem.onStartBuilding -= OnStartBuilding;
            _buildingSystem.onBuildingProgress -= OnBuildingProgress;
            _buildingSystem.onBuilded -= OnBuilded;
        }

        private void OnNewDayComes(int obj)
        {

        }
    }
}