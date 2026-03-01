using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTarget : MonoBehaviour
{

    [SerializeField] CTManager manager;
    public bool IsGreen => colorIndex == manager.GreenIndex;

    int colorIndex;
    SpriteRenderer sr;
    FrogProjectile frog;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Hit()
    {
        if (manager == null)
            return;
        if (manager.IsSolved == true)
            return;
        AdvanceColor();
        ApplyColor();
        manager.OnTargetHit();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        frog = collision.GetComponent<FrogProjectile>();
        if (frog != null)
            Hit();
    }

    void AdvanceColor()
    {
        colorIndex = (colorIndex + 1) % manager.CycleColors.Length;
    }

    void ApplyColor()
    {
        sr.color = manager.CycleColors[colorIndex];
    }
}
