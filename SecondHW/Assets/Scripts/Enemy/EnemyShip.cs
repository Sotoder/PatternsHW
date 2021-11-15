using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemyShip : Enemy, IEnemy
    {
        public Action shipIsReturnToPool = delegate () { }; 
        public static EnemyShip CreateShipEnemy(float maxHP)
        {
            var ship = Instantiate(Resources.Load<EnemyShip>("Enemy/EnemyShip"));
            ship.rigitBody.isKinematic = true;
            ship.Health = new Health(maxHP);

            return ship;
        }

        public void DeactivateShip()
        {
            shipIsReturnToPool.Invoke();
        }
        protected override void EnemyDead()
        {
            base.EnemyDead();
            shipIsReturnToPool.Invoke();
        }
    }
}