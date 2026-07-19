using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class PlayerFeet : MonoBehaviour, IDamageable
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bog>() != null) Player.Instance.State.SetInBog(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Bog>() != null) Player.Instance.State.SetInBog(false);
    }

    public int TakeDamage(Damage damage)
    {
        return Player.Instance.Health.TakeDamage(damage);
    }
}
