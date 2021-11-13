using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/EnemyList", fileName = nameof(AsteroidList))]
    public sealed class AsteroidList : ScriptableObject
    {
        [SerializeField] private List<AsteroidInitData> enemies;

        public List<AsteroidInitData> Enemies { get => enemies; }
    }
}