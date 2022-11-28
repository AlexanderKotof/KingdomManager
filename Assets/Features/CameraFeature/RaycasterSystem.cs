using KM.Features.BattleFeature.BattleSystem3d;
using KM.Features.Raycasterfeature.Raycaster;
using KM.Systems;
using System;
using UnityEngine;

namespace KM.Features.CameraFeature
{
    public class RaycasterSystem : ISystem
    {
        public Camera raycastCamera;
        public LayerMask layerMask;

        private readonly RaycastHit[] _results = new RaycastHit[2];

        public BattleUnitEntity spawnUnit;

        public event Action<RaycastData> OnClick;

        public bool active;

        public RaycasterSystem(LayerMask layerMask)
        {
            this.layerMask = layerMask;
        }

        public void Initialize()
        {
            raycastCamera = GameSystems.GetSystem<CameraSystem>().MainCamera;
        }

        public void Raycast(Vector3 pointerPosition)
        {
            var ray = raycastCamera.ScreenPointToRay(pointerPosition);
            if (Physics.RaycastNonAlloc(ray, _results, 1000, layerMask) > 0)
            {
                foreach (var res in _results)
                {
                    if (res.collider != null && res.collider.TryGetComponent(out Unit unit))
                    {
                        // select unit
                        OnClick?.Invoke(new RaycastData { selectUnit = unit });
                        return;
                    }
                }

                OnClick?.Invoke(new RaycastData { raycastHit = _results[0] });
            }
        }

        public void Destroy()
        {

        }
    }
}