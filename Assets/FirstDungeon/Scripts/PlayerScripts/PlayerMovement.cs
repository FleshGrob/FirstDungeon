using System.Collections;
using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _speed;

        public MovingPlatform Platform; 
        
        Vector2 _movementInput;
        
        public Vector2 MovementInputRaw => _movementInput;
        public Vector2 Facing { get; private set; } = Vector2.down;
        public Rigidbody2D Rb { get; private set; }
        public Vector2 SafePosition { get; private set; }
        

        void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");

            float ax = Mathf.Abs(_movementInput.x);
            float ay = Mathf.Abs(_movementInput.y);

            if (ax > ay) Facing = _movementInput.x > 0 ? Vector2.right : Vector2.left;
            else if (ay > ax) Facing = _movementInput.y > 0 ? Vector2.up : Vector2.down;

            if (!PlayerState.Instance.IsInBog && PlayerState.Instance.IsSafe) SafePosition = Rb.position;
        }

        void FixedUpdate()
        {
            if (PlayerState.Instance.IsStunned)
            {
                Rb.velocity = Vector2.zero;
                return;
            }

            Vector2 playerVelocity = _speed * _movementInput.normalized;
            
            if (Platform == null)
                Rb.velocity = playerVelocity;
            else Rb.velocity = playerVelocity + Platform.PlatformShift / Time.fixedDeltaTime;
        }

        public void Drown(float drowningTime) => StartCoroutine(DrownRoutine(drowningTime));

        IEnumerator DrownRoutine(float drowningTime)
        {
            float t = 0f;
            while (t < drowningTime)
            {
                t += Time.deltaTime;
                yield return null;
            }
            BackToSafe();
            PlayerState.Instance.SetInBog(false);
        }

        public void BackToSafe() => Rb.position = SafePosition;
    }
}