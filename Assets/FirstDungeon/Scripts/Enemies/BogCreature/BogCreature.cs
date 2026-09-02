using UnityEngine;
using System.Collections;
using FirstDungeon.Scripts.Enemies.General.EnemyStates;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BogCreature : Enemy
{
    [SerializeField] BogCreatureConfig _config;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LayerMask _beamStopLayer;
    [SerializeField] int _anglesPerSecond;
    [SerializeField] GameObject _rootAttack;
    
    GameObject _acidBeam;
    SpriteRenderer _acidBeamSr;
    float _teleportTimer;
    
    public override EnemyConfig Config => _config;


    protected override void Awake()
    {
        base.Awake();
        _acidBeam =  transform.GetChild(0).gameObject;
        _acidBeamSr = _acidBeam.GetComponent<SpriteRenderer>();
    }
    
    protected override void Update()
    {
        _teleportTimer -= Time.deltaTime;
        
        if (_isStunned) return;

        base.Update();

        if (_teleportTimer <= 0 && StateMachine.CurrentState is not PatrolState && !IsActing) Teleport();
    }
    
    void Teleport()
    {
        for (int i = 0; i < _config.TeleportMaxAttempts; i++)
        {
            Vector2 candidate = (Vector2)Player.Instance.Transform.position + Random.insideUnitCircle.normalized * 0.65f; // умножение на сумму коллайдеров игрока и врага. может сломаться, если поменять размеры коллайдеров.
            
            if (!NavMesh.SamplePosition(candidate, out NavMeshHit hit, 0.1f, NavMesh.AllAreas)) continue;
            if (Physics2D.OverlapCircle(candidate, 0.3f, _enemyLayer) != null) continue; // используется радиус коллайдера твари
            if (!RoomCol.OverlapPoint(hit.position)) continue;
            Agent.Warp(hit.position);
            _teleportTimer = _config.TeleportCooldown;
            return;
        }

        _teleportTimer = _config.TeleportFailCooldown;
    }

    public override void BasicAttack()
    {
        StartActing(RootAttackRoutine());
    }

    IEnumerator RootAttackRoutine()
    {
        try
        {
            IsActing = true;
            _canWalk = false;

            CastTimeLeft = _config.BasicAttackCastTime;
            float attackDurationLeft = _config.BasicAttackDuration;
            
            _rootAttack.transform.right = Player.Instance.Transform.position - transform.position;
            
            while (CastTimeLeft > 0)
            {
                CastTimeLeft -= Time.deltaTime;
                yield return null;
            }
            
            _rootAttack.gameObject.SetActive(true);

            while (attackDurationLeft > 0)
            {
                attackDurationLeft -= Time.deltaTime;
                yield return null;
            }
        }

        finally
        {
            _rootAttack.gameObject.SetActive(false);
            IsActing = false;
            _canWalk = true;
        }
    }

    public override void SpecialAttack()
    {
        StartActing(AcidBeamRoutine());
    }

    IEnumerator AcidBeamRoutine()
    {
        try
        {
            IsActing = true;
            _canWalk = false;
        
            Vector2 targetVector = Player.Instance.Transform.position - transform.position;
            Vector2 currentVector = targetVector;
            
            CastTimeLeft = _config.SpecialAttackCastTime;

            while (CastTimeLeft > 0)
            {
                CastTimeLeft -= Time.deltaTime;
                yield return null;
            }
        
            _acidBeam.transform.right = currentVector;
            _acidBeam.gameObject.SetActive(true);
            
            CastTimeLeft = _config.SpecialAttackDuration;
            
            while (CastTimeLeft > 0)
            {
                CastTimeLeft -= Time.deltaTime;

                targetVector = Player.Instance.Transform.position - transform.position;
                currentVector = GetValidVector(currentVector, targetVector);

                RaycastHit2D acidBeamHit = Physics2D.Raycast(_acidBeam.transform.position, currentVector,
                    _config.SpecialAttackRange, _beamStopLayer);
                Collider2D acidBeamHitCol = acidBeamHit.collider;

                _acidBeam.transform.right = currentVector;
                _acidBeamSr.size = new Vector2(acidBeamHit.distance, _acidBeamSr.size.y);

                if (acidBeamHitCol == null)
                {
                    _acidBeamSr.size = new Vector2(_config.SpecialAttackRange, _acidBeamSr.size.y);
                    yield return null;
                    continue;
                }

                IDamageable damageable = acidBeamHitCol.GetComponent<IDamageable>();

                if (damageable == null)
                {
                    yield return null;
                    continue;
                }

                Damage damage = new Damage
                {
                    Amount = _config.SpecialAttackDamage,
                    DamageType = Damage.Type.Normal,
                };
                damageable.TakeDamage(damage);

                yield return null;
            }
        }
        
        finally
        {
            _acidBeam.gameObject.SetActive(false);
            IsActing = false;
            _canWalk = true;
        }
    }
    
    Vector2 GetValidVector(Vector2 current, Vector2 target)
    {
        float currentAngle = Mathf.Atan2(current.y, current.x) * Mathf.Rad2Deg;
        float targetAngle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            
        float validAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, _anglesPerSecond * Time.deltaTime);
        float validRadian = validAngle * Mathf.Deg2Rad;
        Vector2 validVector = new Vector2(Mathf.Cos(validRadian), Mathf.Sin(validRadian));
            
        return validVector;
    }
    
    public override void Defense()
    {
        if (IsActing) return;
        SetShielded(true);
    }
}
