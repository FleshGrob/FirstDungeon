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
    [SerializeField] float _speed;
    [SerializeField] float _offset;
    
    bool _isFrog;
    bool _canHook;
    
    Rigidbody2D _rb;
    CapsuleCollider2D _col;
    SpriteRenderer _sR;
    SpriteRenderer _tongueSr;
    Sprite _originalSprite;
    HookingState _currentState;

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
    }

    void OnDestroy()
    {
        InputManager.Instance.OnShapeshiftPressed -= Shapeshift;
        InputManager.Instance.OnAbilityPressed -= HookShot;
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
        if (_canHook) StartCoroutine(HookingRoutine());
    }
    
    IEnumerator HookingRoutine()
    {
        InputManager.Instance.BlockGameplay();
        _currentState = HookingState.Hooking;
        
        float progress = 0;
        float length = 0;
        
        RaycastHit2D hookHit = default;
        IPullable pullable = null;
        HookAnchor hookAnchor = null;
        
        float distance = Vector2.Distance(_rb.position, hookHit.point);
        
        while (_currentState == HookingState.Hooking && length < _hookRange)
        {
            progress += Time.deltaTime / _hookingTime;
            progress = Mathf.Clamp01(progress);
            length = progress * _hookRange;
            
            _tongueSr.size = new Vector2(_tongueSr.size.x, length);
            
            hookHit = Physics2D.Raycast(transform.position, Player.Instance.Movement.Facing, length, _includedLayers);
            Collider2D hookHitCol = hookHit.collider;
            
            if (hookHit.collider != null)
            {
                pullable = hookHit.collider.GetComponent<IPullable>();
                hookAnchor = hookHit.collider.GetComponent<HookAnchor>();
            }
            
            if (hookAnchor != null) _currentState = HookingState.Flying;
            if (pullable != null) _currentState = HookingState.Pulling;
            
            yield return null;
        }

        while (_currentState == HookingState.Flying && distance > _offset)
        {
            distance = Vector2.Distance(_rb.position, hookHit.point);
            
            Vector2 target = hookHit.point;
            
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, target, _speed * Time.deltaTime);
        
            _rb.MovePosition(newPosition);
            
            _tongueSr.size = new Vector2(_tongueSr.size.x, distance);
            
            yield return null;
        }

        while (_currentState == HookingState.Hooking && length > 0)
        {
            progress -= Time.deltaTime / _hookingTime;
            progress = Mathf.Clamp01(progress);
            length = progress * _hookRange;
            
            _tongueSr.size = new Vector2(_tongueSr.size.x, length);
            
            yield return null;
        }
        
        _tongueSr.size = new Vector2(_tongueSr.size.x, 0);
        InputManager.Instance.UnBlockGameplay();
        _currentState = HookingState.Idle;
    }
}
