using KM.Features.GameEventsFeature.Events.Bonuses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Entities/Building")]
public class BuildingBase : GameEntity
{
    public BuildingBase buildingUpgrade;

    public Bonus[] ProvideBonuses;

    public override string GetDescription()
    {
        return Description;
    }

    public bool HasUpgrade()
    {
        return buildingUpgrade != null;
    }
}
