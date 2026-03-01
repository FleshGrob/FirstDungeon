using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpen;
    bool playerInRange;

    PlayerInventory inv;

    void Update()
    {
        if (playerInRange && !isOpen && Input.GetKeyDown(GameKeys.Action))
        {
            TryOpen();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;

        playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInventory>() == null) return;

        playerInRange = false;
        inv = null;
    }

    void TryOpen()
    {
        if (inv.UseKey())
        {
            Open();
        }
    }

    void Open()
    {
        isOpen = true;
        Destroy(gameObject);
    }
}