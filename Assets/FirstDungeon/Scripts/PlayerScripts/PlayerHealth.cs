using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    float health = 5;
    float maxHealth = 5;
    

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            PlayerState.Instance.Death();
        Debug.Log(health);
    }

    public void Heal(float hp)
    {
        if (health == maxHealth)
            return;
        health += hp;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void UpgradeHealth(float hpUpgrade)
    {
        maxHealth += hpUpgrade;
    }

}
