using KM.Features.GameEventsFeature.Events;
using KM.Features.Resources;
using KM.Systems;
using UnityEngine;
namespace KM.Features.GameEventsFeature.Events
{
    [CreateAssetMenu(menuName = "Events/Resources Change")]
    public class TakeResources : GameplayEvent
    {
        public ResourceStorage resources;

        public override void Activate()
        {
            GameSystems.GetSystem<ResourcesSystem>().ChangeResources(resources);

            Debug.Log("TakeResources activated");
        }
    }
}
