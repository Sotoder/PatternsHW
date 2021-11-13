using UnityEngine;

namespace Asteroids
{
    public abstract class BasicMove : IMove
    {
        protected Vector3 _move;
        public float Speed { get; protected set; }

        public abstract void Move(float horizontal, float vertical, float deltaTime);
    }
}
