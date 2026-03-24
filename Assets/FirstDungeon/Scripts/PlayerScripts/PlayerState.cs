using System.Collections;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerState : MonoBehaviour
    {
        public static PlayerState Instance { get; private set; }
        public bool IsStunned { get; private set; }
        public bool IsInBog { get; private set; }
        public bool IsOnPlatform { get; private set; }
        public bool IsAlive { get; private set; } = true;

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
        public void SetOnPlatform(bool value) => IsOnPlatform = value;

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

        public void Die() => IsAlive = false;
    }
}
