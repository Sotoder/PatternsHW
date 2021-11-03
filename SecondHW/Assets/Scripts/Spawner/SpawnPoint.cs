namespace Asteroids
{
    public class SpawnPoint
    {
        public bool isBusy { get; set; }
        public float position;

        public SpawnPoint(bool isBusy, float position)
        {
            this.isBusy = isBusy;
            this.position = position;
        }

        public void SetNotBusy()
        {
            isBusy = false;
        }
    }
}