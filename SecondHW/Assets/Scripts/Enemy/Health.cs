namespace Asteroids
{
    public sealed class Health
    {
        public float Max { get; }
        public float Current { get; private set; }

        public Health(float max)
        {
            Max = max;
            Current = max;
        }

        public void ChangeCurrentHealth(float hp)
        {
            Current = hp;
        }

        public void GetDamage(float damage)
        {
            Current -= damage;
        }
    }
}
