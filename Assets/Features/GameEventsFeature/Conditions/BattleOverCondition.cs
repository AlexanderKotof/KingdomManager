﻿using KM.BattleSystem;
using KM.Features.ArmyFeature;
using KM.Startup;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Conditions
{
    //[CreateAssetMenu(menuName = "Game Entities/Events/Conditions/Battle Over")]
    public class BattleOverCondition : Condition
    {
        int battleId = 0;

        public override void Initialize()
        {
            //AppStartup.Instance.GetSystem<ArmySystem>().BattleReady += BattleManager_onBattleReady;
        }

        private void BattleManager_onBattleReady()
        {
            //obj.onBattleEnded += Obj_onBattleEnded;
        }

        private void Obj_onBattleEnded(bool win)
        {
            if (win)
            {
                Satisfy();
            }
        }
    }
}