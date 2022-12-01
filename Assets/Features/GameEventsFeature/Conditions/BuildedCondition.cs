using KM.Systems;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Conditions
{
    [CreateAssetMenu(menuName = "Events/Conditions/Building Builded")]
    public class BuildedCondition : Condition
    {
        public BuildingBase building;

        private BuildingSystem _buildSystem;

        public override void Initialize()
        {
            _buildSystem = GameSystems.GetSystem<BuildingSystem>();
            _buildSystem.Builded += BuildManager_onBuilded;
        }

        private void BuildManager_onBuilded(BuildingBase obj)
        {
            if (obj.Hash == building.Hash)
            {
                Debug.Log(building.name + " builded!"); 
                Satisfy();
                _buildSystem.Builded -= BuildManager_onBuilded;
            }
        }


    }
}