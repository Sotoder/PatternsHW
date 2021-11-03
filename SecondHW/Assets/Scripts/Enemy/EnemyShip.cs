using UnityEngine;

namespace Asteroids
{
    public class EnemyShip : Enemy
    {
        public static EnemyShip CreateShipEnemy(float maxHP)
        {
            var ship = Instantiate(Resources.Load<EnemyShip>("Enemy/EnemyShip"));

            ship.Health = new Health(maxHP);

            return ship;
        }
    }
}