using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids
{
    public sealed class AsteroidFactory : IEnemyFactory<Asteroid>
    {
        public Asteroid Create(AsteroidInitData initData)
        {           
            var asteroid = Object.Instantiate(initData.AsteroidPref, initData.Position, Quaternion.Euler(initData.Rotation.x, initData.Rotation.y, 0));
            asteroid.DependencyInjectHealth(initData.Hp);

            return asteroid;
        }

        public Asteroid Create(Asteroid _enemyPref, Vector3 position, Quaternion rotation, float hp)
        {
            var asteroid = Object.Instantiate(_enemyPref, position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
            asteroid.DependencyInjectHealth(hp);
            asteroid.rigitBody.isKinematic = true;

            return asteroid;
        }
    }
}