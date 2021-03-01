﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PauseControl : MonoBehaviour
{
    public static event Action GameWasPaused;
    public static event Action GameWasResumed;
    private bool _paused = false;
    public void OnPause()
    {
        if (!_paused)
            PauseGame();
        else
            ResumeGame();
    }
    void PauseGame()
    {
        _paused = true;
        Time.timeScale = 0f;
        GameWasPaused?.Invoke();
    }
    void ResumeGame()
    {
        _paused = false;
        Time.timeScale = 1f;
        GameWasResumed?.Invoke();
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