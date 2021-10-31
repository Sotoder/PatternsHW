using System;
using UnityEngine;

namespace Asteroids
{
    public sealed class Player : MonoBehaviour, IDisposable
    {
        public Action gotDamaged = delegate () { };
        
        [SerializeField] private float _maxHP;
        [SerializeField] private ShipInitializationData _shipData;
        [SerializeField] private MoveTypes _moveType;

        private Ship _ship;
        private HitPoints _hp;
        private InputController _inputController;

        private void Start()
        {
            _hp = new HitPoints(this, _maxHP);
            _hp._notEnoughthHP += GameOver;

            _inputController = new InputController();

            var rigitBody = GetComponent<Rigidbody2D>();
            _ship = new Ship(rigitBody, transform, _inputController, _shipData, _moveType);
        }

        private void Update()
        {
            _ship.Execute();
            _inputController.Execute();
        }

        private void FixedUpdate()
        {
            if(_moveType == MoveTypes.Force)
            {
                _ship.FixedExecute();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            gotDamaged.Invoke();
        }
        
        private void GameOver()
        {
            Dispose();
            _hp.Dispose();
            _ship.Dispose();
            Destroy(gameObject);
        }

        public void Dispose()
        {
            _hp._notEnoughthHP -= GameOver;
        }
    }
}