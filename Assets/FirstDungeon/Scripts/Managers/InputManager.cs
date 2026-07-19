using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstDungeon.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        NewControls _controls;
        int _blockCounter;
        
        public bool IsAltHeld => _controls.GamePlay.Alt.IsPressed();
        public bool IsCastHeld => _controls.GamePlay.Cast.IsPressed();
        
        public event Action<Vector2> OnMoveKeyChanged;
        public event Action OnInteractKeyPressed;
        public event Action OnShootKeyPressed;
        public event Action OnShootKeyCanceled;
        public event Action OnShapeshiftKeyPressed;
        public event Action OnAbilityKeyPressed;
        public event Action OnCastKeyPressed;
        public event Action OnCastKeyCanceled;
        public event Action OnPauseKeyPressed;
    

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
            
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            _controls.GamePlay.Move.performed += ChangeMove;
            _controls.GamePlay.Move.canceled += ChangeMove;
            
            _controls.GamePlay.Interact.performed += PressAction;
            
            _controls.GamePlay.Shoot.started += PressShoot;
            _controls.GamePlay.Shoot.canceled += CancelShoot;
            
            _controls.GamePlay.Shapeshift.performed += PressShapeshift;

            _controls.GamePlay.Ability.performed += PressAbility;
            
            _controls.GamePlay.Cast.started += PressCast;
            _controls.GamePlay.Cast.canceled += CancelCast;
            
            _controls.UI.Pause.performed += PressPause;
        }

        void OnDestroy()
        {
            _controls.GamePlay.Move.performed -= ChangeMove;
            _controls.GamePlay.Move.canceled -= ChangeMove;

            _controls.GamePlay.Interact.performed -= PressAction;
            
            _controls.GamePlay.Shoot.started -= PressShoot;
            _controls.GamePlay.Shoot.canceled -= CancelShoot;
            
            _controls.GamePlay.Shapeshift.performed -= PressShapeshift;
            
            _controls.GamePlay.Ability.performed -= PressAbility; 
            
            _controls.GamePlay.Cast.started -= PressCast;
            _controls.GamePlay.Cast.canceled -= CancelCast;
            
            _controls.UI.Pause.performed -= PressPause;
        }
        
        void ChangeMove(InputAction.CallbackContext ctx) => OnMoveKeyChanged?.Invoke(ctx.ReadValue<Vector2>());
        void PressAction(InputAction.CallbackContext ctx) => OnInteractKeyPressed?.Invoke();
        
        void PressShoot(InputAction.CallbackContext ctx) => OnShootKeyPressed?.Invoke();
        void CancelShoot(InputAction.CallbackContext ctx) => OnShootKeyCanceled?.Invoke();
        
        void PressShapeshift(InputAction.CallbackContext ctx) => OnShapeshiftKeyPressed?.Invoke();
        void PressAbility(InputAction.CallbackContext ctx) => OnAbilityKeyPressed?.Invoke();
        
        void PressCast(InputAction.CallbackContext ctx) => OnCastKeyPressed?.Invoke();
        void CancelCast(InputAction.CallbackContext ctx) => OnCastKeyCanceled?.Invoke();
        
        void PressPause(InputAction.CallbackContext ctx) =>  OnPauseKeyPressed?.Invoke();

        public void BlockGameplay()
        {
            _blockCounter++;
            _controls.GamePlay.Disable();
        }

        public void UnBlockGameplay()
        {
            _blockCounter--;
            if (_blockCounter < 0)
            {
                Debug.LogWarning("Gameplay block counter is out of range");
                _blockCounter = 0;
            }
            if (_blockCounter == 0) _controls.GamePlay.Enable();
        }
    }
}
