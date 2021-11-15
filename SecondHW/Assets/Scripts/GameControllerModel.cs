using System.Collections.Generic;

namespace Asteroids
{
    public class GameControllerModel
    {
        private readonly List<IFixedExecute> _fixedControllers;
        private readonly List<IExecute> _executeControllers;
        public GameControllerModel()
        {
            _executeControllers = new List<IExecute>(8);
            _fixedControllers = new List<IFixedExecute>(8);
        }

        public List<IExecute> ExecuteControllers => _executeControllers;

        internal List<IFixedExecute> FixedControllers => _fixedControllers;
    }
}