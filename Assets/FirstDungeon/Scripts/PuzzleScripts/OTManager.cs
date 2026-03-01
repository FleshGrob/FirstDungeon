using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTManager : MonoBehaviour
{
    [SerializeField] OrderTarget[] order;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] Transform chestSpawnPos;

    int currentIndex = 0;
    bool solved;


    public void OnTargetHit(OrderTarget t)
    {
        if (solved == true)
            return;
        if (t == order[currentIndex])
        {
            t.SetCorrect();
            currentIndex += 1;
        }
        else
        {
            ResetAll();
            currentIndex = 0;
        }
        if (currentIndex >= order.Length)
        {
            solved = true;
            Instantiate(chestPrefab, chestSpawnPos.position, Quaternion.identity);
        }

    }


    void ResetAll()
    {
        foreach (OrderTarget t in order)
            t.SetIdle();
    }
}
