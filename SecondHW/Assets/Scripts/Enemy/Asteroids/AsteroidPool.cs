using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids
{
    public class AsteroidPool
    {
        private readonly List<IAsteroid> _smallAsteroidPool;
        private readonly List<IAsteroid> _bigAsteroidPool;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly int _capacityPool;
        private Transform _rootPool;
        private TimerController _timerController;

        private const int SMALL_ASTEROID_HP = 100;
        private const int BIG_ASTEROID_HP = 200;

        public AsteroidPool(int capacityPool, TimerController timerController)
        {
            _timerController = timerController;
            _asteroidFactory = new AsteroidFactory();
            _smallAsteroidPool = new List<IAsteroid>(capacityPool);
            _bigAsteroidPool = new List<IAsteroid>(capacityPool);
            _capacityPool = capacityPool;

            if (!_rootPool)
            {
                _rootPool = new GameObject("[AsteroidPool]").transform;
            }
            _rootPool.transform.Translate(0, 20, 0);

            FillPool();
        }

        private void FillPool()
        {
            var asteroidPref = Resources.Load<SmallAsteroid>("Enemy/Asteroid");
            var bigAsteroidPref = Resources.Load<BigAsteroid>("Enemy/BigAsteroid");

            for (int i = 0; i < _capacityPool; i++)
            {
                var asteroid = _asteroidFactory.Create(asteroidPref, _rootPool.position, _rootPool.rotation, SMALL_ASTEROID_HP);
                AddAsteroid(_smallAsteroidPool, asteroid);
                asteroid = _asteroidFactory.Create(bigAsteroidPref, _rootPool.position, _rootPool.rotation, BIG_ASTEROID_HP);
                AddAsteroid(_bigAsteroidPool, asteroid);
            }
        }

        private void AddAsteroid(List<IAsteroid> asteroids, IAsteroid asteroid)
        {
            asteroid.transform.SetParent(_rootPool);
            asteroids.Add(asteroid);
        }

        public IAsteroid GetAsteroid(AsteroidType asteroidType, Vector3 position)
        {
            IAsteroid result;
            result = asteroidType switch
            {
                AsteroidType.Asteroid => GetSmalAsteroid(),
                AsteroidType.BigAsteroid => GetBigAsteroid(),
                _ => throw new NotImplementedException("Не найден тип астероида")
            };

            result.IsOnScene = true;
            result.transform.parent = null;
            result.transform.position = position;

            var flyTimeTimer = new Timer(3f);
            flyTimeTimer.timerIsOver += delegate ()
            {
                if (result.IsOnScene)
                {
                    ReturnToPool(result);
                }
            };
            _timerController.Add(flyTimeTimer);

            result.Destroed += ReturnToPool;

            return result;
        }

        private IAsteroid GetBigAsteroid()
        {
            IAsteroid asteroid = null;
            for (int i = 0; i < _bigAsteroidPool.Count; i++)
            {
                if (!_bigAsteroidPool[i].IsOnScene)
                {
                    asteroid = _bigAsteroidPool[i];
                }
            }
            if (asteroid is null)
            {
                var asteroidPref = Resources.Load<BigAsteroid>("Enemy/BigAsteroid");
                asteroid = _asteroidFactory.Create(asteroidPref, _rootPool.position, _rootPool.rotation, BIG_ASTEROID_HP);
                _bigAsteroidPool.Add(asteroid);
            }

            return asteroid;
        }

        private IAsteroid GetSmalAsteroid()
        {
            IAsteroid asteroid = null;
            for (int i = 0; i < _smallAsteroidPool.Count; i++)
            {
                if (!_smallAsteroidPool[i].IsOnScene)
                {
                    asteroid = _smallAsteroidPool[i];
                    break;
                }
            }
            if (asteroid is null)
            {
                var asteroidPref = Resources.Load<SmallAsteroid>("Enemy/Asteroid");
                asteroid = _asteroidFactory.Create(asteroidPref, _rootPool.position, _rootPool.rotation, SMALL_ASTEROID_HP);
                _smallAsteroidPool.Add(asteroid);
            }

            return asteroid;
        }

        public void ReturnToPool(IAsteroid asteroid)
        {
            asteroid.transform.localPosition = _rootPool.transform.position;
            asteroid.transform.localRotation = _rootPool.transform.rotation;
            asteroid.transform.SetParent(_rootPool);
            asteroid.rigitBody.velocity = Vector2.zero;
            asteroid.Health.ChangeCurrentHealth(asteroid.Health.Max);
            asteroid.IsOnScene = false;
        }

        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }
    }
}