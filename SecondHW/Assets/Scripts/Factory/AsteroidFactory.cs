using UnityEngine;

namespace Asteroids
{
    public sealed class AsteroidFactory : IEnemyFactory
    {
        public Enemy Create(Health hp)
        {
            var enemy = Object.Instantiate(Resources.Load<Asteroid>("Enemy/Asteroid"));

            enemy.DependencyInjectHealth(hp);

            return enemy;
        }
    }
}