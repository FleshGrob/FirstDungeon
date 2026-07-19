using System.Collections;
using FirstDungeon.Scripts.Enemies.General;
using FirstDungeon.Scripts.Enemies.General.EnemyStates;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.AI;


public abstract class Enemy : MonoBehaviour, IPullable
{
    [SerializeField] LayerMask _obstacle;
    [SerializeField] GameObject _corpsePrefab;
    [SerializeField] GameObject _energyDropPrefab;
    [SerializeField] LayerMask _projectileLayer;
    [SerializeField] protected bool _isStunned;
    
    float _hurtTime = 0.5f;
    float _defenseTimer;
    
    protected Rigidbody2D _rb;
    protected Collider2D _col;
    protected SpriteRenderer _sr;
    protected Coroutine _pullRoutine;
    EnemyHealth _health;
    Color _originalColor;

    public Transform PullTransform { get; private set; }
    public float BasicAttackTimer { get; private set; }
    public float SpecialAttackTimer { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }
    public Vector2 HomePosition { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Vector2 Facing { get; private set; } = Vector2.down;
    public GameObject Room { get; private set; }
    public bool IsInAir { get; private set; }
    public bool IsActing { get; protected set; }
    public Collider2D RoomCol { get; protected set; }
    public abstract EnemyConfig Config { get; }
    public LayerMask Obstacle => _obstacle;
    
    
    void Awake()
    {
        PullTransform = transform;
        
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _sr  = GetComponentInChildren<SpriteRenderer>();
        _health = GetComponent<EnemyHealth>();
        Agent = GetComponent<NavMeshAgent>();
        StateMachine = new EnemyStateMachine();
        Agent.updateRotation = false;  
        Agent.updateUpAxis = false;  
        _originalColor = _sr.color;
        HomePosition = transform.position;
    }

    void Start()
    {
        StateMachine.ChangeState(new PatrolState(this));
        RoomCol = GetComponentInParent<CompositeCollider2D>();

        _health.OnHurt += Hurt;
        _health.OnDeath += Die;
    }

    protected virtual void Update()
    {
        BasicAttackTimer -= Time.deltaTime;
        SpecialAttackTimer -= Time.deltaTime;
        
        if (_isStunned) return;
        
        if (!IsPlayerInRoom() && !(StateMachine.CurrentState is PatrolState))
        {
            StateMachine.ChangeState(new PatrolState(this));
            return;
        }
        
        StateMachine.Tick();
        
        if (ShouldDefend())
        {
            Defense(); 
            _defenseTimer = Config.DefenseCooldown; 
        }
        
        float ax = Mathf.Abs(Agent.velocity.x);
        float ay = Mathf.Abs(Agent.velocity.y);
        
        if (Agent.velocity.sqrMagnitude < 0.02f) return; 
        
        if (ax > ay) Facing = Agent.velocity.x > 0 ? Vector2.right : Vector2.left;
        else if (ay > ax) Facing = Agent.velocity.y > 0 ? Vector2.up : Vector2.down;
    }

    void OnDestroy()
    {
        _health.OnHurt -= Hurt;
        _health.OnDeath -= Die;
    }

    public void Move(Vector2 destination, float speed)
    {
        Agent.isStopped = false;
        Agent.speed = speed;
        Agent.SetDestination(destination);
    }

    protected virtual bool ShouldDefend()
    {
        _defenseTimer -= Time.deltaTime;
            
        if (_defenseTimer > 0) return false;
        
        if (IsActing) return false;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Config.ProjectileDetectRadius, _projectileLayer);
        foreach (Collider2D hit in hits)
        {
            Projectile projectile = hit.GetComponent<Projectile>();
            if (projectile == null) continue;
            
            Vector2 toMage = (Vector2)transform.position - (Vector2)projectile.transform.position;
            float dot = Vector2.Dot(projectile.Rb.linearVelocity, toMage);
            if (dot > 0)
                return true;
        }
        return false;
    }

    void Hurt()
    {
        StartCoroutine(HurtRoutine());
        if (StateMachine.CurrentState is PatrolState)
            StateMachine.ChangeState(new CombatState(this));
    }

    IEnumerator HurtRoutine()
    {
        _sr.color = Color.red;
        yield return new WaitForSeconds(_hurtTime);
        _sr.color = _originalColor;
    }
    
    void Die()
    {
        Instantiate(_corpsePrefab, transform.position, _corpsePrefab.transform.rotation);
        Instantiate(_energyDropPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    public void Pull(Vector2 frogPosition, float speed, float offset)
    {
        _pullRoutine = StartCoroutine(PullingRoutine(frogPosition, speed, offset));
    }

    IEnumerator PullingRoutine(Vector2 frogPosition, float speed, float offset)
    {
        Agent.enabled = false;
        _isStunned = true;
        IsInAir = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
            
        float distance = Vector2.Distance(_rb.position, frogPosition);
            
        while (distance > offset)
        {
            distance = Vector2.Distance(_rb.position, frogPosition);
            
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, frogPosition, speed * Time.fixedDeltaTime);
        
            _rb.MovePosition(newPosition);
            
            yield return new WaitForFixedUpdate();
        }

        _rb.linearVelocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _isStunned = false;
        IsInAir = false;
        Agent.enabled = true;
    }

    public void CancelPulling()
    {
        if (_pullRoutine != null)
        { 
            StopCoroutine(_pullRoutine);
            _rb.linearVelocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _isStunned = false;
            Agent.enabled = true;
            _pullRoutine = null;
        }
    }
    
    public bool IsPlayerInRoom() => RoomCol.OverlapPoint(Player.Instance.Transform.position);
    public void ResetBasicTimer() => BasicAttackTimer = Config.BasicAttackCooldown;
    public void ResetSpecialTimer() => SpecialAttackTimer = Random.Range(Config.SpecialAttackCooldownMin, Config.SpecialAttackCooldownMax);

    public abstract void BasicAttack();
    public abstract void SpecialAttack();
    public abstract void Defense();
    
    
    
    protected virtual void OnDrawGizmosSelected()
    {
        if (Config == null) return;
    
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Config.PatrolRadius);
    
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Config.AggroRadius);
    
        Gizmos.color = new Color(1f, 0.5f, 0f);
        Gizmos.DrawWireSphere(transform.position, Config.AttackRange);
    
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Config.ProjectileDetectRadius);
        Gizmos.DrawWireSphere(transform.position, Config.PlayerProximityRadius);
    }
}
