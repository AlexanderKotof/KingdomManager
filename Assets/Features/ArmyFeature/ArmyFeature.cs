using KM.Startup;
using UnityEngine;

namespace KM.Features.ArmyFeature
{
    public class ArmyFeature : Feature
    {
        public override void Initialize()
        {
            AppStartup.Instance.RegisterSystem(new ArmySystem());
            AppStartup.Instance.RegisterSystem(new ArmyTacticSystem());
        }
    }
}