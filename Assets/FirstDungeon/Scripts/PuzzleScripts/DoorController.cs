using UnityEngine;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] Sprite _openedSprite;
        [SerializeField] Sprite _closedSprite;
        
        Collider2D _doorCol;
        SpriteRenderer _sr;


        void Awake()
        {
            _doorCol = GetComponent<Collider2D>();
            _sr = GetComponent<SpriteRenderer>();
        }

        public void OpenDoor()
        {
            _doorCol.enabled = false;
            _sr.sprite = _openedSprite;
        }

        public void CloseDoor()
        {
            _doorCol.enabled = true;
            _sr.sprite = _closedSprite;
        }
    }
}
