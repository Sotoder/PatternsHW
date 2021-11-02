using UnityEngine;

namespace Asteroids
{
    public sealed class PlayerController
    {
        private Player _player;
        private InputController _inputController;
        private Camera _camera;
        private MoveTypes _moveType;

        public PlayerController (MoveTypes moveType, Player player, InputController inputController)
        {
            _inputController = inputController;
            _player = player;
            _player.notEnoughthHP += GameOver;

            _inputController = inputController;
            _inputController.accelerationButtonDown += PlayerAddAcceleration;
            _inputController.accelerationButtonUp += PlayerRemoveAcceleration;
            _inputController.fireButtonDown += PlayerFire;

            _camera = Camera.main;
            _moveType = moveType;
        }

        public void Execute(float deltaTime)
        {
            var direction = Input.mousePosition - _camera.WorldToScreenPoint(_player.transform.position);
            _player.Rotation(direction);
            if (!(_moveType == MoveTypes.Force))
            {
                _player.Move(_inputController.horizontal, _inputController.vertical, deltaTime);
            }
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            if (_moveType == MoveTypes.Force)
            {
                _player.Move(_inputController.horizontal, _inputController.vertical, fixedDeltaTime);
            }
        }
        private void PlayerFire()
        {
            _player.Fire();
        }

        private void PlayerRemoveAcceleration()
        {
            _player.RemoveAcceleration();
        }

        private void PlayerAddAcceleration()
        {
            _player.AddAcceleration();
        }

        private void GameOver()
        {
            Object.Destroy(_player.gameObject);
            _inputController.accelerationButtonDown -= PlayerAddAcceleration;
            _inputController.accelerationButtonUp -= PlayerRemoveAcceleration;
            _inputController.fireButtonDown -= PlayerFire;
        }
    }
}