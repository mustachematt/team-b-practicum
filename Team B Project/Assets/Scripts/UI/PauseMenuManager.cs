﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{

    // root gameobject of the pause menu used to toggle activation
    public GameObject pauseMenu;
    // variables used to pull functions from other scripts
    public PauseControl pauseControl;


    // GameObjects used for the pause menu
    // slider component for mouse sensitivity
    public Slider mouseSensitivitySlider;
    public Text mouseSensitivityText;
    // slider component for master volume
    public Slider masterVolumeSlider;
    public Text masterVolumeText;
    // slider component for sound volume
    public Slider soundVolumeSlider;
    public Text soundVolumeText;
    // slider component for music volume
    public Slider musicVolumeSlider;
    public Text musicVolumeText;

    public VolumeControl volumeControl;
    public CameraControl cameraControl;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        ShowSliderValues();
    }

    public void ClosePauseMenu()
    {
        pauseControl.ResumeGame();
    }

    public void ReturnMainMenu()
    {
        pauseControl.ResumeGame();  // that way when the player hits play from the main menu, the game isn't still paused
        SceneManager.LoadScene(0);
    }

    public void ShowSliderValues()
    {
        masterVolumeText.GetComponent<Text>().text = Mathf.Round((masterVolumeSlider.value * 200)) + "%";
        musicVolumeText.GetComponent<Text>().text = Mathf.Round((musicVolumeSlider.value * 200)) + "%";
        soundVolumeText.GetComponent<Text>().text = Mathf.Round((soundVolumeSlider.value * 200)) + "%";
        mouseSensitivityText.GetComponent<Text>().text = mouseSensitivitySlider.value + "%";
    }

    public void SensitivitySlider()
    {
        // zoom speed
        cameraControl.speed = mouseSensitivitySlider.value;
        cameraControl.zoomSpeed = mouseSensitivitySlider.value;
    }
}
