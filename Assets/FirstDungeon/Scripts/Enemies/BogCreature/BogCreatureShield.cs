using UnityEngine;

public class BogCreatureShield : MonoBehaviour
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
        _sr.enabled = _enemy.IsShielded;
    }
}
