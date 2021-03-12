using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{

    // root gameobject of the pause menu used to toggle activation
    public GameObject pauseMenu;
    // used to pull functions from the pausecontrol script
    public PauseControl pauseControl;
    // slider component for mouse sensitivity
    public Slider mouseSensitivitySlider;
    // slider component for master volume
    public Slider masterVolumeSlider;
    // slider component for sound volume
    public Slider soundVolumeSlider;
    // slider component for music volume
    public Slider musicVolumeSlider;
    // button component for going to main menu
    public Button leaveGame;
    // button component for closing the paue menu
    public Button closeMenu;



    /* WHAT WILL BE ON THE PAUSE MENU

        Volume Control
            Sound Volume
            Music Volume
        
        Controls?
        Camera/Mouse/Control sensitivity
        Quit/Return to main menu

    */



    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePauseMenu()
    {
        SetPauseActive(false);
    }

    void SetPauseActive(bool active)
    {
        pauseMenu.SetActive(active);

        if(pauseMenu.activeSelf)
        {
            //
        }
    }

    void OnMouseSensitivityChanged(float newValue)
    {

    }
}
