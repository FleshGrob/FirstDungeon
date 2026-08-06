using UnityEngine;

public class CastMarker : MonoBehaviour
{
    SpriteRenderer _sr;
    Enemy _enemy;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _enemy = GetComponentInParent<Enemy>();
    }

    void Update()
    {
        _sr.enabled = _enemy.IsActing;
    }
}
