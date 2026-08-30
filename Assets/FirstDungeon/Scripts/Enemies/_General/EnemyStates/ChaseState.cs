using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.Enemies.General.EnemyStates
{
    public class ChaseState : IEnemyState
    {
        Enemy _enemy;
    
        public ChaseState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Move(Player.Instance.Transform.position, _enemy.Config.ChaseMoveSpeed);
        }

        public void Tick()
        {
            float dist = Vector2.Distance(_enemy.transform.position, Player.Instance.Transform.position);

            if ((dist < _enemy.Config.BasicAttackRange && _enemy.BasicAttackTimer <= 0) ||
                (dist < _enemy.Config.SpecialAttackRange && _enemy.SpecialAttackTimer <= 0) ||
                dist < Mathf.Min(_enemy.Config.BasicAttackRange, _enemy.Config.SpecialAttackRange)) // эта строчка нужна для ренжевиков, чтобы не чейзили зазря
            {
                _enemy.StateMachine.ChangeState(new CombatState(_enemy)); 
                return;
            }
            
            _enemy.Move(Player.Instance.Transform.position, _enemy.Config.ChaseMoveSpeed);
        }

        public void Exit()
        {
            _enemy.Agent.isStopped = true;
        }
    }
}