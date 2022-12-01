using KM.Features.ArmyFeature;
using KM.Features.CameraFeature;
using KM.Systems;
using ScreenSystem;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KM.UI.ArmySetupScreen
{
    public class ArmySetupScreen : BaseScreen
    {
        public CheckboxButtonComponent switchStateButton;

        public WindowComponent setupStateView;
        public DragZoneComponent dragZone;
        public ListComponent unitsListComponent;

        private ArmySystem _armySystem;
        private ArmyPlacementSystem _armyTacticSystem;
        private RaycasterSystem _raycasterSystem;
        private CameraSystem _cameraSystem;

        protected override void OnInit()
        {
            base.OnInit();

            dragZone.OnClick += OnClick;
            dragZone.OnDragging += OnDragging;

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            dragZone.OnClick -= OnClick;
            dragZone.OnDragging -= OnDragging;
        }

        private void OnDragging(PointerEventData eventData)
        {
            var delta = new Vector3(-eventData.delta.x, 0, -eventData.delta.y);
            _cameraSystem.MoveCamera(delta);
        }

        private void OnClick(PointerEventData eventData)
        {
            _raycasterSystem.Raycast(eventData.position);
        }

        protected override void OnShow()
        {
            switchStateButton.SetCheckedState(false, true);
            switchStateButton.SetCallback(SwitchState);

            _armySystem = GameSystems.GetSystem<ArmySystem>();
            _armyTacticSystem = GameSystems.GetSystem<ArmyPlacementSystem>();
            _raycasterSystem = GameSystems.GetSystem<RaycasterSystem>();
            _cameraSystem = GameSystems.GetSystem<CameraSystem>();

            _armySystem.ArmyUpdated += UpdateList;
            UpdateList();
        }


        private void UpdateList()
        {
            var units = new Army(_armySystem.freeArmy);
            units.AddUnits(_armySystem.guardArmy);

            unitsListComponent.SetItems<EntityButton>(units.units.Count, (item, par) =>
            {
                var unit = units.units[par.index];
                var freeUnits = _armySystem.freeArmy.GetUnitCount(unit.prototype);
                var unitsInGuard = _armySystem.guardArmy.GetUnitCount(unit.prototype);
                item.SetInfo(unit.prototype, OnUnitClick, $"{freeUnits} / {unitsInGuard}");
            });
        }

        private void SwitchState(bool setuping)
        {
            _armyTacticSystem.SelectUnit(null);

            if (setuping)
            {
                ScreensManager.GetScreen<CarouselScreens.CarouselScreen>().Hide();
                dragZone.Show();
                setupStateView.Show();
            }
            else
            {
                ScreensManager.GetScreen<CarouselScreens.CarouselScreen>().Show();
                dragZone.Hide();
                setupStateView.Hide();
            }

            _armyTacticSystem.SetActive(setuping);
            UpdateList();
        }

        private void OnUnitClick(GameEntity obj)
        {
            _armyTacticSystem.SelectUnit((BattleUnitEntity)obj);
        }

        protected override void OnHide()
        {
            _armySystem.ArmyUpdated -= UpdateList;
        }

    }
}
