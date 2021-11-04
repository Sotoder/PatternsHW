using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids
{
    public class EnemyPool
    {
        private readonly List<Asteroid> _smallAsteroidPool;
        private readonly List<Asteroid> _bigAsteroidPool;
        private readonly List<EnemyShip> _shipPool;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly int _capacityPool;
        private Transform _rootPool;

        private const int SMALL_ASTEROID_HP = 100;
        private const int BIG_ASTEROID_HP = 200;

        public List<Asteroid> SmallAsteroidPool => _smallAsteroidPool;

        public List<Asteroid> BigAsteroidPool => _bigAsteroidPool;

        public List<EnemyShip> ShipPool => _shipPool;

        public EnemyPool(int capacityPool)
        {
            _asteroidFactory = new AsteroidFactory();
            _shipPool = new List<EnemyShip>(capacityPool);
            _smallAsteroidPool = new List<Asteroid>(capacityPool);
            _bigAsteroidPool = new List<Asteroid>(capacityPool);
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
                var ship = EnemyShip.CreateShipEnemy(150f);
                AddShip(_shipPool, ship);
            }
        }

        private void AddAsteroid(List<Asteroid> asteroids, Asteroid asteroid)
        {
            asteroid.transform.SetParent(_rootPool);
            asteroids.Add(asteroid);
        }

        private void AddShip(List<EnemyShip> ships, EnemyShip ship)
        {
            ship.gameObject.transform.position = _rootPool.position;
            ship.gameObject.transform.rotation = _rootPool.rotation;
            ship.transform.SetParent(_rootPool);
            ships.Add(ship);
        }

        public Enemy GetEnemy(EnemyType enemyType, Vector3 position)
        {
            Enemy result;
            result = enemyType switch
            {
                EnemyType.Asteroid => (Enemy)GetSmalAsteroid(),
                EnemyType.BigAsteroid => (Enemy)GetBigAsteroid(),
                EnemyType.EnemyShip => (Enemy)GetShip(),
                _ => throw new NotImplementedException("Не найден тип врага")
            };

            result.IsOnScene = true;
            result.transform.parent = null;
            result.transform.position = position;
            result.rigitBody.isKinematic = false;

            return result;
        }

        private EnemyShip GetShip()
        {
            EnemyShip ship = null;
            for (int i = 0; i < _shipPool.Count; i++)
            {
                if (!_shipPool[i].IsOnScene)
                {
                    ship = _shipPool[i];
                }
            }
            if (ship is null)
            {
                ship = EnemyShip.CreateShipEnemy(150f);
                ship.gameObject.transform.position = _rootPool.position;
                ship.gameObject.transform.rotation = _rootPool.rotation;
                _shipPool.Add(ship);
            }
            return ship;
        }

        private Asteroid GetBigAsteroid()
        {
            Asteroid asteroid = null;
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

        private Asteroid GetSmalAsteroid()
        {
            Asteroid asteroid = null;
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

        public void ReturnToPool(Enemy enemy)
        {
            enemy.rigitBody.Sleep();
            enemy.rigitBody.velocity = Vector2.zero;
            enemy.transform.localPosition = _rootPool.transform.position;
            enemy.transform.localRotation = _rootPool.transform.rotation;
            enemy.transform.SetParent(_rootPool);
            enemy.rigitBody.isKinematic = true;
            enemy.gameObject.SetActive(true);
            enemy.Health.ChangeCurrentHealth(enemy.Health.Max);
            enemy.IsOnScene = false;
        }

        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }
    }
}