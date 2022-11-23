using KM.Features.DayChange;
using KM.Startup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KM.Features.GameEventsFeature.Events.Conditions
{
    [CreateAssetMenu(menuName = "Events/Conditions/Day")]
    public class DayCondition : Condition
    {
        public int Day = 0;
        public override void Initialize()
        {
            AppStartup.Instance.GetSystem<DayChangeSystem>().NewDayCome += Instance_onNewDayComes;
        }

        private void Instance_onNewDayComes(int obj)
        {
            if (obj >= Day)
            {
                Satisfy();
                AppStartup.Instance.GetSystem<DayChangeSystem>().NewDayCome -= Instance_onNewDayComes;
            }
        }
    }
}