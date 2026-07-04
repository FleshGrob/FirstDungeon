using FirstDungeon.Scripts.EffectsScripts;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Explosion>() != null)
            Destroy(gameObject);
    }
}
