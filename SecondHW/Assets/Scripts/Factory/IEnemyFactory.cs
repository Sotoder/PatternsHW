namespace Asteroids
{
    public interface IEnemyFactory
    {
        Enemy Create(Health hp);
    }
}