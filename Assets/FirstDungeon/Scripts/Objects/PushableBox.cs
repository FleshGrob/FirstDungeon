using System.Collections;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class PushableBox : MonoBehaviour
    {
        [Header("Step")]
        [SerializeField] float stepSpeed = 10f;
        [SerializeField] LayerMask obstacleMask;
        [SerializeField] Vector2 checkSize = new Vector2(0.8f, 0.8f);

        [Header("Grid")]
        [SerializeField] Grid grid;

        bool isMoving;
        Vector2 lastDir = Vector2.zero;

        Collider2D col;
        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            if (grid == null)
                grid = FindFirstObjectByType<Grid>();

            if (grid != null)
                rb.position = CellCenter(rb.position);
        }

        Vector2 CellCenter(Vector2 worldPos)
        {
            Vector3Int cell = grid.WorldToCell(worldPos);
            return (Vector2)grid.GetCellCenterWorld(cell);
        }

        Vector3Int DirToCellOffset(Vector2 dir4)
        {
            if (dir4 == Vector2.right) return new Vector3Int(1, 0, 0);
            if (dir4 == Vector2.left) return new Vector3Int(-1, 0, 0);
            if (dir4 == Vector2.up) return new Vector3Int(0, 1, 0);
            if (dir4 == Vector2.down) return new Vector3Int(0, -1, 0);
            return Vector3Int.zero;
        }

        public bool TryStep(Vector2 dir)
        {
            if (isMoving) return false;
            if (grid == null) return false;

            float ax = Mathf.Abs(dir.x);
            float ay = Mathf.Abs(dir.y);

            if (ax > ay)
            {
                dir = new Vector2(Mathf.Sign(dir.x), 0f);
                lastDir = dir;
            }
            else if (ay > ax)
            {
                dir = new Vector2(0f, Mathf.Sign(dir.y));
                lastDir = dir;
            }
            else
            {
                dir = lastDir;
            }

            if (dir == Vector2.zero) return false;

            Vector3Int cell = grid.WorldToCell(rb.position);
            Vector3Int offset = DirToCellOffset(dir);
            if (offset == Vector3Int.zero) return false;

            Vector3Int targetCell = cell + offset;

            Vector2 current = (Vector2)grid.GetCellCenterWorld(cell);
            Vector2 target = (Vector2)grid.GetCellCenterWorld(targetCell);

            Collider2D hit = Physics2D.OverlapBox(target, checkSize, 0f, obstacleMask);
            if (hit != null && hit != col) return false;

            StartCoroutine(StepRoutine(current, target));
            return true;
        }

        IEnumerator StepRoutine(Vector2 start, Vector2 target)
        {
            isMoving = true;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.fixedDeltaTime * stepSpeed;
                rb.MovePosition(Vector2.Lerp(start, target, t));
                yield return new WaitForFixedUpdate();
            }

            rb.MovePosition(target);
            isMoving = false;
        }

    }
}