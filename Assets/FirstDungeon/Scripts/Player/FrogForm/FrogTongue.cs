using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{
    
    void Update()
    {
        transform.up = Player.Instance.Movement.Facing;
    }
}
