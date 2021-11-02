using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public struct ShipInitializationData
    {
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
    }
}
