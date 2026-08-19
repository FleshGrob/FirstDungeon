using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class HealMarker : MonoBehaviour
{
    SpriteRenderer _sr;


    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _sr.enabled = Player.Instance.EnergyStone.IsHealing;
    }
}
