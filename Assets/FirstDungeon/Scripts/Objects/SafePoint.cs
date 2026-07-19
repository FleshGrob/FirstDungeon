using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    bool _isPlayerInRange;
    
    
    void Start()
    {
        InputManager.Instance.OnInteractKeyPressed += Restore;
    }

    void OnDestroy()
    {
        InputManager.Instance.OnInteractKeyPressed -= Restore;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null) _isPlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null) _isPlayerInRange = false;
    }

    void Restore()
    {
        if (!Player.Instance.State.CanDo(PlayerState.PlayerAction.Interact)) return;
        
        if (!_isPlayerInRange) return;
        
        Player.Instance.Health.HealFull();
        Player.Instance.Mana.ReplenishFull();
        Player.Instance.EnergyStone.RefillFullCharges();
    }
}
