using UnityEngine;

namespace Asteroids
{
    public class MoveTransform : BasicMove
    {
        private readonly Transform _transform;

        public MoveTransform(Transform transform, float speed)
        {
            _transform = transform;
            Speed = speed;
        }

        public override void Move(float horizontal, float vertical, float deltaTime)
        {
            var speed = deltaTime * Speed;
            _move.Set(horizontal * speed, vertical * speed, 0.0f);
            _transform.localPosition += _move;
        }
    }
}