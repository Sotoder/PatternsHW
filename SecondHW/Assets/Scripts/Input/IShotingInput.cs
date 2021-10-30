using System;


namespace Asteroids
{
    public interface IShotingInput
    {
        public Action FireButtonDown { get; set; }
    }
}
