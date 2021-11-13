using UnityEngine;

namespace Asteroids
{
    public class BulletFactory
    {
        private Sprite _sprite;

        private const float MASS = 1f;           
        private const int DAMAGE = 50;

        public BulletFactory(Sprite sprite)
        {
            _sprite = sprite;
        }

        public Bullet GetBullet()
        {
            var bullet = new BulletBuilder().SetDamage(DAMAGE)
                                    .AddSprite(_sprite)
                                    .AddCollider()
                                    .AddRigitBody(MASS);
            return bullet;
        }
    }
}
