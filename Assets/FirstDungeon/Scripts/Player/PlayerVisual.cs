using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] Color _stunnedColor;
    [SerializeField] Color _invulnerableColor;
    
    SpriteRenderer _playerSpriteRenderer;
    Sprite _playerSprite;
    Color _playerColor;


    void Awake()
    {
        _playerSpriteRenderer  = GetComponent<SpriteRenderer>();
        _playerSprite = _playerSpriteRenderer.sprite;
        _playerColor = _playerSpriteRenderer.color;
    }

    public void BackToNormal()
    {
        _playerSpriteRenderer.color = _playerColor;
    }
    
    public void ShowStun()
    {
        _playerSpriteRenderer.color  = _stunnedColor;
    }
    
    public void ShowInvulnerable()
    {
        _playerSpriteRenderer.color  = _invulnerableColor;
    }
    
    
}
