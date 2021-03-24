using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Console : MonoBehaviour
{
    public static bool cheats = false;
    InputField input;
    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponentInChildren<InputField>();
    }
    public void Submit()
    {
        string command = input.text;
        input.text = "";
        input.caretPosition = 0;
      //  Debug.Log("Command: " + command);
        CheckCommand(command);
        gameObject.SetActive(false);
    }
    void CheckCommand(string command)
    {
        if (command.ToLower() == "sv_cheats 1")
        {
            Debug.Log("Cheats Activated");
            cheats = true;
        }
        else if(command.ToLower() == "sv_cheats 0")
        {
            Debug.Log("Cheats Deactivated");
            cheats = false;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
