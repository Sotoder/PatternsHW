using UnityEngine;

namespace Asteroids
{
    public class SpawnPoint
    {
        public bool isBusy { get; set; }
        public bool isShipOnTrack { get; set; }
        public float position;

        public SpawnPoint(float position)
        {
            this.position = position;
        }

        public void SetNotBusy()
        {
            isBusy = false;
        }
        public void ShipIsGone()
        {
            isBusy = false;
            isShipOnTrack = false;
        }

    }
}