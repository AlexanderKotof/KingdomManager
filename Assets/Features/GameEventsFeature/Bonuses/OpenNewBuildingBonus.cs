using KM.Startup;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Bonuses
{
    [CreateAssetMenu(menuName = "Game Entities/Events/Bonuses/Unlock Building")]
    public class OpenNewBuildingBonus : Bonus
    {
        public BuildingBase building;

        public override void Activate()
        {
            Debug.Log("Activate " + name);
            AppStartup.Instance.GetSystem<BuildingSystem>().ReadyToBuild.Add(building);
        }

        public override void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public override string GetDescription()
        {
            return "Allows build: " + building.Name;
        }
    }
}
