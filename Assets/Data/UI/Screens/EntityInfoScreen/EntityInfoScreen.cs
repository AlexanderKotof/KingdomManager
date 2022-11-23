using KM.Features.ArmyFeature;
using KM.Startup;
using ScreenSystem.Screens;
using UnityEngine.UI;

namespace KM.UI
{
    public class EntityInfoScreen : BaseScreen
    {
        public Image icon;

        public Text nameText;

        public Text countText;

        public Text descriptionText;

        public Text CostText;

        public Button BuyButton;
        public Button CloseButton;

        private GameEntity _showEntity;

        protected override void OnInit()
        {

        }

        public void ShowUnitUI(BattleUnitEntity unit, bool canRecruit, int count = 0)
        {
            _showEntity = unit;

            icon.sprite = unit.Icon;

            countText.text = count > 0 ? count.ToString() : "";

            nameText.text = unit.name;

            descriptionText.text = unit.GetDescription();

            CostText.text = canRecruit ? unit.ProduseCost.ToString() : "";

            BuyButton.gameObject.SetActive(canRecruit);
            BuyButton.GetComponentInChildren<Text>().text = "Recruit";

        }

        public void ShowBuildingUI(BuildingBase building, bool builded)
        {
            _showEntity = building;

            icon.sprite = building.Icon;

            nameText.text = building.name;

            descriptionText.text = building.GetDescription();
            CostText.text = !builded ? building.ProduseCost.ToString() : "";

            BuyButton.gameObject.SetActive(!builded);
            BuyButton.GetComponentInChildren<Text>().text = "Build";
        }

        private void OnBuyPressed()
        {
            if (_showEntity == null)
                return;

            if (_showEntity is BuildingBase building)
            {
                AppStartup.Instance.GetSystem<BuildingSystem>().Build(building);
                Hide();
            }
            else if (_showEntity is BattleUnitEntity unit)
            {
                AppStartup.Instance.GetSystem<ArmySystem>().RecruitUnit(unit);
            }
        }

        protected override void OnShow()
        {
            CloseButton.onClick.AddListener(Hide);
            BuyButton.onClick.AddListener(OnBuyPressed);

        }

        protected override void OnHide()
        {
            CloseButton.onClick.RemoveListener(Hide);
            BuyButton.onClick.RemoveListener(OnBuyPressed);
        }


    }
}