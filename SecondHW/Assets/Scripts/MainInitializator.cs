namespace Asteroids
{
    public class MainInitializator
    {
        public MainInitializator(GameController gameController, InitializationData data)
        {
            var timerController = new TimerController();
            gameController.Add(timerController);

            var asteroidsController = new EnemyController(gameController, timerController, data.EnemyPrototypes);
            gameController.Add(asteroidsController);

            var inputController = new InputController();
            gameController.Add(inputController);

            var bulletFactory = new BulletFactory(data.BulletSprite);
            var bulletPool = new BulletPool(20, timerController, bulletFactory);
            ServiceLocator.SetService<BulletPool>(bulletPool);

            var playerData = new PlayerData(data.PlayerInitData);
            data.Player.Init(playerData);

            var playerController = new PlayerController(data.Player, playerData, inputController);
            gameController.Add(playerController);
        }
    }
}