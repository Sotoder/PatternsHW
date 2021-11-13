using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class InitializationData
    {
        [SerializeField] private PlayerInitData _playerInitData;
        [SerializeField] private Player _player;
        [SerializeField] private AsteroidList _asteroidList;
        [SerializeField] private Sprite _bulletSprite;
        [SerializeField] private List<Enemy> _enemyPrototypes;

        public Sprite BulletSprite { get => _bulletSprite; }
        public PlayerInitData PlayerInitData { get => _playerInitData; }
        public Player Player { get => _player; }
        public AsteroidList AsteroidList { get => _asteroidList; }
        public List<Enemy> EnemyPrototypes { get => _enemyPrototypes; }
    }
}
