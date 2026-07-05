using System;
using System.Collections;
using FirstDungeon.Scripts.Managers;
using UnityEngine;

public class FrogShapeshift : MonoBehaviour
{
    [SerializeField] Sprite _frogSprite;
    [SerializeField] GameObject _frogTongue;
    [SerializeField] float _hookingTime;
    [SerializeField] float _hookRange;
    [SerializeField] LayerMask _includedLayers;
    
    bool _isFrog;
    bool _canHook;
    SpriteRenderer _sR;
    SpriteRenderer _tongueSr;
    Sprite _originalSprite;

    void Awake()
    {
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
        float progress = 0;
        float length = 0;
        
        while (length < _hookRange)
        {
            progress += Time.deltaTime / _hookingTime;
            progress = Mathf.Clamp01(progress);
            length = progress * _hookRange;
            
            RaycastHit2D hookHit = Physics2D.Raycast(transform.position, transform.up, length, _includedLayers);
            Collider2D hookHitCol = hookHit.collider;

            _tongueSr.size = new Vector2(_tongueSr.size.x, length);
            
            yield return null;
        }
    }
}
