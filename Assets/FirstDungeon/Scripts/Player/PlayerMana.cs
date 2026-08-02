using System;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int MaxMana { get; private set; } = 12;
    public int CurrentMana { get; private set; } = 12;

    public bool FullMana => CurrentMana == MaxMana;
    
    public event Action OnManaChanged;


    void Awake()
    {
        CurrentMana = MaxMana;
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
    }

    public void Replenish(int amount)
    {
        CurrentMana += amount;
        if (CurrentMana > MaxMana) CurrentMana = MaxMana;
        
        OnManaChanged?.Invoke();
    }

    public void ReplenishFull()
    {
        CurrentMana = MaxMana;
        OnManaChanged?.Invoke();
    }
}
