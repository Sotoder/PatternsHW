using System;
using UnityEngine;

namespace Asteroids
{
    public interface IEnemy
    {
        public Health Health { get; }
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public bool IsOnScene { get; set; }
        public void DependencyInjectHealth(float maxHP);
    }
}
