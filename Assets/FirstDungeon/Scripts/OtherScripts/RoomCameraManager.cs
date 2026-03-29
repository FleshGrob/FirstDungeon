using Cinemachine;
using FirstDungeon.Scripts.PlayerScripts;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class RoomCameraManager : MonoBehaviour
    {
        CinemachineVirtualCamera _virtualCamera;
        CinemachineConfiner2D _confiner;
        Collider2D _col;
        PlayerMovement _player;
        
        void Awake()
        {
            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            _confiner = _virtualCamera.GetComponent<CinemachineConfiner2D>();
            _col = GetComponent<CompositeCollider2D>();
            _player = FindObjectOfType<PlayerMovement>();
        }

        void Start()
        {
            Vector2 playerPosition = _player.transform.position;
            
            if (_col.OverlapPoint(playerPosition))
                _confiner.m_BoundingShape2D = _col;
            _confiner.InvalidateCache();
        }
        
        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject != _player.gameObject) return;
            if (_confiner.m_BoundingShape2D == _col) return;
            
            _confiner.m_BoundingShape2D = _col;
            _confiner.InvalidateCache();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (_player == null) return;
            if (other.gameObject != _player.gameObject) return;
            if (_col.OverlapPoint(_player.transform.position)) return;

            _confiner.m_BoundingShape2D = null;
        }
    }
}
