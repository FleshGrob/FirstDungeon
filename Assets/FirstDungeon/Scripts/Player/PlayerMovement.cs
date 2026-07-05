using System.Collections;
using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _speed;
        
        Vector2 _movementInput;
        Coroutine _drownRoutine;
        
        public Vector2 MovementInputRaw => _movementInput;
        public Vector2 Facing { get; private set; } = Vector2.down;
        public Rigidbody2D Rb { get; private set; }
        public Vector2 SafePosition { get; private set; }
        public MovingPlatform Platform { get; private set; } 
        

        void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            InputManager.Instance.OnMoveKeyChanged += GetMovementInput;
        }

        void Update()
        {
            float ax = Mathf.Abs(_movementInput.x);
            float ay = Mathf.Abs(_movementInput.y);

            if (ax > ay) Facing = _movementInput.x > 0 ? Vector2.right : Vector2.left;
            else if (ay > ax) Facing = _movementInput.y > 0 ? Vector2.up : Vector2.down;

            if (!Player.Instance.State.IsInBog && Player.Instance.State.IsSafe) SafePosition = Rb.position;
        }

        void FixedUpdate()
        {
            Vector2 playerVelocity = _speed * _movementInput.normalized;
            
            if (Platform == null)
                Rb.linearVelocity = playerVelocity;
            else Rb.linearVelocity = playerVelocity + Platform.PlatformShift / Time.fixedDeltaTime;
        }

        void OnDestroy()
        {
            if (InputManager.Instance != null) 
                InputManager.Instance.OnMoveKeyChanged -= GetMovementInput;
        }
        
        void GetMovementInput(Vector2 input)
        {
            _movementInput = input;
        }

        public void Drown(float drowningTime)
        {
            if (_drownRoutine != null) return;
            _drownRoutine = StartCoroutine(DrownRoutine(drowningTime));
            Player.Instance.State.Stun(drowningTime);
        }

        IEnumerator DrownRoutine(float drowningTime)
        {
            float t = 0f;
            while (t < drowningTime)
            {
                t += Time.deltaTime;
                yield return null;
            }
            BackToSafe();
            Player.Instance.State.SetInBog(false);
            _drownRoutine = null;
        }

        public void BackToSafe()
        {
            Rb.position = SafePosition;
            Debug.Log($"{Rb.position} -> {SafePosition}");
        }

        public void SetPlatform(MovingPlatform platform)
        {
            Platform = platform;
            Player.Instance.State.GetInAir(platform != null);
        }
    }
}