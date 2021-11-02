using UnityEngine;

namespace Asteroids
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] InitializationData _initializationData;
        [SerializeField] Player _player;

        private PlayerController _playerController;
        private InputController _inputController;

        private void Start()
        {
            _player.Init(_initializationData);
            _inputController = new InputController();
            _playerController = new PlayerController(_initializationData.MoveType, _player, _inputController);

            IEnemyFactory factory = new AsteroidFactory();
            factory.Create(new Health(100.0f, 100.0f));
        }

        private void Update()
        {
            _inputController.Execute();
            _playerController.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _playerController.FixedExecute(Time.fixedDeltaTime);
        }
    }
}
