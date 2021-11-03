using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        public Health Health { get; protected set; }
        public bool IsOnScene { get; set; }

        public void DependencyInjectHealth(float maxHP)
        {
            Health = new Health(maxHP);
        }
    }
}