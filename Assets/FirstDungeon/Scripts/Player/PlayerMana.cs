using System;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] int _maxMana;

    int _currentMana;
    
    public event Action OnManaChanged;


    void Awake()
    {
        _currentMana = _maxMana;
        Debug.Log($"PlayerMana: {_currentMana} / {_maxMana}");
    }

    public bool Has(int amount)
    {
        return amount <= _currentMana;
    }

    public void Spend(int amount)
    {
        if (!Has(amount)) return;
        
        _currentMana -= amount;
        OnManaChanged?.Invoke();
        
        Debug.Log($"PlayerMana: {_currentMana} / {_maxMana}");
    }

    public void Restore(int amount)
    {
        _currentMana += amount;
        if (_currentMana > _maxMana) _currentMana = _maxMana;
        
        OnManaChanged?.Invoke();
        Debug.Log($"PlayerMana: {_currentMana} / {_maxMana}");
    }

    public void RestoreFull()
    {
        _currentMana = _maxMana;
        OnManaChanged?.Invoke();
    }
}
