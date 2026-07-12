using System;
using System.Collections;
using UnityEngine;

public class Pullable : MonoBehaviour, IPullable
{
    Coroutine _pullRoutine;
    
    public Transform PullTransform { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public bool IsInAir { get; private set; }
    
    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        PullTransform = transform;
    }

    public void Pull(Vector2 frogPosition, float speed, float offset)
    {
        _pullRoutine = StartCoroutine(PullingRoutine(frogPosition, speed, offset));
    }

    IEnumerator PullingRoutine(Vector2 frogPosition, float speed, float offset)
    {
        Rb.bodyType = RigidbodyType2D.Dynamic;
        IsInAir  = true;
            
        float distance = Vector2.Distance(Rb.position, frogPosition);
            
        while (distance > offset)
        {
            distance = Vector2.Distance(Rb.position, frogPosition);
            
            Vector2 newPosition = Vector2.MoveTowards(Rb.position, frogPosition, speed * Time.fixedDeltaTime);
        
            Rb.MovePosition(newPosition);
            
            yield return new WaitForFixedUpdate();
        }

        Rb.linearVelocity = Vector2.zero;
        IsInAir  = false;
        Rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void CancelPulling()
    {
        if (_pullRoutine != null)
        { 
            StopCoroutine(_pullRoutine);
            Rb.linearVelocity = Vector2.zero;
            IsInAir  = false;
            Rb.bodyType = RigidbodyType2D.Kinematic;
            _pullRoutine = null;
        }
    }
}
