using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Entities/General")]
public class General : GameEntity
{
    public float EXP = 0;
    public float NextLevelEXP = 100;

    public int Level = 1;

    public int Strenght = 1;
    public int Defence = 1;
    public int Initiative = 1;

    // List <Items> inventory;

    public override string GetDescription()
    {
        return $"{Name}\nLevel {Level}\n{Description}\nStrenght {Strenght}\nDefence {Defence}\nInitiative {Initiative}";
    }
}
