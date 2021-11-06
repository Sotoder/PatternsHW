using UnityEngine;

namespace Asteroids
{
    public sealed class PlayerController: IController, IExecute, IFixedExecute
    {
        private Player _player;
        private InputController _inputController;
        private PlayerData _playerData;

        public PlayerController (Player player, PlayerData data, InputController inputController)
        {
            _inputController = inputController;
            _player = player;
            _playerData = data;
            _player.getDamage += ChangeHP;

            _inputController = inputController;
            _inputController.accelerationButtonDown += PlayerAddAcceleration;
            _inputController.accelerationButtonUp += PlayerRemoveAcceleration;
            _inputController.fireButtonDown += PlayerFire;
        }

        public void Execute(float deltaTime)
        {
            if (!(_playerData.MoveType == MoveTypes.Force))
            {
                _player.Move(_inputController.horizontal, _inputController.vertical, deltaTime);
            }
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            if (_playerData.MoveType == MoveTypes.Force)
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

        private void ChangeHP()
        {
            if (_playerData.hp <= 0)
            {
                GameOver();
            }
            else
            {
                _playerData.hp--;
            }
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