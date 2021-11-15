using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public struct PlayerInitData
    {
        [SerializeField] private float _maxHP;
        [SerializeField] private MoveTypes _moveType;
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private Rigidbody2D _bullet;
        [SerializeField] private Transform _barrel;
        [SerializeField] private float _force;

        public Rigidbody2D Bullet { get => _bullet; }
        public Transform Barrel { get => _barrel; }
        public float Force { get => _force; }
        public float Speed { get => _speed; }
        public float Acceleration { get => _acceleration; }
        public float MaxHP { get => _maxHP; }
        public MoveTypes MoveType { get => _moveType; }
    }
}