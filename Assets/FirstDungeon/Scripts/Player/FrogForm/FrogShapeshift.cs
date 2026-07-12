using System;
using System.Collections;
using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class FrogShapeshift : MonoBehaviour
{
    enum HookingState
    {
        Idle,
        Hooking,
        Flying,
        Pulling,
        Retracting
    }
    
    [SerializeField] Sprite _frogSprite;
    [SerializeField] GameObject _frogTongue;
    [SerializeField] float _hookingTime;
    [SerializeField] float _hookRange;
    [SerializeField] LayerMask _includedLayers;
    [SerializeField] float _flyingSpeed;
    [SerializeField] float _pullingSpeed;
    [SerializeField] float _flyingOffset;
    [SerializeField] float _pullingOffset;
    
    bool _isFrog;
    bool _canHook;
    
    Rigidbody2D _rb;
    CapsuleCollider2D _col;
    SpriteRenderer _sR;
    SpriteRenderer _tongueSr;
    Sprite _originalSprite;
    HookingState _currentState;
    Coroutine _hookingRoutine;
    IPullable _pullable;
    Transform _targetTransform;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _sR  = GetComponent<SpriteRenderer>();
        _originalSprite = _sR.sprite;
        _tongueSr = _frogTongue.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InputManager.Instance.OnShapeshiftPressed += Shapeshift;
        InputManager.Instance.OnAbilityPressed += HookShot;
        Player.Instance.State.OnStunned += StopHooking;
    }

    void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnShapeshiftPressed -= Shapeshift;
            InputManager.Instance.OnAbilityPressed -= HookShot;
            Player.Instance.State.OnStunned -= StopHooking;
        }
    }

    void Shapeshift()
    {
        if (!_isFrog) TurnIntoFrog();
        else TurnIntoHuman();
    }

    void TurnIntoFrog()
    {
        _isFrog = true;
        _canHook = true;
        _sR.sprite = _frogSprite;
    }

    void TurnIntoHuman()
    {
        _isFrog = false;
        _canHook = false;
        _sR.sprite = _originalSprite;
    }

    void HookShot()
    {
        if (_canHook) _hookingRoutine = StartCoroutine(HookingRoutine());
    }
    
    IEnumerator HookingRoutine()
    {
        InputManager.Instance.BlockGameplay();
        _currentState = HookingState.Hooking;
        
        float progress = 0;
        float length = 0;
        float distance = _hookRange;
        
        RaycastHit2D hookHit = default;
        HookAnchor hookAnchor = null;
        Vector2 previousPosition = Vector2.zero;
        float previousDistance = 0;
        
        while (_currentState == HookingState.Hooking && length < _hookRange)
        {
            progress += Time.fixedDeltaTime /_hookingTime;
            progress = Mathf.Clamp01(progress);
            length = progress * _hookRange;
            
            _tongueSr.size = new Vector2(_tongueSr.size.x, length);
            
            hookHit = Physics2D.Raycast(transform.position, Player.Instance.Movement.Facing, length, _includedLayers);
            
            if (hookHit.collider != null)
            {
                _pullable = hookHit.collider.GetComponent<IPullable>();
                hookAnchor = hookHit.collider.GetComponent<HookAnchor>();
            }
            
            if (hookAnchor != null) _currentState = HookingState.Flying;
            if (_pullable != null)
            {
                _currentState = HookingState.Pulling;
                _targetTransform  = _pullable.PullTransform;
                _pullable.Pull(_rb.position, _pullingSpeed, _pullingOffset);
            }
            
            yield return new WaitForFixedUpdate();
        }

        Player.Instance.State.GetInAir(true);
        
        while (_currentState == HookingState.Flying && distance > _flyingOffset)
        {
            Vector2 target = hookHit.point;
            
            distance = Vector2.Distance(_rb.position, target);
            
            if (distance == previousDistance)
            {
                StopHooking();
                yield break;
            }
            
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, target, _flyingSpeed * Time.fixedDeltaTime);
        
            _rb.MovePosition(newPosition);
            
            _tongueSr.size = new Vector2(_tongueSr.size.x, distance);
            
            previousDistance = distance;
            
            yield return new WaitForFixedUpdate();
        }
        
        Player.Instance.State.GetInAir(false);

        while (_currentState == HookingState.Pulling && distance > _pullingOffset)
        {
            if (_targetTransform == null)
            {
                StopHooking(); 
                yield break;
            }
            
            Vector2 target = _targetTransform.position;
            
            distance = Vector2.Distance(_rb.position, target);
            
            if (distance == previousDistance)
            {
                StopHooking();
                yield break;
            }
            
            _tongueSr.size = new Vector2(_tongueSr.size.x, distance);
            
            previousDistance = distance;
            
            yield return new WaitForFixedUpdate();
        }
        
        while (_currentState == HookingState.Hooking && length > 0)
        {
            progress -= Time.fixedDeltaTime / _hookingTime;
            progress = Mathf.Clamp01(progress);
            length = progress * _hookRange;
            
            _tongueSr.size = new Vector2(_tongueSr.size.x, length);
            
            yield return new WaitForFixedUpdate();
        }
        
        InputManager.Instance.UnBlockGameplay();

        _tongueSr.size = new Vector2(_tongueSr.size.x, 0);
        _currentState = HookingState.Idle;
        _pullable  = null;
        _hookingRoutine  = null;
    }

    public void StopHooking()
    {
        if (_currentState == HookingState.Idle) return;
        
        StopCoroutine(_hookingRoutine);
        InputManager.Instance.UnBlockGameplay();
        
        if (_currentState == HookingState.Flying) Player.Instance.State.GetInAir(false);
        if (_targetTransform != null) _pullable.CancelPulling();
        
        _pullable = null;
        _hookingRoutine  = null;
        _tongueSr.size = new Vector2(_tongueSr.size.x, 0);
        _currentState = HookingState.Idle;
    }
}
