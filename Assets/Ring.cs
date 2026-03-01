using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] RingManager manager;
    [SerializeField] Color hitColor;

    public bool Activated = false;
    FrogProjectile frog;
    int frogID;
    public SpriteRenderer sr;
    public Color originalColor;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        frog = collision.GetComponent<FrogProjectile>();
        if (frog != null)
        {
            frogID = frog.GetInstanceID();
            Hit();
        }
    }

    public void Hit()
    {
        if (manager == null)
            return;
        if (manager.Solved() == true)
            return;
        if (manager.rightID == 0)
        {
            manager.GetFrog(frogID, frog);
            Activated = true;
            sr.color = hitColor;
        }
        else if (frogID == manager.rightID)
        {
            Activated = true;
            sr.color = hitColor;
            manager.Solved();
        }
        else
        {
            manager.Reset();
        }

    }

}
