using UnityEngine;

namespace Asteroids
{
    public abstract class Enemy : MonoBehaviour
    {
        public static IEnemyFactory Factory;
        public Health Health { get; protected set; }

        public void DependencyInjectHealth(Health hp)
        {
            Health = hp;
        }
    }
}