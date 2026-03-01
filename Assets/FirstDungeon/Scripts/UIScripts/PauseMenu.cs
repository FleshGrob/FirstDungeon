using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseRoot; 
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] GameObject buttonsRoot;
    [SerializeField] GameObject controlsRoot;

    bool isPaused;

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseRoot != null)
            pauseRoot.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
            TogglePause();
    }

    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseRoot != null)
            pauseRoot.SetActive(true);
        ShowButtonsOnOpen();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseRoot != null)
            pauseRoot.SetActive(false);
    }

    public void Controls()
    {
        if (buttonsRoot != null) buttonsRoot.SetActive(false);
        if (controlsRoot != null) controlsRoot.SetActive(true);
    }

    public void BackFromControls()
    {
        if (buttonsRoot != null) buttonsRoot.SetActive(true);
        if (controlsRoot != null) controlsRoot.SetActive(false);
    }

    void ShowButtonsOnOpen()
    {
        if (buttonsRoot != null) buttonsRoot.SetActive(true);
        if (controlsRoot != null) controlsRoot.SetActive(false);
    }
}