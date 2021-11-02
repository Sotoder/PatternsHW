using System;
using UnityEngine;

namespace Asteroids
{
    public class ShipMoutionController: IMove, IRotation, IDisposable
    {
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        private readonly IAccelerationInput _inputController;

        private float _force;

        public float Speed => _moveImplementation.Speed;

        public ShipMoutionController(ShipInitializationData shipData, IAccelerationInput inputController, Transform transform, Rigidbody2D rigidbody, MoveTypes moveType)
        {
            _force = shipData.Force;

            _moveImplementation = moveType switch
            {
                MoveTypes.Transform => new MoveTransform(transform, shipData.Speed),
                MoveTypes.Acceleration => new AccelerationMove(transform, shipData.Speed, shipData.Acceleration),
                MoveTypes.Force => new ForceMove(shipData.Speed, rigidbody, _force),
                _ => new ForceMove(shipData.Speed, rigidbody, _force)
            };
            _rotationImplementation = new RotationShip(transform);

            _inputController = inputController;
            _inputController.AccelerationButtonDown += AddAcceleration;
            _inputController.AccelerationButtonUp += RemoveAcceleration;
        }

        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _moveImplementation.Move(horizontal, vertical, deltaTime);
        }

        public void Rotation(Vector3 direction)
        {
            _rotationImplementation.Rotation(direction);
        }

        public void AddAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.AddAcceleration();
            }
        }

        public void RemoveAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.RemoveAcceleration();
            }
        }

        public void Dispose()
        {
            _inputController.AccelerationButtonDown -= AddAcceleration;
            _inputController.AccelerationButtonUp -= RemoveAcceleration;
        }
    }
}