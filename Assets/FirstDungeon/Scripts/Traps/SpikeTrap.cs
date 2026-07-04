using System;
using FirstDungeon.Scripts.ObjectsScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    Spikes _spikes;
    float _spikesTimer;
    


    void Start()
    {
        _spikes = GetComponentInChildren<Spikes>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
            _spikes.gameObject.SetActive(true);
    }
}
