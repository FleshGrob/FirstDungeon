using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChTManager : MonoBehaviour
{
    [SerializeField] ChaosTarget[] targets;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] Transform chestSpawnPos;

    bool solved;
    public bool Solved => solved;


    public void OnTargetHit()
    {
        if (solved == true)
            return;

        foreach (ChaosTarget t in targets)
        {
            if (t.Activated == false) return;
        }

        solved = true;
        if (solved == true)
            Instantiate(chestPrefab, chestSpawnPos.position, Quaternion.identity);
    }
}
