using KM.Features.CameraFeature;
using KM.Systems;
using ScreenSystem.Components;
using System;
using UnityEngine.EventSystems;

namespace KM.UI.ArmySetupScreen
{
    public class DragZoneComponent : WindowComponent, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        private bool _isDraging;

        public event Action<PointerEventData> OnDragBegin;
        public event Action<PointerEventData> OnDragging;
        public event Action<PointerEventData> OnDragEnds;

        public event Action<PointerEventData> OnClick;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDraging = true;
            OnDragBegin?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragging?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDraging = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isDraging)
            {
                OnDragEnds?.Invoke(eventData);
                return;
            }

            OnClick?.Invoke(eventData);
        }
    }
}