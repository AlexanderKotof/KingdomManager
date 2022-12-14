using KM.Features.BattleFeature.BattleSystem3d;
using UnityEngine;

public enum AttackType
{
    Basic = 1,
    Ranged = 0,
    Heavy = 3
}
public enum DefenceType
{
    Light = 0,
    Midle = 1,
    Strong = 2,
    Building = 3
}


[CreateAssetMenu (menuName = "Game Entities/Battle Unit")]
public class BattleUnitEntity : GameEntity, IUnitPrototype
{
    [Header("Unit properties")]
    public AttackType AttackType;

    public DefenceType DefenceType;

    [Tooltip("How many attacks can takes")]
    public int Health = 1;

    [Tooltip("Attack damage")]
    public int AttackDamage = 1;
    [Tooltip("Good attack chance")]
    public float AttackChance = 0.5f;
    [Tooltip("Chance to reduce damage, if > 0")]
    public int Defence = 1;

    public float AttackDistance = 1f;

    public int Initiative = 1;

    public Unit Prefab { get => prefab; }
    public Unit prefab;

    public override string GetDescription()
    {
        string description = Description;

        description += "\n HP: " + Health + "  Damage: " + AttackDamage;

        if (Defence > 0)
            description += "  Defence: " + Defence;

        description += "\n Attack " + AttackType + "\n Defence " + DefenceType;

        return description;
    }
}
