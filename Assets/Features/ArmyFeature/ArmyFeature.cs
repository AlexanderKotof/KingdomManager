using KM.Systems;

namespace KM.Features.ArmyFeature
{
    public class ArmyFeature : Feature
    {
        public override void Initialize()
        {
            GameSystems.RegisterSystem(new ArmySystem());
            GameSystems.RegisterSystem(new ArmyRecruitSystem());
            GameSystems.RegisterSystem(new ArmyPlacementSystem());
        }
    }
}