using System;
using System.Collections;
using UnityEngine;

public class Pullable : MonoBehaviour, IPullable
{
    Rigidbody2D _rb;
    Coroutine _pullRoutine;
    public Transform PullTransform { get; private set; }
    
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        PullTransform = transform;
    }

    public void Pull(Vector2 frogPosition, float speed, float offset)
    {
        _pullRoutine = StartCoroutine(PullingRoutine(frogPosition, speed, offset));
    }

    IEnumerator PullingRoutine(Vector2 frogPosition, float speed, float offset)
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
            
        float distance = Vector2.Distance(_rb.position, frogPosition);
            
        while (distance > offset)
        {
            distance = Vector2.Distance(_rb.position, frogPosition);
            
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, frogPosition, speed * Time.fixedDeltaTime);
        
            _rb.MovePosition(newPosition);
            
            yield return new WaitForFixedUpdate();
        }

        _rb.linearVelocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void CancelPulling()
    {
        if (_pullRoutine != null)
        { 
            StopCoroutine(_pullRoutine);
            _rb.linearVelocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _pullRoutine = null;
        }
    }
}
