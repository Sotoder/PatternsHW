using System;
using UnityEngine;

namespace Asteroids
{
    public interface IEnemy
    {
        public Health Health { get; }
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public Rigidbody2D rigitBody { get; set; }
        public bool IsOnScene { get; set; }
        public void DependencyInjectHealth(float maxHP);
    }
}
