using UnityEngine;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public static class SpawnUnitsUtils
    {
        public static Unit SpawnUnit(IUnitPrototype prototype, Vector3 position)
        {
            var unitGo = Unit.Instantiate<Unit>(prototype.Prefab, position, Quaternion.identity);
            unitGo.SetPrototype(prototype);
            return unitGo;
        }
    }
}
