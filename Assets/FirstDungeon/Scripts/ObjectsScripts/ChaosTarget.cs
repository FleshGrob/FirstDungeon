using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosTarget : MonoBehaviour
{
    [SerializeField] ChTManager manager;
    [SerializeField] Color activeColor;
    SpriteRenderer sr;

    bool activated = false;
    public bool Activated => activated;
    
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        FrogProjectile frog = other.GetComponent<FrogProjectile>();
        if (frog != null && frog.Dark == true)
            Hit();
    }

    void Hit()
    {
        activated = true;
        sr.color = activeColor;
        manager.OnTargetHit();
    }
}
