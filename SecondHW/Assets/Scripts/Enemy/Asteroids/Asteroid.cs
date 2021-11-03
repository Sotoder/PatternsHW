using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Asteroid: Enemy, IAsteroid
    {
        public Action<Asteroid> Destroed { get; set; } = delegate (Asteroid asteroid) { };
        public Rigidbody2D rigitBody { get; set; }

        private void Awake()
        {
            rigitBody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 6)
            {
                Health.GetDamage(collision.GetComponent<Bullet>().damage);

                if (Health.Current <= 0)
                {
                    Destroed.Invoke(this);
                }
            }
        }
    }
}