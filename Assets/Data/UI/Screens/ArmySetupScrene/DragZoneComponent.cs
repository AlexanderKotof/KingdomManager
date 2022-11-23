using KM.Features.CameraFeature;
using KM.Startup;
using ScreenSystem.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KM.UI.ArmySetupScreen
{
    public class DragZoneComponent : WindowComponent, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        private bool _isDraging;

        private RaycasterSystem _raycasterSystem;
        private CameraSystem _cameraSystem;

        protected override void OnShow()
        {
            _raycasterSystem = AppStartup.Instance.GetSystem<RaycasterSystem>();
            _cameraSystem = AppStartup.Instance.GetSystem<CameraSystem>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDraging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = new Vector3(-eventData.delta.x, 0, -eventData.delta.y);
            _cameraSystem.MoveCamera(delta);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDraging = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isDraging)
                return;

            _raycasterSystem.Raycast(eventData.position);
        }
    }
}