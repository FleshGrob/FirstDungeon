using UnityEngine;
using UnityEngine.AI;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] Sprite _openedSprite;
        [SerializeField] Sprite _closedSprite;
        
        Collider2D _doorCol;
        SpriteRenderer _sr;
        NavMeshObstacle _navMeshObstacle;


        void Awake()
        {
            _doorCol = GetComponent<Collider2D>();
            _sr = GetComponent<SpriteRenderer>();
            _navMeshObstacle  = GetComponent<NavMeshObstacle>();
        }

        public void OpenDoor()
        {
            _doorCol.enabled = false;
            _navMeshObstacle.enabled = false;
            _sr.sprite = _openedSprite;
        }

        public void CloseDoor()
        {
            _doorCol.enabled = true;
            _navMeshObstacle.enabled = true;
            _sr.sprite = _closedSprite;
        }
    }
}
