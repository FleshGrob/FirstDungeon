using System.Collections;
using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class EnergyStone : MonoBehaviour
{
    [SerializeField] int _charges;
    [SerializeField] int _healAmount;
    [SerializeField] int _replenishAmount;
    [SerializeField] float _healTime;
    [SerializeField] float _replenishTime;
    
    Coroutine _castRoutine;
    
    PlayerState State => Player.Instance.State;
    PlayerHealth Health => Player.Instance.Health;
    PlayerMana Mana => Player.Instance.Mana;


    void Start()
    {
        InputManager.Instance.OnCastKeyPressed += Cast;
        Health.OnDamaged += CancelCast;
        State.OnStunned += CancelCast;
    }

    void OnDestroy()
    {
        InputManager.Instance.OnCastKeyPressed -= Cast;
        Health.OnDamaged -= CancelCast;
        State.OnStunned -= CancelCast;
    }

    public void GainCharge(int amount)
    {
        _charges += amount;
    }

    void Cast()
    {
        if (!State.CanDo(PlayerState.PlayerAction.CastStone)) return;
        if (_charges <= 0) return;
        
        switch (InputManager.Instance.IsAltHeld)
        {
            case false:
                if (!Health.FullHealth) CastHeal();
                break;
            case true:
                if (!Mana.FullMana) CastReplenish();
                break;
        }
    }

    void CastHeal()
    {
        _castRoutine = StartCoroutine(HealRoutine());
    }

    IEnumerator HealRoutine()
    {
        _charges -= 1;
        
        State.SetActing(true);
        State.SetRooted(true);
        
        yield return new WaitForSeconds(_healTime);
        Health.Heal(_healAmount);
        
        State.SetActing(false);
        State.SetRooted(false);
        
        _castRoutine = null;
    }

    void CastReplenish()
    {
        _castRoutine = StartCoroutine(ReplenishRoutine());
    }

    IEnumerator ReplenishRoutine()
    {
        State.SetActing(true);
        State.SetRooted(true);

        float t = _replenishTime;
        
        while (InputManager.Instance.IsCastHeld && _charges > 0)
        {
            _charges -= 1;
            
            while (InputManager.Instance.IsCastHeld && t > 0)
            {
                t -= Time.deltaTime;
                yield return null;
            }

            if (t > 0) break;
            Mana.Replenish(_replenishAmount);
            t = _replenishTime;
        }
        
        State.SetActing(false);
        State.SetRooted(false);
        
        _castRoutine = null;
    }
    
    void CancelCast()
    {
        if (_castRoutine == null)  return;
        
        StopCoroutine(_castRoutine);
        _castRoutine = null;
        
        State.SetActing(false);
        State.SetRooted(false);
    }
}
