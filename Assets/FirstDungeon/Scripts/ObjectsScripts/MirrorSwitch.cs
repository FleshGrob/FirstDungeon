using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class MirrorSwitch : MonoBehaviour
    {
        [SerializeField] Mirror _mirror;
        bool _isPlayerInRange;

        void Start()
        {
            InputManager.Instance.OnActionKeyPressed += Rotate;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerMovement>() != null)
                _isPlayerInRange = true;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerMovement>() != null)
                _isPlayerInRange = false;
        }
        
        void OnDestroy()
        {
            if (InputManager.Instance != null) 
                InputManager.Instance.OnActionKeyPressed -= Rotate;
        }

        void Rotate()
        {
            if (_isPlayerInRange)
                _mirror.transform.Rotate(0, 0, 45);
        }
    }
}
