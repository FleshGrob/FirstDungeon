using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogProjectile : MonoBehaviour
{
    public Color DarkColor;   
    public Rigidbody2D Rb => rb;
    public bool Dark = false;

    float speed;
    float lifeTime = 8f;
    Rigidbody2D rb;
    bool death = false;
    public event Action<bool> OnDeath;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    public void Launch(Vector2 dir, float speed)
    {
        if (dir == Vector2.zero)
            return;
        dir = dir.normalized;
        this.speed = speed;
        rb.velocity = dir * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dark") == true) return;
        if (other.GetComponent<Mirror>() != null) return;
        if (other.GetComponent<Ring>() != null) return;
        death = true;
        Destroy(gameObject);
        
    }

    public void Reflection (Vector2 newDir)
    {
        rb.velocity = newDir * speed;
    }

    void OnDestroy()
    {
        OnDeath?.Invoke(death);
    }
}
