namespace Asteroids
{
    public class GameController : IController
    {
        private GameControllerModel _model;

        public GameController()
        {
            _model = new GameControllerModel();
        }

        public void Add(IController controller)
        {
            if (controller is IExecute executeController)
            {
                _model.ExecuteControllers.Add(executeController);
            }

            if (controller is IFixedExecute fixedController)
            {
                _model.FixedControllers.Add(fixedController);
            }
        }

        public void Execute(float deltaTime)
        {
            for (var element = 0; element < _model.ExecuteControllers.Count; ++element)
            {
                _model.ExecuteControllers[element].Execute(deltaTime);
            }
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            for (var element = 0; element < _model.FixedControllers.Count; ++element)
            {
                _model.FixedControllers[element].FixedExecute(fixedDeltaTime);
            }
        }
    }
}
