using System;
using static KM.Features.BattleFeature.BattleSystem3d.BattleSystem;

namespace KM.Startup
{

    [Serializable]
        public struct StateInfo
        {
            public GameState state;
            public BattleInfo battleInfo;
        }


}