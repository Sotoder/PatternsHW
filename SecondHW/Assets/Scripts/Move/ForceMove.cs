using UnityEngine;

namespace Asteroids
{
    public class ForceMove: BasicMove
    {
        private readonly float _force;
        private readonly Rigidbody2D _rigidbody;

        public ForceMove(float speed, Rigidbody2D rigidbody, float force)
        {
            Speed = speed;
            _force = force;
            _rigidbody = rigidbody;
        }

        public override void Move(float horizontal, float vertical, float deltaTime)
        {
            var speed = deltaTime * Speed;
            _move.Set(horizontal * speed, vertical * speed, 0.0f);
            _rigidbody.velocity = _move * speed * _force;
        }
    }
}
