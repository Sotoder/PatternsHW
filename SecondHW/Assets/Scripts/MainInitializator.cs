namespace Asteroids
{
    public class MainInitializator
    {
        public MainInitializator(GameController gameController, InitializationData data)
        {
            var timerController = new TimerController();
            gameController.Add(timerController);

            var asteroidsController = new EnemyController(gameController, timerController);
            gameController.Add(asteroidsController);

            var inputController = new InputController();
            gameController.Add(inputController);

            var playerController = new PlayerController(data.PlayerInitData.MoveType, data.Player, inputController);
            gameController.Add(playerController);

            data.Player.Init(data.PlayerInitData, timerController);
        }
    }
}