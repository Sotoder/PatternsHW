using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class AsteroidInitData
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _rotation;
        [SerializeField] private float _hp;
        [SerializeField] private Asteroid _asteroidPref;

        public Vector2 Position { get => _position; }
        public Vector2 Rotation { get => _rotation; }
        public float Hp { get => _hp; }
        public Asteroid AsteroidPref { get => _asteroidPref; }

        public AsteroidInitData(Vector2 position, Vector2 rotation, float hp, Asteroid enemyPref)
        {
            _position = position;
            _rotation = rotation;
            _hp = hp;
            _asteroidPref = enemyPref;
        }

    }
}