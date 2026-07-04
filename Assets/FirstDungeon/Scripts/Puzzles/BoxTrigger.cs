using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class BoxTrigger : MonoBehaviour
    {
        [SerializeField] DoorController _door;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PushableBox>() == null) return;
            
            _door.OpenDoor();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PushableBox>() == null) return;
            
            _door.CloseDoor();
        }
    }
}