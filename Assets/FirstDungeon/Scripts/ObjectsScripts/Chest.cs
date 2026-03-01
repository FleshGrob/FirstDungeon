using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool isOpened = false;
    bool playerInRange = false;

    PlayerInventory playerInventory;

    void Update()
    {
        if (playerInRange && !isOpened && Input.GetKeyDown(GameKeys.Action))
        {
            Open();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory == null) return;

        playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInventory>() == null) return;

        playerInRange = false;
        playerInventory = null;
    }

    void Open()
    {
        isOpened = true;
        playerInventory.AddKey();

        Destroy(gameObject);
    }
}