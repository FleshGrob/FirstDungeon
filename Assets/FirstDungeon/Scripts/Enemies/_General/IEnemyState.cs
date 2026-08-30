namespace FirstDungeon.Scripts.Enemies.General
{
    public interface IEnemyState 
    {
        void Enter();
        void Tick();
        void Exit();
    }
}
