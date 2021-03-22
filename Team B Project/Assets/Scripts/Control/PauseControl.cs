using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PauseControl : MonoBehaviour
{
    public static event Action GameWasPaused;
    public static event Action GameWasResumed;
    private bool _paused = false;
    public GameObject pauseMenu;

    public void OnPause()
    {
        if (!_paused)
            PauseGame();
        else
            ResumeGame();
    }
    public void PauseGame()
    {
        _paused = true;
        Time.timeScale = 0f;
        GameWasPaused?.Invoke();
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        _paused = false;
        Time.timeScale = 1f;
        GameWasResumed?.Invoke();
        pauseMenu.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
