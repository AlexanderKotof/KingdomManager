using KM.Features.GameEventsFeature.Events.Conditions;
using System;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events
{
    public abstract class GameplayEvent : ScriptableObject
    {
        public Condition[] requiredConditions = new Condition[0];
        int satisfiedConditions = 0;

        public event Action onActivated;

        public void Initialize()
        {
            foreach (var condition in requiredConditions)
            {
                condition.Initialize();
                condition.ConditionSatisfied += ConditionSatisfied;
            }
        }

        public abstract void Activate();

        public void ConditionSatisfied(Condition condition)
        {
            condition.ConditionSatisfied -= ConditionSatisfied;

            satisfiedConditions++;

            if (satisfiedConditions >= requiredConditions.Length)
            {
                onActivated?.Invoke();
                Activate();
            }
        }
    }
}