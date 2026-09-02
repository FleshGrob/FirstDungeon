using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.Enemies.General.EnemyStates
{
    public class CombatState : IEnemyState
    {
        Enemy _enemy;
        float _basicAttackTimer;
        float _specialAttackTimer;
        
    
        public CombatState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter() { }

        public void Tick()
        {
            float dist = Vector2.Distance(_enemy.transform.position, Player.Instance.Transform.position);

            if (!_enemy.IsActing)
            {
                if (_enemy.BasicAttackTimer <= 0 && dist <= _enemy.Config.BasicAttackRange)
                {
                    _enemy.BasicAttack();
                    _enemy.ResetBasicTimer();
                }
                else if (_enemy.SpecialAttackTimer <= 0 && dist <= _enemy.Config.SpecialAttackRange)
                {
                    _enemy.SpecialAttack();
                    _enemy.ResetSpecialTimer();
                }
            }
            
            if (dist > Mathf.Min(_enemy.Config.BasicAttackRange, _enemy.Config.SpecialAttackRange))
            {
                _enemy.StateMachine.ChangeState(new ChaseState(_enemy));
            }
        }
        
        public void Exit() { }
    }
}
