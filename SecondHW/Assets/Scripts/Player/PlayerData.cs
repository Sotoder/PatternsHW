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

        public Rigidbody2D Bullet { get => _bullet; }
        public Transform Barrel { get => _barrel; }
        public float Force { get => _force; }
        public float Speed { get => _speed; }
        public float Acceleration { get => _acceleration; }

        public PlayerData(PlayerInitializationData initializationData)
        {
            hp = initializationData.MaxHP;
            _speed = initializationData.Speed;
            _acceleration = initializationData.Acceleration;
            _bullet = initializationData.Bullet;
            _barrel = initializationData.Barrel;
            _force = initializationData.Force;
        }
    }
}