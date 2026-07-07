using System;
using System.Collections;
using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] float _invulnerableTime;

        public Action OnStunned;
        
        public bool IsStunned { get; private set; }
        public bool IsInBog { get; private set; }
        public bool IsAlive { get; private set; } = true;
        public bool IsInAir { get; private set; }
        public bool IsSafe { get; private set; } = true;
        public bool IsInvulnerable { get; private set; }
        

        public void SetInBog(bool value) => IsInBog = value;
        public void GetInAir(bool value) => IsInAir = value;
        public void SetSafe(bool value) => IsSafe = value;

        [ContextMenu("SerializedStun")]
        public void SerializedStun()
        {
            Stun(1);
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

        public void Die() => IsAlive = false;
    }
}
