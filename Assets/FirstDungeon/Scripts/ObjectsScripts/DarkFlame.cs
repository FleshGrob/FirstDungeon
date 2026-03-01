using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFlame : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        FrogProjectile frog = other.GetComponent<FrogProjectile>();
        SpriteRenderer frogsr = other.GetComponent<SpriteRenderer>();
        if (frog == null) return;
        frog.Dark = true;
        frogsr.color = frog.DarkColor;
    }
}
