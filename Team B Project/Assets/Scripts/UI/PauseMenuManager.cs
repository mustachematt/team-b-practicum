using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{

    // root gameobject of the pause menu used to toggle activation
    public GameObject pauseMenu;
    // used to pull functions from the pausecontrol script
    public PauseControl pauseControl;
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
        //buttonPress.Play();
        pauseControl.ResumeGame();  // that way when the player hits play from the main menu, the game isn't still paused
        SceneManager.LoadScene(0);
    }

    public void ShowSliderValues()
    {
        masterVolumeText.GetComponent<Text>().text = masterVolumeSlider.value + "%";
        musicVolumeText.GetComponent<Text>().text = musicVolumeSlider.value + "%";
        soundVolumeText.GetComponent<Text>().text = soundVolumeSlider.value + "%";
        mouseSensitivityText.GetComponent<Text>().text = mouseSensitivitySlider.value + "%";
    }
    
}
