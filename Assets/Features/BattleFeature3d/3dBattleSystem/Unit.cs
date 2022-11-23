using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.BattleFeature.BattleSystem3d
{

    public class Unit : MonoBehaviour
    {
        public float health = 100;
        public Fraction fraction;

        public IUnitPrototype prototype;

        public Action<Unit> Destroyed;

        public enum Fraction
        {
            Fraction1,
            Fraction2,
        }


        private void Start()
        {

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
