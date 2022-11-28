using KM.Systems;
using UnityEngine;

namespace KM.Features.ArmyFeature
{
    public class ArmyFeature : Feature
    {
        public override void Initialize()
        {
            GameSystems.RegisterSystem(new ArmySystem());
            GameSystems.RegisterSystem(new ArmyTacticSystem());
        }
    }
}