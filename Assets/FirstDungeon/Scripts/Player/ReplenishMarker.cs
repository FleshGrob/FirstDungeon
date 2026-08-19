using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class ReplenishMarker : MonoBehaviour
{
    SpriteRenderer _sr;


    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _sr.enabled = Player.Instance.EnergyStone.IsReplenishing;
    }
}
