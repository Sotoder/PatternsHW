using UnityEngine;

namespace Asteroids
{
    public sealed class PlayerController
    {

        private MoveTypes _moveType;
        private Player _player;
        private InputController _inputController;
        private Camera _camera;

        public PlayerController (PlayerInitializationData playerInitializationData, Player player)
        {
            _inputController = new InputController();
            player.Init(playerInitializationData, _inputController);

            _player = player;
            _moveType = playerInitializationData.MoveType;
            _camera = Camera.main;

            _player.notEnoughthHP += GameOver;
        }

        public void Execute()
        {
            _inputController.Execute();

            var direction = Input.mousePosition - _camera.WorldToScreenPoint(_player.transform.position);
            _player.Rotation(direction);

            if (!(_moveType == MoveTypes.Force))
            {
                _player.Move(Time.deltaTime);
            }

        }

        public void FixedExecute()
        {
            if(_moveType == MoveTypes.Force)
            {
                _player.Move(Time.fixedDeltaTime);
            }
        }
        
        private void GameOver()
        {
            Object.Destroy(_player.gameObject);
            _player.Dispose();
        }
    }
}