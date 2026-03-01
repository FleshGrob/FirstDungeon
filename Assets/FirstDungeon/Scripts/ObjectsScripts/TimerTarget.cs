using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTarget : MonoBehaviour
{

    [SerializeField] TTManager manager;
    [SerializeField] Color hitColor;
    Color originalColor;
    
    bool activated;
    public bool IsActivated => activated;
    float timer;

    SpriteRenderer sr;
    FrogProjectile frog;
    


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Update()
    {
        if (manager.Solved == true) return;

        if (timer > 0f) timer -= Time.deltaTime;
        else activated = false;

        if (activated == false) sr.color = originalColor;
    }

    public void Hit()
    {
        if (activated == true)
            return;
        activated = true;
        sr.color = hitColor;
        timer = manager.FullTimer;

        manager.OnTargetHit();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        frog = collision.GetComponent<FrogProjectile>();
        if (frog != null)
            Hit();
    }
}
