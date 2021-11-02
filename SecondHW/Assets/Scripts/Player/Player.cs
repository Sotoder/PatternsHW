using System;
using UnityEngine;

namespace Asteroids
{
    public sealed class Player : MonoBehaviour
    {
        public Action notEnoughthHP = delegate () { };

        private PlayerData _data;

        private IMove _moveImplementation;
        private IRotation _rotationImplementation;

        public float Speed => _moveImplementation.Speed;


        public void Init (InitializationData initializationData)
        {
            _data = new PlayerData(initializationData);
            var rigitbody = GetComponent<Rigidbody2D>();

            _moveImplementation = initializationData.MoveType switch
            {
                MoveTypes.Transform => new MoveTransform(transform, _data.Speed),
                MoveTypes.Acceleration => new AccelerationMove(transform, _data.Speed, _data.Acceleration),
                MoveTypes.Force => new ForceMove(_data.Speed, rigitbody, _data.Force),
                _ => new ForceMove(_data.Speed, rigitbody, _data.Force)
            };

            _rotationImplementation = new RotationShip(transform);
        }

        public void Fire()
        {
            var temAmmunition = Instantiate(_data.Bullet, _data.Barrel.position, _data.Barrel.rotation);
            temAmmunition.AddForce(_data.Barrel.up * _data.Force);
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            ChangeHP();
        }

        private void ChangeHP()
        {
            if (_data.hp <= 0)
            {
                notEnoughthHP.Invoke();
            }
            else
            {
                _data.hp--;
            }
        }

    }
}