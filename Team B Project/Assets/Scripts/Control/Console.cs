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
        if (command.ToLower() == "sv_cheats 1") //Enable Cheats
        {
            Debug.Log("Cheats Activated");
            cheats = true;
        }
        else if(command.ToLower() == "sv_cheats 0") //Disable Cheats
        {
            Debug.Log("Cheats Deactivated");
            cheats = false;
        }
        else if(command.ToLower() == "thats so metal") //Gain Metal resources
        {
            if (cheats) {
                Debug.Log("+10000 metal");
                ControlledPlayer.Instance.AddResources(new Resource(10000, Resource.ResourceKind.metal));
            }
            else {
                Debug.Log("Enable Cheats First");
            }
        }
        else if(command.ToLower() == "miniature sun") //Gain Fuel resources
        {
            if (cheats) {
                Debug.Log("+10000 fuel");
                ControlledPlayer.Instance.AddResources(new Resource(10000, Resource.ResourceKind.fuel));
            }
            else {
                Debug.Log("Enable Cheats First");
            }
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
