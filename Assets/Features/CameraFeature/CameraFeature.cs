using KM.Core.Features;
using KM.Systems;
using UnityEngine;

namespace KM.Features.CameraFeature
{
    public class CameraFeature : Feature
    {
        public GameObject cameraPrefab;
        public LayerMask raycastLayerMask;
        public float cameraMoveSpeed;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new CameraSystem(cameraPrefab, cameraMoveSpeed));
            GameSystems.RegisterSystem(new RaycasterSystem(raycastLayerMask));
        }


    }
}