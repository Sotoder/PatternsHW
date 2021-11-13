using System;
using UnityEngine;

namespace Asteroids {
    public class InputController: IController, IExecute
    {
        public Action fireButtonDown = delegate () { };
        public Action accelerationButtonDown = delegate () { };
        public Action accelerationButtonUp = delegate () { };
        public float horizontal;
        public float vertical;

        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                accelerationButtonDown.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                accelerationButtonUp.Invoke();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                fireButtonDown.Invoke();
            }

            horizontal = Input.GetAxis(HORIZONTAL);
            vertical = Input.GetAxis(VERTICAL);
        }
    }
}
