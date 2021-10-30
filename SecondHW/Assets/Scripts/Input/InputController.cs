using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids {
    public class InputController: IAccelerationInput, IShotingInput
    {
        public Action FireButtonDown { get; set; } = delegate () { };
        public Action AccelerationButtonDown { get; set; } = delegate () { };
        public Action AccelerationButtonUp { get; set; } = delegate () { };

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                AccelerationButtonDown.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                AccelerationButtonUp.Invoke();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                FireButtonDown.Invoke();
            }
        }
    }
}
