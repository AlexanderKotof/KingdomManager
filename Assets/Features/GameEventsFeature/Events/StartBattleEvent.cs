using KM.Features.ArmyFeature;
using KM.Features.Resources;
using KM.Startup;
using UnityEngine;
using static KM.Features.BattleFeature.BattleSystem3d.BattleSystem;

namespace KM.Features.GameEventsFeature.Events
{
    [CreateAssetMenu(menuName = "Events/Start Battle")]
    public class StartBattleEvent : GameplayEvent
    {
        public Army EnemyesWave;

        public bool Imediate = false;

        public ResourceStorage WinPrize;

        public override void Activate()
        {
            var battleInfo = new BattleInfo { enemies = new Army(EnemyesWave) };
            AppStartup.Instance.GetSystem<BattleFeature.BattleSystem3d.BattleSystem>().BeginBattle(battleInfo);
        }
    }

}
