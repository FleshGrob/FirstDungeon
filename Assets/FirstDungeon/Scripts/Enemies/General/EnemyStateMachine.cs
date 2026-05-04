namespace FirstDungeon.Scripts.Enemies.General
{
    public class EnemyStateMachine
    {
        public IEnemyState CurrentState { get; private set; }

    
        public void ChangeState(IEnemyState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Tick()
        {
            CurrentState?.Tick();
        }
    }
}
