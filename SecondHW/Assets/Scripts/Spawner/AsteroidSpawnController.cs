using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    internal class AsteroidSpawnController
    {
        private AsteroidPool _asteroidPool;
        private Camera _camera;
        private List<SpawnPoint> _screenSpawnPoints;
        private TimerController _timerController;

        private bool _isGlobalCooldown;

        private const int TOP_OF_THE_SCREEN = 5;
        private const int FORCE = 5;
        private const float GLOBAL_COOLDOWN = 0.2f;
        private const int RANDOMIZE_LUNCH_MODIFER = 20;

        public AsteroidSpawnController(AsteroidPool asteroidPool, int spawnPointsCount, TimerController timerController)
        {
            _asteroidPool = asteroidPool;
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
                _screenSpawnPoints.Add(new SpawnPoint(false, pointPosition));
            }
        }

        public void Execute()
        {          
            if (!_isGlobalCooldown)
            {
                var tracks = GetNotBusyTracks();
                if (tracks.Count < 1) return;

                _isGlobalCooldown = true;
                var GCDTimer = new Timer(GLOBAL_COOLDOWN);
                GCDTimer.timerIsOver += GlobalCooldownDown;
                _timerController.Add(GCDTimer);

                LunchAsteroids(tracks);
            }
        }

        private void LunchAsteroids(List<int> tracks)
        {
            var luckyTrack = _screenSpawnPoints[tracks[UnityEngine.Random.Range(0, tracks.Count - 1)]];

            luckyTrack.isBusy = true;

            int lunchType = UnityEngine.Random.Range(1, RANDOMIZE_LUNCH_MODIFER);
            IAsteroid asteroid;

            if (lunchType < RANDOMIZE_LUNCH_MODIFER * 0.5)
            {
                asteroid = _asteroidPool.GetAsteroid(AsteroidType.Asteroid, new Vector3(luckyTrack.position, TOP_OF_THE_SCREEN, 0));
            } else
            {
                asteroid = _asteroidPool.GetAsteroid(AsteroidType.BigAsteroid, new Vector3(luckyTrack.position, TOP_OF_THE_SCREEN, 0));
            }

            var forseModifer = UnityEngine.Random.Range(1, 1.1f);
            asteroid.rigitBody.velocity = Vector2.down * FORCE * forseModifer;

            float busyTimerDuration = UnityEngine.Random.Range(1f, 2f);
            
            var busyTrackTimer = new Timer(busyTimerDuration);
            busyTrackTimer.timerIsOver += luckyTrack.SetNotBusy;
            _timerController.Add(busyTrackTimer);
        }

        private List<int> GetNotBusyTracks()
        {
            var result = new List<int>();

            for(int i = 0; i < _screenSpawnPoints.Count; i++)
            {
                if (!_screenSpawnPoints[i].isBusy)
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