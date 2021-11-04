using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    internal class EnemySpawnController: IController, IExecute
    {
        private EnemyPool _enemyPool;
        private Camera _camera;
        private List<SpawnPoint> _screenSpawnPoints;
        private TimerController _timerController;

        private bool _isGlobalCooldown;

        private const int START_LUNCH_POSITION_Y = 7;
        private const int FORCE = 2;
        private const float GLOBAL_COOLDOWN = 0.4f;
        private const int RANDOMIZE_LUNCH_MODIFER = 20;

        public EnemySpawnController(EnemyPool enemyPool, int spawnPointsCount, TimerController timerController)
        {
            _enemyPool = enemyPool;
            _camera = Camera.main;
            _timerController = timerController;

            _screenSpawnPoints = new List<SpawnPoint>(spawnPointsCount);
            CreateSpawnPoints(spawnPointsCount);
        }

        private void CreateSpawnPoints(int spawnPointsCount)
        {
            var maxScreenWidth = _camera.orthographicSize * 2;
            var step = _camera.orthographicSize * 4 / (spawnPointsCount + 1);
            var pointPosition = _camera.orthographicSize * -2;

            while (pointPosition < maxScreenWidth - step)
            {
                pointPosition += step;
                _screenSpawnPoints.Add(new SpawnPoint(pointPosition));
            }
        }

        public void Execute(float deltaTime)
        {          
            if (!_isGlobalCooldown)
            {
                var freeTracks = GetNotBusyTracks();
                if (freeTracks.Count < 1) return;

                _isGlobalCooldown = true;
                var GCDTimer = new Timer(GLOBAL_COOLDOWN);
                GCDTimer.timerIsOver += GlobalCooldownDown;
                _timerController.Add(GCDTimer);

                LunchEnemy(freeTracks);
            }
        }

        private void LunchEnemy(List<int> freeTracks)
        {
            var luckyTrack = _screenSpawnPoints[freeTracks[UnityEngine.Random.Range(0, freeTracks.Count - 1)]];
            luckyTrack.isBusy = true;

            int lunchType = UnityEngine.Random.Range(1, RANDOMIZE_LUNCH_MODIFER);
            Enemy enemy;

            if (lunchType < RANDOMIZE_LUNCH_MODIFER * 0.4)
            {
                enemy = _enemyPool.GetEnemy(EnemyType.Asteroid, new Vector3(luckyTrack.position, START_LUNCH_POSITION_Y, 0));
            } else if (lunchType >= RANDOMIZE_LUNCH_MODIFER * 0.4 && lunchType < RANDOMIZE_LUNCH_MODIFER * 0.8)
            {
                enemy = _enemyPool.GetEnemy(EnemyType.BigAsteroid, new Vector3(luckyTrack.position, START_LUNCH_POSITION_Y, 0));
            } else
            {
                enemy = _enemyPool.GetEnemy(EnemyType.EnemyShip, new Vector3(luckyTrack.position, START_LUNCH_POSITION_Y, 0));
                luckyTrack.isShipOnTrack = true;
            }

            var forseModifer = UnityEngine.Random.Range(1, 1.1f);
            enemy.rigitBody.velocity = Vector2.down * FORCE * forseModifer;
            
            if(enemy is EnemyShip)
            {
                (enemy as EnemyShip).shipIsReturnToPool += luckyTrack.ShipIsGone;
            } else
            {
                float busyTimerDuration = UnityEngine.Random.Range(1f, 2f);
                var busyTrackTimer = new Timer(busyTimerDuration);
                busyTrackTimer.timerIsOver += luckyTrack.SetNotBusy;
                _timerController.Add(busyTrackTimer);
            }
        }

        private List<int> GetNotBusyTracks()
        {
            var result = new List<int>();

            for(int i = 0; i < _screenSpawnPoints.Count; i++)
            {
                if (!_screenSpawnPoints[i].isBusy && !_screenSpawnPoints[i].isShipOnTrack)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        private void GlobalCooldownDown()
        {
            _isGlobalCooldown = false;
        }
    }
}