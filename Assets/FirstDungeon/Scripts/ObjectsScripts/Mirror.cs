using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    private void OnTriggerEnter2D (Collider2D other)
    {
        FrogProjectile fp = other.gameObject.GetComponent<FrogProjectile>();
        if (fp != null)
        {
            Vector2 normal = transform.right;
            Vector2 dir = fp.Rb.velocity.normalized;
            Vector2 newDir = Vector2.Reflect(dir, normal);

            fp.Reflection(newDir);
        }

    }
}
