using System;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] Color _healColor;
    [SerializeField] Color _replenishColor;
    [SerializeField] Color _stunnedColor;
    [SerializeField] Color _invulnerableColor;
    
    SpriteRenderer _playerSpriteRenderer;
    Sprite _playerSprite;
    Color _playerColor;

    EnergyStone _energyStone => Player.Instance.EnergyStone;

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
