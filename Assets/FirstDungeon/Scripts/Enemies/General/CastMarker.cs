using UnityEngine;

public class CastMarker : MonoBehaviour
{
    SpriteRenderer _sr;
    Enemy _enemy;
    Vector3 _baseScale;

    
    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _enemy = GetComponentInParent<Enemy>();
        _baseScale = transform.localScale;
    }

    void Update()
    {
        _sr.enabled = _enemy.IsActing;
        transform.localScale = new Vector3(_baseScale.x * _enemy.CastTimeLeft, _baseScale.y, _baseScale.z);
    }
}
