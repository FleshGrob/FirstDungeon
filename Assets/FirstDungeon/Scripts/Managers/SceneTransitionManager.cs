using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    
    public enum SceneType { Menu, Dungeon}
    
    public event Action<SceneType> OnSceneLoaded;


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

    public void LoadDungeonScene()
    {
        SceneManager.LoadScene("DungeonScene");
        OnSceneLoaded?.Invoke(SceneType.Dungeon);
    }
    
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
        OnSceneLoaded?.Invoke(SceneType.Menu);
    }
}
