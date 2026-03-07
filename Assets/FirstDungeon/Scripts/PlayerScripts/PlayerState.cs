using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }
    public bool IsStunned { get; private set; }
    public bool InBog { get; private set; }
    public bool IsAlive { get; private set; } = true;

    void Awake()
    {
        Instance = this;
    }

    public void SetInBog(bool value) => InBog = value;

    public void Stun(float time) => StartCoroutine(Stunned(time));

    IEnumerator Stunned(float time)
    {
        IsStunned = true;

        float t = 0f;
        while (t < time)
        {
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        IsStunned = false;
    }

    public void Death() => IsAlive = false;
}
