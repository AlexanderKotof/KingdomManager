using System;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.BattleFeature.BattleSystem3d
{
    public class Unit : MonoBehaviour
    {
        public float health = 100;
        public Fraction fraction;

        public IUnitPrototype prototype;

        public float damage = 10;
        public float attackDistance = 1;

        public bool canMove = true;
        public bool canAttack = true;

        public List<Unit> enemies;

        public Action<Unit> Destroyed;

        public enum Fraction
        {
            Fraction1,
            Fraction2,
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Destroyed?.Invoke(this);
            }
        }
    }
}
