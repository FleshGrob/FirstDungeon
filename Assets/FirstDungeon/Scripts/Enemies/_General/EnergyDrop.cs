using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class EnergyDrop : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.EnergyStone.GainCharge(1);
            Destroy(gameObject);
        }
    }
}
