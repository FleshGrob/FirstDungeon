using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTManager : MonoBehaviour
{
    [SerializeField] Color[] cycleColors;
    [SerializeField] ColorTarget[] targets;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] Transform chestSpawnPos;

    public Color[] CycleColors => cycleColors;
    public int GreenIndex;

    bool solved;
    public bool IsSolved => solved;


    public void OnTargetHit()
    {
        if (solved == true)
            return;
        if (AllGreen(targets) == true)
            solved = true;
        if (solved == true)
            Instantiate(chestPrefab, chestSpawnPos.position, Quaternion.identity);
    }

    bool AllGreen(ColorTarget[] targets)
    {
        foreach (ColorTarget t in targets)
        {
            if (t.IsGreen == false)
                return false;
        }
        return true;
    }
}
