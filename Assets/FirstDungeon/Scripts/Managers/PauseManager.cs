using System;
using UnityEngine;

namespace FirstDungeon.Scripts.Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static PauseManager Instance { get; private set; }
        
        bool IsPaused;
        public event Action OnPause;
        public event Action OnResume;


        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            InputManager.Instance.OnPauseKeyPressed += ChangePause;
        }

        void OnDestroy()
        {
            if (InputManager.Instance != null) 
                InputManager.Instance.OnPauseKeyPressed -= ChangePause;
        }

        void ChangePause()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                OnPause?.Invoke();
                InputManager.Instance.BlockGameplay();
                Time.timeScale = 0;
            }
            
            else
            {
                IsPaused = false;
                OnResume?.Invoke();
                InputManager.Instance.UnBlockGameplay();
                Time.timeScale = 1;
            }
        }
    }
}
