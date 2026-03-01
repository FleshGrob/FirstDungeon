using UnityEngine;

public class FrogChest : MonoBehaviour
{
    bool isOpened = false;
    bool playerInRange = false;
    PlayerShooter shooter;

    void Update()
    {
        if (playerInRange && !isOpened && Input.GetKeyDown(GameKeys.Action))
            Open();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        shooter = other.GetComponent<PlayerShooter>();
        if (shooter == null) return;

        playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerShooter>() == null) return;

        playerInRange = false;
        shooter = null;
    }

    void Open()
    {
        if (shooter == null) return;

        isOpened = true;
        shooter.UnlockFrogStaff();

        Destroy(gameObject);
    }
}