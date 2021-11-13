using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public sealed class Health
    {
        [SerializeField] float _maxHP;
        [SerializeField] float _current;

        public float Max { get => _maxHP; }
        public float Current { get => _current; private set => _current = value; }

        public void ChangeCurrentHealth(float hp)
        {
            Current = hp;
        }

        public void GetDamage(float damage)
        {
            Current -= damage;
        }
    }
}
