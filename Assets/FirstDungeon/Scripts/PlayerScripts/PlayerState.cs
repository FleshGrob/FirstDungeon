using System.Collections;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] float _invulnerableTime = 1f;
        
        public static PlayerState Instance { get; private set; }
        public bool IsStunned { get; private set; }
        public bool IsInBog { get; private set; }
        public bool IsAlive { get; private set; } = true;
        public bool IsInAir { get; private set; }
        public bool IsSafe { get; private set; } = true;
        public bool IsInvulnerable  { get; private set; }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void SetInBog(bool value) => IsInBog = value;
        public void GetInAir(bool value) => IsInAir = value;
        public void SetSafe(bool value) => IsSafe = value;

        public void Stun(float time) => StartCoroutine(StunRoutine(time));

        IEnumerator StunRoutine(float time)
        {
            IsStunned = true;

            float t = 0f;
            while (t < time)
            {
                t += Time.deltaTime;
                yield return null;
            }

            IsStunned = false;
        }
        
        public void SetInvulnerable() => StartCoroutine(InvulnerableRoutine(_invulnerableTime));
        IEnumerator InvulnerableRoutine(float time)
        {
            IsInvulnerable = true;

            float t = 0f;
            while (t < time)
            {
                t += Time.deltaTime;
                yield return null;
            }

            IsInvulnerable = false;
        }

        public void Die() => IsAlive = false;
    }
}
