using KM.Core;
using KM.Features.DayChange;
using KM.Features.Population;
using KM.Features.Resources;
using KM.Startup;
using KM.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : ISystem
{
    public List<BuildingBase> BuildedBuildings;
    public List<BuildingBase> ReadyToBuild;
    private bool building;
    float ProductionCost = 0;

    public event Action<BuildingBase> onStartBuilding;
    public event Action<float, float> onBuildingProgress;
    public event Action<BuildingBase> onBuilded;

    public class BuildingInProgress
    {

    }

    private BuildingBase _currentBuildingProgress;

    private PopulationSystem _populationSystem;
    private ResourcesSystem _resourcesSystem;
    private DayChangeSystem _dayChangeSystem;

    private PopulationData _buildersPopulation;

    public BuildingSystem(List<BuildingBase> readyToBuild)
    {
        ReadyToBuild = new List<BuildingBase>(readyToBuild);
        BuildedBuildings = new List<BuildingBase>();
    }

    public void Initialize()
    {
        _populationSystem = GameSystems.GetSystem<PopulationSystem>();
        _resourcesSystem = GameSystems.GetSystem<ResourcesSystem>();
        _dayChangeSystem = GameSystems.GetSystem<DayChangeSystem>();

        _buildersPopulation = _populationSystem.GetPopulation(PopulationType.Builders);
    }

    public void Destroy()
    {

    }


    public void Build(BuildingBase building)
    {
        Build(ReadyToBuild.IndexOf(building));
    }

    private void Build(int index)
    {
        if(index < 0 || index >= ReadyToBuild.Count)
        {
            Debug.Log("No Building in building list");
            return;
        }
        if(_currentBuildingProgress!=null)
        {
            Debug.Log("Already Builds!");
            return;
        }

        _currentBuildingProgress = ReadyToBuild[index];

        if (_resourcesSystem.Resources.HasResources(_currentBuildingProgress.ProduseCost))
        {
            _resourcesSystem.ChangeResources(_currentBuildingProgress.ProduseCost.Invert());
        }
        else
        {
            Debug.Log("Dont have resources!");
            _currentBuildingProgress = null;
            return;
        }

        Coroutines.Run(BuildingProgress());
    }

    public void SpeedUp()
    {
        var resources = new ResourceStorage(0, 0, 0, 0, 0, -1);
        _resourcesSystem.ChangeResources(resources);
        ProductionCost = 0;
    }

    public bool IsBuilded(BuildingBase building)
    {
        return BuildedBuildings.Contains(building);
    }

    public void CancelBuilding()
    {
        _resourcesSystem.ChangeResources(_currentBuildingProgress.ProduseCost);

        Coroutines.Stop(BuildingProgress());

        onBuilded?.Invoke(null);

        _currentBuildingProgress = null;

    }

    IEnumerator BuildingProgress()
    {
        ProductionCost = _currentBuildingProgress.ProduseTimeSec;

        onStartBuilding?.Invoke(_currentBuildingProgress);

        var secondsGone = 0;
        var requiredTime = ProductionCost / Builders;
        while (ProductionCost > 0)
        {
            yield return new WaitForSeconds(1);

            ProductionCost -= Builders;
            var secondsLeft = ProductionCost / (Builders + 1);
            secondsGone += 1;
            onBuildingProgress?.Invoke(secondsLeft, (_currentBuildingProgress.ProduseTimeSec - secondsLeft) / _currentBuildingProgress.ProduseTimeSec);         
        }

        onBuildingProgress?.Invoke(0, 1);

        BuildingBuilded(_currentBuildingProgress);

        _currentBuildingProgress = null;
    }

    private void BuildingBuilded(BuildingBase building)
    {
        ReadyToBuild.Remove(building);
        BuildedBuildings.Add(building);

        if (building.HasUpgrade())
            ReadyToBuild.Add(building.buildingUpgrade);

        foreach(var bonus in building.ProvideBonuses)
            bonus.Activate();

        onBuilded?.Invoke(building);
    }

    private int Builders
    {
        get
        {
            return _buildersPopulation.Count;
        }
    }
}
