using KM.Features.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntity : ScriptableObject
{
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    [Header("Base properties", order = 0)]
    public string Description;
    public Sprite Icon;

    [Header("Produse")]
    public int ProduseTimeSec = 10;
    public ResourceStorage ProduseCost;

    public int Hash
    {
        get
        {
            if (hash == 0)
                hash = Animator.StringToHash(name) + Animator.StringToHash(Description);

            return hash;
        }
        set
        {
            hash = value;
        }
    }
    int hash = 0;

    public override int GetHashCode()
    {
        return Hash;
    }

    public abstract string GetDescription();

    public T CreateEntityInstance<T>(T entity) where T : GameEntity
    {
        var instance = GameObject.Instantiate(entity);
        instance.name = entity.Name;
        instance.Hash = entity.Hash;

        instance.hideFlags = HideFlags.HideAndDontSave;

        return instance;
    }
}
