using KM.Features.Population;
using KM.Features.Resources;
using KM.Startup;
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

    BuildingBase NowBuilds;

    private PopulationSystem _populationSystem;
    private ResourcesSystem _resourcesSystem;

    private PopulationData _buildersPopulation;

    public BuildingSystem(List<BuildingBase> readyToBuild)
    {
        ReadyToBuild = new List<BuildingBase>(readyToBuild);
        BuildedBuildings = new List<BuildingBase>();
    }

    public void Initialize()
    {
        _populationSystem = AppStartup.Instance.GetSystem<PopulationSystem>();
        _resourcesSystem = AppStartup.Instance.GetSystem<ResourcesSystem>();

        _buildersPopulation = _populationSystem.GetPopulation(PopulationType.Builders);
    }

    public void Destroy()
    {

    }


    public void Build(BuildingBase building)
    {
        Build(ReadyToBuild.IndexOf(building));
    }

    public void Build(int index)
    {
        if(index < 0 || index >= ReadyToBuild.Count)
        {
            Debug.Log("Build error");
            return;
        }
        if(NowBuilds!=null)
        {
            Debug.Log("Already Builds!");
            return;
        }

        NowBuilds = ReadyToBuild[index];

        if (_resourcesSystem.Resources.HasResources(NowBuilds.ProduseCost))
        {
            _resourcesSystem.ChangeResources(NowBuilds.ProduseCost.Invert());
        }
        else
        {
            Debug.Log("Dont have resources!");
            NowBuilds = null;
            return;
        }

        AppStartup.Instance.StartCoroutine(BuildingProgress());
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
        _resourcesSystem.ChangeResources(NowBuilds.ProduseCost);

        AppStartup.Instance.StopCoroutine(BuildingProgress());

        onBuilded?.Invoke(null);

        NowBuilds = null;

    }

    IEnumerator BuildingProgress()
    {
        ProductionCost = NowBuilds.ProduseTimeSec;

        onStartBuilding?.Invoke(NowBuilds);

        while (ProductionCost > 0)
        {
            yield return new WaitForSeconds(1);

            ProductionCost -= Builders + 1;
            var secondsLeft = ProductionCost / (Builders + 1);
            onBuildingProgress?.Invoke(secondsLeft, (NowBuilds.ProduseTimeSec - secondsLeft) / NowBuilds.ProduseTimeSec);         
        }

        onBuildingProgress?.Invoke(0, 1);

        BuildingBuilded(NowBuilds);

        NowBuilds = null;
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
        set
        {
            _buildersPopulation.Count = value;
        }
    }
}
