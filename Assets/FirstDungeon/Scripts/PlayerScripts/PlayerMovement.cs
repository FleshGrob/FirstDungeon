using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 4f;

    public Rigidbody2D Rb { get; private set; }
    public Vector2 SafePos;
    Vector2 input;
    Vector2 facing = Vector2.down;

    public Vector2 Facing => facing;
    public Vector2 InputRaw => input;

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        float ax = Mathf.Abs(input.x);
        float ay = Mathf.Abs(input.y);

        if (ax > ay) facing = input.x > 0 ? Vector2.right : Vector2.left;
        else if (ay > ax) facing = input.y > 0 ? Vector2.up : Vector2.down;

        if (!PlayerState.Instance.InBog) SafePos = Rb.position;
    }

    void FixedUpdate()
    {
        if (PlayerState.Instance.IsStunned)
        {
            Rb.velocity = Vector2.zero;
            return;
        }

        Rb.velocity = speed * input.normalized;
    }

    public void Drown(float time) => StartCoroutine(Drowning(time));

    IEnumerator Drowning (float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Rb.position = SafePos;
        PlayerState.Instance.SetInBog(false);
    }

}