using UnityEngine;

namespace Asteroids
{
    public class PlayerData
    {
        public float hp;

        private float _speed;
        private float _acceleration;
        private Rigidbody2D _bullet;
        private Transform _barrel;
        private float _force;
        private MoveTypes _moveType;

        public Rigidbody2D Bullet { get => _bullet; }
        public Transform Barrel { get => _barrel; }
        public float Force { get => _force; }
        public float Speed { get => _speed; }
        public float Acceleration { get => _acceleration; }
        public MoveTypes MoveType { get => _moveType; }

        public PlayerData(PlayerInitData data)
        {
            hp = data.MaxHP;
            _speed = data.Speed;
            _acceleration = data.Acceleration;
            _bullet = data.Bullet;
            _barrel = data.Barrel;
            _force = data.Force;
            _moveType = data.MoveType;
        }
    }
}