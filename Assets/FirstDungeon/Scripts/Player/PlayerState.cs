using System;
using System.Collections;
using FirstDungeon.Scripts.Managers;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerState : MonoBehaviour
    {
        public enum PlayerAction
        {
            Move,
            Interact,
            CastStone,
            Shoot,
            Spawn,
            Shapeshift,
            Attack,
            HookShot,
        }
        
        enum Form
        {
            Witch,
            Frog
        }
        
        [SerializeField] float _invulnerableTime;

        Form _currentForm;
        int _airCount;
        
        public event Action OnStunned;
        
        public bool IsActing { get; private set; }
        public bool IsStunned { get; private set; }
        public bool IsRooted { get; private set; }
        public bool IsInvulnerable { get; private set; }
        public bool IsInBog { get; private set; }
        public bool IsInAir { get; private set; }
        public bool IsSafe { get; private set; } = true;
        public bool IsAlive { get; private set; } = true;
        

        public bool CanDo(PlayerAction action)
        {
            if (IsStunned) return false;
            
            if (action == PlayerAction.Move) return !IsRooted;
            
            if (IsActing) return false;

            switch (action)
            {
                case PlayerAction.Interact:
                case PlayerAction.CastStone:
                case PlayerAction.Shapeshift:
                    return true;
                case PlayerAction.Shoot:
                case PlayerAction.Spawn:
                    return _currentForm == Form.Witch;
                case PlayerAction.Attack:
                case PlayerAction.HookShot:
                    return _currentForm == Form.Frog; 
            }
            
            return false;
        }
        
        public void SetActing(bool value) => IsActing = value;
        
        public void SetRooted(bool value) => IsRooted = value;
        
        public void SetSafe(bool value) => IsSafe = value;
        
        public void SetInBog(bool value) => IsInBog = value;
        
        public void ChangeForm()
        {
            _currentForm = _currentForm == Form.Witch ? Form.Frog : Form.Witch;
        }
        
        public void Stun(float time)
        { 
            StartCoroutine(StunRoutine(time)); 
            OnStunned?.Invoke();
        }
        
        IEnumerator StunRoutine(float time)
        {
            IsStunned = true;
            InputManager.Instance.BlockGameplay();
            Player.Instance.Visual.ShowStun();

            float t = 0f;
            while (t < time)
            {
                t += Time.deltaTime;
                yield return null;
            }

            IsStunned = false;
            InputManager.Instance.UnBlockGameplay();
            Player.Instance.Visual.BackToNormal();
        }
        
        public void SetInvulnerable() => StartCoroutine(InvulnerableRoutine(_invulnerableTime));
        
        IEnumerator InvulnerableRoutine(float time)
        {
            IsInvulnerable = true;
            Player.Instance.Visual.ShowInvulnerable();

            float t = 0f;
            while (t < time)
            {
                t += Time.deltaTime;
                yield return null;
            }

            IsInvulnerable = false;
            Player.Instance.Visual.BackToNormal();
        }
        
        public void GetInAir(bool value)
        {
            if (value) _airCount++;
            else _airCount--;
            
            IsInAir = _airCount > 0;
        }

        public void Die() => IsAlive = false;
    }
}
