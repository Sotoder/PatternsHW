using UnityEngine;

namespace Asteroids
{
    public interface IEnemyFactory<T> where T: Enemy 
    {
        T Create(AsteroidInitData initData);
        T Create(Asteroid _asteroidPref, Vector3 position, Quaternion rotation, float hp);
    }
}