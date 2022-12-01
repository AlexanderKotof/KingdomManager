using KM.Features.CameraFeature;
using KM.Systems;
using KM.UI.ArmySetupScreen;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KM.UI.BattleBeginsScreen
{
    public class InBattleScreen : BaseScreen
    {
        public ButtonComponent speedUpButton;
        public DragZoneComponent dragZone;

        private RaycasterSystem _raycasterSystem;
        private CameraSystem _cameraSystem;

        protected override void OnInit()
        {
            base.OnInit();

            dragZone.OnDragging += OnDragging;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            dragZone.OnDragging -= OnDragging;
        }

        protected override void OnShow()
        {
            base.OnShow();

            _raycasterSystem = GameSystems.GetSystem<RaycasterSystem>();
            _cameraSystem = GameSystems.GetSystem<CameraSystem>();
        }

        private void OnDragging(PointerEventData eventData)
        {
            var delta = new Vector3(-eventData.delta.x, 0, -eventData.delta.y);
            _cameraSystem.MoveCamera(delta);
        }
    }
}
