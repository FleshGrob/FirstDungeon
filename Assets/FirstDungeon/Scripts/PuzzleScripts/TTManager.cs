using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTManager : MonoBehaviour
{
    [SerializeField] TimerTarget[] targets;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] Transform chestSpawnPos;
    [SerializeField] float fullTimer = 1.5f;
    public float FullTimer => fullTimer;

    bool solved;
    public bool Solved => solved;


    public void OnTargetHit()
    {
        if (solved == true)
            return;

        foreach (TimerTarget t in targets)
        {
            if (t.IsActivated == false) return;
        }

        solved = true;

        if (solved == true)
            Instantiate(chestPrefab, chestSpawnPos.position, Quaternion.identity);
    }
}