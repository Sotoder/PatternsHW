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

            var bulletPool = new BulletPool(20, timerController);
            var playerData = new PlayerData(data.PlayerInitData);
            data.Player.Init(playerData, bulletPool);

            var playerController = new PlayerController(data.Player, playerData, inputController);
            gameController.Add(playerController);
        }
    }
}