using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSwitch : MonoBehaviour
{
    [SerializeField] Mirror mirror;
    bool playerInRange;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerInRange = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(GameKeys.Action) && playerInRange == true)
        {
            mirror.transform.Rotate(0, 0, 45);
        }
    }
}
