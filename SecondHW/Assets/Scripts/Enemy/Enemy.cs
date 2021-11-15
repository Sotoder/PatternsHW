using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        public Health Health { get; protected set; }
        public bool IsOnScene { get; set; }
        public Rigidbody2D rigitBody { get; set; }

        private void Awake()
        {
            rigitBody = GetComponent<Rigidbody2D>();
        }

        public void DependencyInjectHealth(float maxHP)
        {
            Health = new Health(maxHP);
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
            gameObject.SetActive(false);
        }
    }
}