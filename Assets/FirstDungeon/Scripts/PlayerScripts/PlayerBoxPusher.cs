using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerBoxPusher : MonoBehaviour
    {
        [SerializeField] LayerMask pushableMask;
        [SerializeField] PlayerBoxGrabPull grabPull;

        [Header("Push hold (no grab)")]
        [SerializeField] float pushHoldSeconds = 0.33f; 

        [Header("Contact grace")]
        [SerializeField] float contactGraceSeconds = 0.25f; 

        PushableBox pushingBox;
        Vector2 pushingDir;
        float lastContactTime;

        float pushHoldTimer;
        bool firstStepDone;

        PlayerMovement movement;


        void Awake()
        {
            movement = GetComponent<PlayerMovement>();
            if (grabPull == null) grabPull = GetComponent<PlayerBoxGrabPull>();
        }

        Vector2 SnapTo4(Vector2 v)
        {
            float ax = Mathf.Abs(v.x);
            float ay = Mathf.Abs(v.y);

            if (ax > ay) return v.x > 0 ? Vector2.right : Vector2.left;
            if (ay > ax) return v.y > 0 ? Vector2.up : Vector2.down;

            return Vector2.zero;
        }

        void FixedUpdate()
        {
            if (pushingBox == null)
                return;

            if (Time.time - lastContactTime > contactGraceSeconds)
            {
                ResetSession();
                return;
            }

            Vector2 inputRaw = movement.MovementInputRaw;
            if (inputRaw == Vector2.zero)
            {
                ResetSession();
                return;
            }

            Vector2 inputDir4 = SnapTo4(inputRaw);
            if (inputDir4 == Vector2.zero || inputDir4 != pushingDir)
            {
                ResetSession();
                return;
            }

            Vector2 toBox = (Vector2)pushingBox.transform.position - (Vector2)transform.position;
            Vector2 dirToBox = SnapTo4(toBox);
            if (dirToBox == Vector2.zero || dirToBox != pushingDir)
            {
                ResetSession();
                return;
            }

            if (!firstStepDone)
            {
                pushHoldTimer += Time.fixedDeltaTime;

                if (pushHoldTimer >= pushHoldSeconds)
                {
                    bool started = pushingBox.TryStep(pushingDir);
                    if (started)
                        firstStepDone = true; 
                }

                return;
            }

            pushingBox.TryStep(pushingDir);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (grabPull != null && grabPull.IsGrabbing)
                return;

            int otherLayer = collision.gameObject.layer;
            if ((pushableMask.value & (1 << otherLayer)) == 0)
                return;

            Vector2 inputRaw = movement.MovementInputRaw;
            if (inputRaw == Vector2.zero)
                return;

            Vector2 pushDir = SnapTo4(inputRaw);
            if (pushDir == Vector2.zero)
                return;

            Vector2 toOther = (Vector2)collision.transform.position - (Vector2)transform.position;
            Vector2 dirToOther = SnapTo4(toOther);
            if (dirToOther == Vector2.zero)
                return;

            if (pushDir != dirToOther)
                return;

            PushableBox box = collision.collider.GetComponent<PushableBox>();
            if (box == null)
                return;

            lastContactTime = Time.time;

            if (pushingBox != box || pushingDir != pushDir)
            {
                pushingBox = box;
                pushingDir = pushDir;
                pushHoldTimer = 0f;
                firstStepDone = false;
            }
        }

        void ResetSession()
        {
            pushingBox = null;
            pushingDir = Vector2.zero;
            lastContactTime = 0f;

            pushHoldTimer = 0f;
            firstStepDone = false;
        }

    }
}