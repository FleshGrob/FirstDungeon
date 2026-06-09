using UnityEngine;
using UnityEngine.SceneManagement;

public class BootScript : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
