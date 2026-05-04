using UnityEngine;

namespace FirstDungeon.Scripts.Enemies.General.EnemyStates
{
    public class PatrolState : IEnemyState
    {
        Enemy _enemy;
        Vector2 _target;
        bool _isGoing;
        float _idleTimer;
        
    
        public PatrolState(Enemy enemy)
        {
            _enemy = enemy;
        }
        

        public void Enter()
        {
            PickNewTarget();
            _enemy.Move(_target, _enemy.Config.PatrolMoveSpeed);
            _isGoing = true;
        }

        public void Tick()
        {
            float dist = Vector2.Distance(_enemy.transform.position, _enemy.PlayerTransform.position);
            if (dist <= _enemy.Config.AggroRadius)
            {
                RaycastHit2D hit = Physics2D.Linecast(_enemy.transform.position,
                    _enemy.PlayerTransform.position, _enemy.Obstacle);
                if (hit.collider == null)
                {
                    _enemy.StateMachine.ChangeState(new CombatState(_enemy));
                    return;
                }
            }
            
            if (_isGoing && !_enemy.Agent.pathPending && _enemy.Agent.remainingDistance < _enemy.Agent.stoppingDistance)
            {
                _idleTimer = Random.Range(_enemy.Config.PatrolIdleTimeMin, _enemy.Config.PatrolIdleTimeMax);
                _isGoing = false;
            }

            if (!_isGoing)
            {
                _idleTimer -= Time.deltaTime;
                if (_idleTimer <= 0f)
                {
                    PickNewTarget();
                    _enemy.Move(_target, _enemy.Config.PatrolMoveSpeed);
                    _isGoing = true;
                }
            }
        }

        public void Exit() { }

        void PickNewTarget()
        {
            _target = Random.insideUnitCircle * _enemy.Config.PatrolRadius + _enemy.HomePosition;
        }
    }
}
