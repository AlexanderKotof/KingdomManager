using KM.Startup;
using UnityEngine;

namespace KM.Features.BattleFeature
{
    public class BattleFeature : Feature
    {
        public GameObject citadelPrefab;
        public Vector3 citadelPosition;

        public override void Initialize()
        {
            AppStartup.Instance.RegisterSystem(new BattleSystem3d.BattleSystem());
            AppStartup.Instance.RegisterSystem(new CitadelSystem(this));
        }
    }
}