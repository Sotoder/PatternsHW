using System;
using UnityEngine;

namespace Asteroids
{
    public sealed class Ship : IDisposable
    {
        private readonly IShotingInput _inputController;

        private Rigidbody2D _bullet;
        private Transform _barrel;
        private ShipMoutionController _moutionController;

        private float _force;
        private Transform _transform;
        private Camera _camera;
        private MoveTypes _moveType;

        public Ship(Rigidbody2D rigitBody, Transform transform, InputController inputController, ShipInitializationData shipData, MoveTypes moveType)
        {
            _moutionController = new ShipMoutionController(shipData, inputController as IAccelerationInput, transform, rigitBody, moveType);

            _camera = Camera.main;
            _bullet = shipData.Bullet;
            _barrel = shipData.Barrel;
            _force = shipData.Force;
            _transform = transform;
            _moveType = moveType;

            _inputController = inputController as IShotingInput;
            _inputController.FireButtonDown += Fire;
        }

        public void Execute()
        {
            var direction = Input.mousePosition - _camera.WorldToScreenPoint(_transform.position);
            _moutionController.Rotation(direction);
            if(!(_moveType == MoveTypes.Force))
            {
                _moutionController.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Time.deltaTime);
            }
        }

        public void FixedExecute()
        {
            _moutionController.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Time.fixedDeltaTime);
        }

        public void Fire()
        {
            var temAmmunition = UnityEngine.Object.Instantiate(_bullet, _barrel.position, _barrel.rotation);
            temAmmunition.AddForce(_barrel.up * _force);
        }

        public void Dispose()
        {
            _inputController.FireButtonDown -= Fire;
        }
    }
}