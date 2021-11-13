using UnityEngine;

namespace Asteroids {
    public class BulletBuilder
    {
        private Bullet _bullet;

        public BulletBuilder ()
        {
            var bulletObject = GameObject.Instantiate(Resources.Load<GameObject>("Player/Bullet"));
            _bullet = bulletObject.AddComponent<Bullet>();
        }

        public BulletBuilder AddSprite(Sprite sprite)
        {
            var spriteRenderer = GetOrAddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            return this;
        }

        public BulletBuilder AddRigitBody(float mass)
        {
            var rigidbody2D = GetOrAddComponent<Rigidbody2D>();
            rigidbody2D.mass = mass;
            rigidbody2D.isKinematic = true;
            _bullet.rigitBody = rigidbody2D;
            return this;
        }

        public BulletBuilder AddCollider()
        {
            var collider2D = GetOrAddComponent<CapsuleCollider2D>();
            collider2D.isTrigger = true;
            return this;
        }

        public BulletBuilder SetDamage(int damage)
        {
            _bullet.damage = damage;
            return this;
        }

        public static implicit operator Bullet(BulletBuilder bulletBuilder)
        {
            return bulletBuilder._bullet;
        }

        private T GetOrAddComponent<T>() where T : Component
        {
            var result = _bullet.gameObject.GetComponent<T>();
            if (!result)
            {
                result = _bullet.gameObject.AddComponent<T>();
            }
            return result;
        }
    }
}
