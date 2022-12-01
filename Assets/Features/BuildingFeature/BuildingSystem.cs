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

    public event Action<BuildingBase> BuildingBegins;
    public event Action<string, float> BuildingProgress;
    public event Action<BuildingBase> Builded;

    private float _productionCostLeft;
    private BuildingBase _buildingPrototype;

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

        _dayChangeSystem.NewDayCome += OnNewDayCome;
    }

    public void Destroy()
    {
        _dayChangeSystem.NewDayCome -= OnNewDayCome;
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
        if(_buildingPrototype!=null)
        {
            Debug.Log("Already Builds!");
            return;
        }

        _buildingPrototype = ReadyToBuild[index];

        if (_resourcesSystem.Resources.HasResources(_buildingPrototype.ProduseCost))
        {
            _resourcesSystem.ChangeResources(_buildingPrototype.ProduseCost.Invert());
        }
        else
        {
            Debug.Log("Dont have resources!");
            _buildingPrototype = null;
            return;
        }

        Coroutines.Run(BuildingProgressCoroutine());
    }

    private void OnNewDayCome(int day)
    {
        if (_buildingPrototype == null)
            return;

         _productionCostLeft -= Builders;
    }

    public void SpeedUp()
    {
        var resources = new ResourceStorage(0, 0, 0, 0, 0, -1);
        _resourcesSystem.ChangeResources(resources);

        Coroutines.Stop(BuildingProgressCoroutine());
        BuildingBuilded();
    }

    public bool IsBuilded(BuildingBase building)
    {
        return BuildedBuildings.Contains(building);
    }

    public void CancelBuilding()
    {
        _resourcesSystem.ChangeResources(_buildingPrototype.ProduseCost);

        Coroutines.Stop(BuildingProgressCoroutine());

        Builded?.Invoke(null);

        _buildingPrototype = null;

    }

    IEnumerator BuildingProgressCoroutine()
    {
        _productionCostLeft = _buildingPrototype.ProduseTimeSec;

        BuildingBegins?.Invoke(_buildingPrototype);

        var startTime = Time.realtimeSinceStartup;
        while (_productionCostLeft > 0)
        {
            yield return null;

            if (Builders == 0)
            {
                BuildingProgress?.Invoke("Indefinite", 0);
                continue;
            }

            var secondsLeft = Mathf.CeilToInt(_productionCostLeft / Builders);
            var secondsGone = Time.realtimeSinceStartup - startTime;

            BuildingProgress?.Invoke(secondsLeft.ToString(), (secondsGone) / (secondsGone + secondsLeft));
        }

        BuildingBuilded();
    }

    private void BuildingBuilded()
    {
        if (_buildingPrototype == null)
            return;

        ReadyToBuild.Remove(_buildingPrototype);
        BuildedBuildings.Add(_buildingPrototype);

        if (_buildingPrototype.HasUpgrade())
            ReadyToBuild.Add(_buildingPrototype.buildingUpgrade);

        foreach(var bonus in _buildingPrototype.ProvideBonuses)
            bonus.Activate();

        Builded?.Invoke(_buildingPrototype);

        _buildingPrototype = null;
    }

    private int Builders
    {
        get
        {
            return _buildersPopulation.Count;
        }
    }
}
