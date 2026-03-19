using System;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public event Action OnActionKeyPressed;
        public event Action OnShootKeyPressed;
    

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        void Update()
        {
            if (PlayerState.Instance.IsStunned)
                return;
            if (Input.GetKeyDown(GameKeys.Action))
                OnActionKeyPressed?.Invoke();
            if (Input.GetKeyDown(GameKeys.Shoot))
                OnShootKeyPressed?.Invoke();
        }
    }
}
