using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class BulletPool
    {
        private readonly List<Bullet> _bulletPool;
        private readonly int _capacityPool;
        private Transform _rootPool;
        private TimerController _timerController;

        private const float BULLET_LIFE_TIME = 1f;
        private const int BULLET_DAMAGE = 50;

        public BulletPool(int capacityPool, TimerController timerController)
        {
            _bulletPool = new List<Bullet>(capacityPool);
            _capacityPool = capacityPool;
            _timerController = timerController;

            if (!_rootPool)
            {
                _rootPool = new GameObject("[BulletPool]").transform;
            }
            var rootPoolPosition = new Vector3(5, 20, 0);
            _rootPool.transform.Translate(rootPoolPosition);

            FillPool();
        }

        private void FillPool()
        {

            for (int i = 0; i < _capacityPool; i++)
            {
                var bullet = Bullet.CreateBullet(BULLET_DAMAGE);
                bullet.transform.position = _rootPool.position;
                bullet.transform.SetParent(_rootPool);
                _bulletPool.Add(bullet);
            }
        }

        public Bullet GetBullet(Vector3 position, Quaternion rotation)
        {
            Bullet bullet = null;
            for (int i = 0; i < _bulletPool.Count; i++)
            {
                if (!_bulletPool[i].isOnScene)
                {
                    bullet = _bulletPool[i];
                }
            }

            if (bullet is null)
            {
                bullet = Bullet.CreateBullet(BULLET_DAMAGE);
                _bulletPool.Add(bullet);
            }

            bullet.isOnScene = true;
            bullet.transform.parent = null;
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;

            var flyTimeTimer = new Timer(BULLET_LIFE_TIME);
            flyTimeTimer.timerIsOver += delegate ()
            {
                if (bullet.isOnScene)
                {
                    ReturnToPool(bullet);
                }
            };
            _timerController.Add(flyTimeTimer);

            return bullet;
        }

        public void ReturnToPool(Bullet bullet)
        {
            bullet.transform.localPosition = _rootPool.transform.position;
            bullet.transform.localRotation = _rootPool.transform.rotation;
            bullet.rigitBody.velocity = Vector2.zero;
            bullet.transform.SetParent(_rootPool);
            bullet.isOnScene = false;
        }

        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }
    }
}
