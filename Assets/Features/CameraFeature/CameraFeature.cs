using KM.Startup;
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
            AppStartup.Instance.RegisterSystem(new CameraSystem(cameraPrefab, cameraMoveSpeed));
            AppStartup.Instance.RegisterSystem(new RaycasterSystem(raycastLayerMask));
        }


    }
}