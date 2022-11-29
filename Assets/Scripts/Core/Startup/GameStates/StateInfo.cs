using KM.Features.BattleFeature.BattleSystem3d;
using System;

namespace KM.Startup
{

    [Serializable]
    public struct StateInfo
    {
        public GameState state;
        public BattleInfo battleInfo;
    }

}