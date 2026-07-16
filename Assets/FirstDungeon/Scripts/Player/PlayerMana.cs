using System;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int MaxMana { get; private set; } = 12;
    public int CurrentMana { get; private set; } = 12;
    
    public event Action OnManaChanged;


    void Awake()
    {
        CurrentMana = MaxMana;
        Debug.Log($"PlayerMana: {CurrentMana} / {MaxMana}");
    }

    public bool Has(int amount)
    {
        return amount <= CurrentMana;
    }

    public void Spend(int amount)
    {
        if (!Has(amount)) return;
        
        CurrentMana -= amount;
        OnManaChanged?.Invoke();
        
        Debug.Log($"PlayerMana: {CurrentMana} / {MaxMana}");
    }

    public void Restore(int amount)
    {
        CurrentMana += amount;
        if (CurrentMana > MaxMana) CurrentMana = MaxMana;
        
        OnManaChanged?.Invoke();
        Debug.Log($"PlayerMana: {CurrentMana} / {MaxMana}");
    }

    public void RestoreFull()
    {
        CurrentMana = MaxMana;
        OnManaChanged?.Invoke();
    }
}
