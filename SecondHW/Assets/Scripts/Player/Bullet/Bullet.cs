using UnityEngine;

namespace Asteroids {
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public bool isOnScene;
        public Rigidbody2D rigitBody { get; set; }
        public const int BULLET_SPEED = 10;

        private void Awake()
        {
            rigitBody = GetComponent<Rigidbody2D>();
        }

        public static Bullet CreateBullet(float damage)
        {
            var bullet = Instantiate(Resources.Load<Bullet>("Player/Bullet"));
            bullet.damage = damage;
            return bullet;
        }

        private void OnBecameInvisible()
        {

        }
    }
}
