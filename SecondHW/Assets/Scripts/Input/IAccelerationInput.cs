using System;


namespace Asteroids
{
    public interface IAccelerationInput
    {
        public Action AccelerationButtonDown { get; set; }
        public Action AccelerationButtonUp { get; set; }
    }
}