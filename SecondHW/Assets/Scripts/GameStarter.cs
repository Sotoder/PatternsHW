using UnityEngine;

namespace Asteroids
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] InitializationData _initializationData;

        private GameController _gameController;

        private void Start()
        {
            _gameController = new GameController();
            new MainInitializator(_gameController, _initializationData);

            //EnemyShip.CreateShipEnemy(100f); //- Демонстрация всех видов Фабрик

            //IEnemyFactory<IAsteroid> factory = new AsteroidFactory();
            //for (int i = 0; i < _asteroidList.Enemies.Count; i++)
            //{
            //    factory.Create(_asteroidList.Enemies[i]);
            //}

            //BigAsteroid.Factory = factory;
            //var bigAsteroidPref = Resources.Load<BigAsteroid>("Enemy/BigAsteroid");
            //BigAsteroid.Factory.Create(new AsteroidInitData(new Vector2(6, 4), new Vector2(0, 0), 100, bigAsteroidPref));
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _gameController.Execute(deltaTime);
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            _gameController.FixedExecute(fixedDeltaTime);
        }
    }
}
