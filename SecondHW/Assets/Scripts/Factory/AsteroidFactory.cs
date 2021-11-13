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

            return asteroid;
        }

        public Asteroid Create(Asteroid asteroidPrototype)
        {
            var asteroid = Object.Instantiate(asteroidPrototype);

            return asteroid;
        }
    }
}