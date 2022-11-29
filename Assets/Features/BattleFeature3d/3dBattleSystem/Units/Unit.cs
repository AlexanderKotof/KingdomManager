using System;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public class Unit : MonoBehaviour
    {
        public IUnitPrototype Prototype { get; private set; }

        public float Health { get; private set; }
        public float Damage { get; private set; }
        public float AttackDistance { get; private set; }

        public bool canMove = true;
        public bool canAttack = true;

        public Fraction fraction;

        public List<Unit> enemies;
        public Unit target;

        public Action<Unit> Destroyed;
        public float lastAttackTime;

        public bool IsDead => Health <= 0;

        public enum Fraction
        {
            Fraction1,
            Fraction2,
        }

        public void SetPrototype(IUnitPrototype prototype)
        {
            Prototype = prototype;

            if (prototype is BattleUnitEntity entity)
            {
                Health = entity.Health;
                Damage = entity.AttackDamage;
                AttackDistance = entity.AttackDistance;
            }
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Destroyed?.Invoke(this);
            }
        }
    }
}
