using System;
using System.Collections.Generic;

namespace Asteroids
{
    public class EnemyController: IExecute, IController
    {
        private EnemyPool _enemyPool;

        public EnemyController (GameController gameController, TimerController timerController)
        {
            _enemyPool = new EnemyPool(10);

            var asteroidSpawnController = new EnemySpawnController(_enemyPool, 9, timerController);
            gameController.Add(asteroidSpawnController);
        }
        public void Execute(float deltaTime)
        {
            CheckEnemiesState(_enemyPool.SmallAsteroidPool);
            CheckEnemiesState(_enemyPool.BigAsteroidPool);
            CheckEnemiesState(_enemyPool.ShipPool);

        }

        private void CheckEnemiesState<T>(List<T> enemiesList) where T: Enemy
        {
            for (int i = 0; i < enemiesList.Count; i++)
            {
                if ((enemiesList[i].IsOnScene && enemiesList[i].transform.position.y < -5) ||(enemiesList[i].IsOnScene && !enemiesList[i].gameObject.activeSelf))
                {
                    if (enemiesList[i] is EnemyShip) (enemiesList[i] as EnemyShip).DeactivateShip();
                    _enemyPool.ReturnToPool(enemiesList[i]);
                }
            }
        }
    }
}