using UnityEngine;

namespace Asteroids
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] PlayerInitializationData _initializationData;
        [SerializeField] Player _player;

        private PlayerController _playerController;

        private void Start()
        {
            _playerController = new PlayerController(_initializationData, _player);
        }

        private void Update()
        {
            _playerController.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _playerController.FixedExecute(Time.fixedDeltaTime);
        }
    }
}
