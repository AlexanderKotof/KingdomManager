using KM.Features.BattleFeature.BattleSystem3d;
using KM.UI.Utils;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using System;

namespace KM.UI.BattleBeginsScreen
{
    public class BattleOverScreen : BaseScreen
    {
        public ListComponent enemiesList;
        public ButtonComponent closeButton;

        private BattleSystem _battleSystem;

        public void SetBattleInfo(BattleInfo battle)
        {
            //enemiesList.SetItems<>();
            closeButton.SetCallback(() =>
            {
                Hide();
                UIUtils.ShowScreensOnBattleEnds(battle);
            });
        }
    }
}
