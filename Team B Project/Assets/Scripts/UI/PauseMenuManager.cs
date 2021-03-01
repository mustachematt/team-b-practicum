using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public PauseControl pauseControl;


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
}
