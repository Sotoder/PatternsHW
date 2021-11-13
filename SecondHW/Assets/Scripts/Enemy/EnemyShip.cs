using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemyShip : Enemy, IEnemy
    {
        public Action shipLeftScene = delegate () { }; 

        public void DeactivateShip()
        {
            shipLeftScene.Invoke();
        }

        protected override void EnemyDead()
        {
            base.EnemyDead();
            shipLeftScene.Invoke();
        }
    }
}