using System;
using UnityEngine;
namespace KM.Features.GameEventsFeature.Events.Conditions
{
    public abstract class Condition : ScriptableObject
    {
        public event Action<Condition> ConditionSatisfied;

        public abstract void Initialize();

        public void Satisfy()
        {
            Debug.Log(name + " condition satisfied");
            ConditionSatisfied?.Invoke(this);
        }
    }
}


