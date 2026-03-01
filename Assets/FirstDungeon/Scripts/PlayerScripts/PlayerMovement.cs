using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 4f;

    public Rigidbody2D rb;
    Vector2 input;
    Vector2 facing = Vector2.down;

    public Vector2 Facing => facing;
    public Vector2 InputRaw => input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        float ax = Mathf.Abs(input.x);
        float ay = Mathf.Abs(input.y);

        if (ax > ay) facing = input.x > 0 ? Vector2.right : Vector2.left;
        else if (ay > ax) facing = input.y > 0 ? Vector2.up : Vector2.down;
    }

    void FixedUpdate()
    {
        rb.velocity = speed * input.normalized;
    }
}