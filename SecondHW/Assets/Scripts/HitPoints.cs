using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class HitPoints: IDisposable
    {
        public Action _notEnoughthHP = delegate () { };

        [SerializeField] private float _hp;

        private readonly Player _player;
        public HitPoints(Player player, float maxHP)
        {
            _player = player;
            _player.gotDamaged += ChangeHP;
        }

        private void ChangeHP()
        {
            if (_hp <= 0)
            {
                _notEnoughthHP.Invoke();
            }
            else
            {
                _hp--;
            }
        }

        public void Dispose()
        {
            _player.gotDamaged -= ChangeHP;
        }
    }
}
