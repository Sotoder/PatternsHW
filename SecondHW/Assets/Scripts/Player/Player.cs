using System;
using UnityEngine;

namespace Asteroids
{
    public sealed class Player : MonoBehaviour
    {
        public Action notEnoughthHP = delegate () { };

        private PlayerData _data;
        private BulletPool _bulletPool;

        private IMove _moveImplementation;

        public float Speed => _moveImplementation.Speed;


        public void Init (InitializationData initializationData, BulletPool bulletPool)
        {
            _data = new PlayerData(initializationData);
            _bulletPool = bulletPool;

            var rigitbody = GetComponent<Rigidbody2D>();
            _moveImplementation = initializationData.MoveType switch
            {
                MoveTypes.Transform => new MoveTransform(transform, _data.Speed),
                MoveTypes.Acceleration => new AccelerationMove(transform, _data.Speed, _data.Acceleration),
                MoveTypes.Force => new ForceMove(_data.Speed, rigitbody, _data.Force),
                _ => new ForceMove(_data.Speed, rigitbody, _data.Force)
            };
        }

        public void Fire()
        {
            var bullet = _bulletPool.GetBullet(_data.Barrel.position, _data.Barrel.rotation);
            bullet.rigitBody.velocity = _data.Barrel.up * Bullet.BULLET_SPEED;
        }

        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _moveImplementation.Move(horizontal, vertical, deltaTime);
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 3)
            {
                ChangeHP();
            }
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