using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] Ring[] rings;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] Transform chestSpawnPos;

    public int rightID { get; private set; } = 0;
    FrogProjectile frog;


    public void GetFrog(int frogID, FrogProjectile fr)
    {
        frog = fr;
        rightID = frogID;
        frog.OnDeath += Fail;
    }

    public bool Solved()
    {
       foreach(Ring r in rings)
       {
            if (r.Activated == false)
                return false;
       }
        Instantiate(chestPrefab, chestSpawnPos.position, Quaternion.identity);
        return true; 
    }

    public void Reset()
    {
        rightID = 0;
        foreach (Ring r in rings)
        {
            r.Activated = false;
            r.sr.color = r.originalColor;
        }
    }

    void Fail(bool death)
    {
        frog.OnDeath -= Fail;
        if (Solved() == false)
        {
            Reset();
        }
        else return;
    }
}
