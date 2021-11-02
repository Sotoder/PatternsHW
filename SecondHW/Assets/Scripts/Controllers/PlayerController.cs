using UnityEngine;

namespace Asteroids
{
    public sealed class PlayerController
    {
        private Player _player;
        private InputController _inputController;

        public PlayerController (PlayerInitializationData playerInitializationData, Player player)
        {
            _inputController = new InputController();
            player.Init(playerInitializationData, _inputController);

            _player = player;
            _player.notEnoughthHP += GameOver;
        }

        public void Execute(float deltaTime)
        {
            _inputController.Execute();
            _player.Execute(deltaTime);
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            _player.FixedExecute(fixedDeltaTime);
        }
        
        private void GameOver()
        {
            Object.Destroy(_player.gameObject);
            _player.Dispose();
        }
    }
}