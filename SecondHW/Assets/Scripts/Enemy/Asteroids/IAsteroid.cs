using System;
using UnityEngine;

namespace Asteroids
{
    public interface IAsteroid : IEnemy
    {
        public Rigidbody2D rigitBody { get; set; }

        public Action<Asteroid> Destroed { get; set; } 
    }
}