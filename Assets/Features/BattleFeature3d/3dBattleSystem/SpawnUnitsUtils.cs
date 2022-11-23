using UnityEngine;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public static class SpawnUnitsUtils
    {
        public static Unit SpawnUnit(IUnitPrototype prototype, Vector3 position)
        {
            var unitGo = Object.Instantiate(prototype.Prefab, position, Quaternion.identity);
            unitGo.prototype = prototype;
            return unitGo;
        }
    }
}
