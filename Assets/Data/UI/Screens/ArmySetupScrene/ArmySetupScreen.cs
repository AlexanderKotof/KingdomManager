using KM.Features.ArmyFeature;
using KM.Startup;
using ScreenSystem;
using ScreenSystem.Components;
using ScreenSystem.Screens;

namespace KM.UI.ArmySetupScreen
{
    public class ArmySetupScreen : BaseScreen
    {
        private bool _isSetuping;

        public CheckboxButtonComponent switchStateButton;

        public WindowComponent setupStateView;
        public DragZoneComponent dragZone;
        public ListComponent unitsListComponent;

        private ArmySystem _armySystem;
        private ArmyTacticSystem _armyTacticSystem;

        protected override void OnShow()
        {
            switchStateButton.SetCallback(SwitchState);

            _armySystem = AppStartup.Instance.GetSystem<ArmySystem>();
            _armyTacticSystem = AppStartup.Instance.GetSystem<ArmyTacticSystem>();

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
            _isSetuping = setuping;

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