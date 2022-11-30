using KM.Features.BattleFeature.BattleSystem3d;
using KM.Systems;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.UI.BattleBeginsScreen
{
    public class BattleBeginsScreen : BaseScreen
    {
        public ListComponent enemiesList;
        public ButtonComponent startBattleButton;

        private BattleSystem _battleSystem;

        public void SetBattleInfo(BattleInfo battle, System.Action callback)
        {
            //enemiesList.SetItems<>();
            startBattleButton.SetCallback(() =>
            {
                callback.Invoke();
                Hide();
            });
        }

    }
}
