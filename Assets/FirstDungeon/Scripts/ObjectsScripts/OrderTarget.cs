using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTarget : MonoBehaviour
{

    [SerializeField] OTManager manager;
    [SerializeField] Color idleColor;
    [SerializeField] Color correctColor;

    FrogProjectile frog;
    SpriteRenderer sr;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = idleColor;
    }

    public void Hit()
    {
        if (manager == null)
            return;
        manager.OnTargetHit(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        frog = collision.GetComponent<FrogProjectile>();
        if (frog != null)
            Hit();
    }

    public void SetIdle()
    {
        sr.color = idleColor;
    }

    public void SetCorrect()
    {
        sr.color = correctColor;
    }
}
