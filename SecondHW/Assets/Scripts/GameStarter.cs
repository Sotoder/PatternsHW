using UnityEngine;

namespace Asteroids
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] InitializationData _initializationData;
        [SerializeField] Player _player;
        [SerializeField] AsteroidList _asteroidList;

        private PlayerController _playerController;
        private InputController _inputController;
        private AsteroidSpawnController _AsteroidSpawnController;
        private TimerController _timerController;

        private void Start()
        {
            _timerController = new TimerController();

            var bulletPool = new BulletPool(20, _timerController);
            var asteroidPool = new AsteroidPool(10, _timerController);

            _player.Init(_initializationData, bulletPool);
            _inputController = new InputController();
            _playerController = new PlayerController(_initializationData.MoveType, _player, _inputController);
            _AsteroidSpawnController = new AsteroidSpawnController(asteroidPool, 9, _timerController);

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
            _inputController.Execute();
            _playerController.Execute(Time.deltaTime);
            _AsteroidSpawnController.Execute();
            _timerController.Execute();
        }

        private void FixedUpdate()
        {
            _playerController.FixedExecute(Time.fixedDeltaTime);
        }
    }
}
