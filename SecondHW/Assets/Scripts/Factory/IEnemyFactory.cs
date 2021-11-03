using UnityEngine;

namespace Asteroids
{
    public interface IEnemyFactory<T> where T: IEnemy 
    {
        T Create(AsteroidInitData initData);
        T Create(Asteroid _asteroidPref, Vector3 position, Quaternion rotation, float hp);
    }
}