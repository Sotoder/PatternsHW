using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class InitializationData
    {
        [SerializeField] private PlayerInitData _playerInitData;
        [SerializeField] private Player _player;
        [SerializeField] private AsteroidList _asteroidList;

        public PlayerInitData PlayerInitData { get => _playerInitData; }
        public Player Player { get => _player; }
        public AsteroidList AsteroidList { get => _asteroidList; }
    }
}
