using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Collider2D doorCollider;
    SpriteRenderer spriteRenderer;
    public Sprite openedSprite;
    public Sprite closedSprite;


    void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenDoor()
    {
        doorCollider.enabled = false;
        spriteRenderer.sprite = openedSprite;
    }

    public void CloseDoor()
    {
        doorCollider.enabled = true;
        spriteRenderer.sprite = closedSprite;
    }
}
