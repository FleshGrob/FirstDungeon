using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Chest : MonoBehaviour
    {
        PlayerInventory _playerInventory;
        bool _isOpened;
        
        void Start()
        {
            InputManager.Instance.OnInteractKeyPressed += Open;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerInventory != null) return;
            
            _playerInventory = other.GetComponent<PlayerInventory>();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerInventory>() == null) return;
            
            _playerInventory = null;
        }
        
        void OnDestroy()
        {
            if (InputManager.Instance == null) return;
            InputManager.Instance.OnInteractKeyPressed -= Open;
        }

        void Open()
        {
            if (!Player.Instance.State.CanDo(PlayerState.PlayerAction.Interact)) return;
            
            if (_isOpened) return;
            if (_playerInventory == null) return;
            
            _isOpened = true;
            _playerInventory.AddKey();

            InputManager.Instance.OnInteractKeyPressed -= Open;
            Destroy(gameObject);
        }
    }
}