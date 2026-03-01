using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoxGrabPull : MonoBehaviour
{
    public bool IsGrabbing => isGrabbing;

    [Header("Interaction")]
    [SerializeField] LayerMask pushableMask;
    [SerializeField] float interactRadius = 0.25f;

    [Header("Hold")]
    [SerializeField] float maxGrabDistance = 1.0f;

    [Header("Input thresholds")]
    [SerializeField] float pushThreshold = 0.5f;
    [SerializeField] float pullThreshold = 0.5f;
    [SerializeField] float sideReleaseThreshold = 0.3f;

    [Header("Pull wall safety")]
    [SerializeField] LayerMask solidMask; 
    [SerializeField] float castSkin = 0.02f;
    [SerializeField] float pullSlack = 0.05f;

    bool isGrabbing;
    PushableBox grabbedBox;
    Collider2D grabbedCol;

    PlayerMovement movement;
    Collider2D playerCol;

    Vector2 grabDir = Vector2.zero; 

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        playerCol = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(GameKeys.Action))
            return;

        if (isGrabbing)
        {
            ReleaseGrab();
            return;
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRadius, pushableMask);
        if (hit == null)
            return;

        PushableBox box = hit.GetComponent<PushableBox>();
        if (box == null)
            return;

        grabbedBox = box;
        grabbedCol = hit;
        isGrabbing = true;

        Vector2 delta = (Vector2)grabbedBox.transform.position - (Vector2)transform.position;
        grabDir = SnapTo4(delta);
        if (grabDir == Vector2.zero) grabDir = movement.Facing;
    }

    void FixedUpdate()
    {
        if (!isGrabbing)
            return;

        if (grabbedBox == null || grabbedCol == null)
        {
            ReleaseGrab();
            return;
        }

        Vector2 playerPos = (Vector2)transform.position;
        Vector2 boxPos = (Vector2)grabbedBox.transform.position;
        Vector2 delta = boxPos - playerPos;

        Vector2 newDir = SnapTo4(delta);
        if (newDir != Vector2.zero) grabDir = newDir;

        bool touching = playerCol.IsTouching(grabbedCol);
        float axisDist = Vector2.Dot(delta, grabDir);

        if (!touching && axisDist > maxGrabDistance)
        {
            ReleaseGrab();
            return;
        }

        Vector2 inputDir = movement.InputRaw;
        if (inputDir == Vector2.zero)
            return;

        float pushAmount = Vector2.Dot(inputDir, grabDir);

        if (Mathf.Abs(pushAmount) < sideReleaseThreshold)
        {
            ReleaseGrab();
            return;
        }

        if (pushAmount > pushThreshold)
        {
            grabbedBox.TryStep(grabDir);
        }
        else if (pushAmount < -pullThreshold)
        {
            Vector2 stepDir = -grabDir;

            float alongVel = Vector2.Dot(movement.rb.velocity, grabDir);

            if (alongVel < 0f)
            {
                float desiredBackMove = (-alongVel) * Time.fixedDeltaTime;

                RaycastHit2D[] hits = new RaycastHit2D[2];
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(solidMask);
                filter.useTriggers = false;

                int count = playerCol.Cast(stepDir, filter, hits, desiredBackMove + castSkin);

                if (count == 0)
                {
                    if (axisDist >= maxGrabDistance - pullSlack)
                        grabbedBox.TryStep(stepDir);
                }
            }
        }
    }

    void ReleaseGrab()
    {
        isGrabbing = false;
        grabbedBox = null;
        grabbedCol = null;
        grabDir = Vector2.zero;
    }

    Vector2 SnapTo4(Vector2 v)
    {
        float ax = Mathf.Abs(v.x);
        float ay = Mathf.Abs(v.y);

        if (ax > ay) return v.x > 0 ? Vector2.right : Vector2.left;
        if (ay > ax) return v.y > 0 ? Vector2.up : Vector2.down;

        return Vector2.zero;
    }
}
