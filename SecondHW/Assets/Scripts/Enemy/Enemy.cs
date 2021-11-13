using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        public Action<Enemy> wasKilled = delegate (Enemy enemy) { };
        [SerializeField] Health _health;

        public Health Health { get => _health; protected set => _health = value; }
        public bool IsOnScene { get; set; }
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
                    EnemyDead();
                }
            }
        }
        protected virtual void EnemyDead()
        {
            wasKilled.Invoke(this);
            Destroy(gameObject);
        }
    }
}