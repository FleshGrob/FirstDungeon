using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public event Action OnActionKeyPressed;
        public event Action OnShootKeyPressed;
        public event Action<Vector2> OnMoveKeyChanged;
        public event Action OnPauseKeyPressed;
        
        NewControls _controls;
        int _blockCounter;
    

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
            _controls = new NewControls();
            _controls.Enable();
        }

        void Start()
        {
            _controls.GamePlay.Action.performed += PressAction;
            
            _controls.GamePlay.Shoot.performed += PressShoot;
            
            _controls.GamePlay.Move.performed += ChangeMove;
            _controls.GamePlay.Move.canceled += ChangeMove;
            
            _controls.UI.Pause.performed += PressPause;
        }

        void OnDestroy()
        {
            _controls.GamePlay.Action.performed -= PressAction;
            
            _controls.GamePlay.Shoot.performed -= PressShoot;
            
            _controls.GamePlay.Move.performed -= ChangeMove;
            _controls.GamePlay.Move.canceled -= ChangeMove;
            
            _controls.UI.Pause.performed -= PressPause;
        }
        
        void PressAction(InputAction.CallbackContext ctx) => OnActionKeyPressed?.Invoke();
        void PressShoot(InputAction.CallbackContext ctx) => OnShootKeyPressed?.Invoke();
        void ChangeMove(InputAction.CallbackContext ctx) => OnMoveKeyChanged?.Invoke(ctx.ReadValue<Vector2>());
        void PressPause(InputAction.CallbackContext ctx) =>  OnPauseKeyPressed?.Invoke();

        public void BlockGameplay()
        {
            _blockCounter++;
            _controls.GamePlay.Disable();
        }

        public void UnBlockGameplay()
        {
            _blockCounter--;
            if (_blockCounter <= 0) _controls.GamePlay.Enable();
        }
    }
}
