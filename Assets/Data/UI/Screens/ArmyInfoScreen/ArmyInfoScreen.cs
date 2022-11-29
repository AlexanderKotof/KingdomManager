using KM.Features.ArmyFeature;
using KM.Systems;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using TMPro;
using UnityEngine.UI;

namespace KM.UI.ArmyInfoScreen
{
    public class ArmyInfoScreen : BaseScreen
    {
        private Army playerArmy;

        public ListComponent playerUnitsList;

        public Button generalInfoButton;
        public Button splitUnitButton;
        public Button closeButton;

        public Image generalIcon;
        public TMP_Text generalInfoText;

        protected override void OnInit()
        {
            playerArmy = GameSystems.GetSystem<ArmySystem>().freeArmy;
        }

        protected override void OnShow()
        {
            closeButton.onClick.AddListener(Hide);
            generalInfoButton.onClick.AddListener(ShowGeneralInfoScreen);
            splitUnitButton.onClick.AddListener(SplitUnits);

            ShowGeneralInfo();

            ShowTeam();
        }

        protected override void OnHide()
        {
            closeButton.onClick.RemoveListener(Hide);
            generalInfoButton.onClick.RemoveListener(ShowGeneralInfoScreen);
            splitUnitButton.onClick.RemoveListener(SplitUnits);
        }

        private void ShowTeam()
        {
            /*
            playerUnitsList.SetItems<EntityButton>(TurnBasedBattle.TEAM_UNITS_COUNT, (item, param) =>
            {
                if (playerArmy.units.Count <= param.index)
                    return;

                var unit = playerArmy.units[param.index];
                item.SetInfo(unit.prototype, (ent) =>
                {
                    ScreensManager.ShowScreen<EntityInfoScreen>().ShowUnitUI(unit.prototype, false, unit.count);
                }, unit.count);
            });
            */
        }

        private void ShowGeneralInfoScreen()
        {
            //if (playerArmy.general)
                //GeneralUI.ShowUI(playerArmy.general);
        }

        private void SplitUnits()
        {
            
        }

        private void ShowGeneralInfo()
        {
            if (!playerArmy.general)
                    return;

            generalIcon.sprite = playerArmy.general.Icon;
            generalInfoText.text = playerArmy.general.GetDescription();
        }
    }
}