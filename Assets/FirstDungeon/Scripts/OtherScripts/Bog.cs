using UnityEngine;

public class Bog : MonoBehaviour
{
    Collider2D bogCol;

    void Awake()
    {
        bogCol = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
            PlayerState.Instance.SetInBog(true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerMovement playerM = other.GetComponent<PlayerMovement>();
        IDamageable playerHP = other.GetComponent<IDamageable>();

        if (playerM != null)
        {
            Vector2 playerPos = playerM.Rb.position;
            if (bogCol.OverlapPoint(playerPos) == true && PlayerState.Instance.InBog == true &&
                PlayerState.Instance.IsStunned == false && playerHP != null)
            {
                playerHP.TakeDamage(1);
                playerM.Drown(1);
                PlayerState.Instance.Stun(1);
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
            PlayerState.Instance.SetInBog(false);
    }
}
