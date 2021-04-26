using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using System.IO;

public class Console : MonoBehaviour
{
    public enum devType { dev_JonStarfighter, };
    public List<Ship> dev_fleet;
    public static bool cheats = false;
    InputField input;
    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponentInChildren<InputField>();

        foreach(devType type in Enum.GetValues(typeof(devType)))
        {
            var shipObject = Resources.Load(Path.Combine("Ships", "Prefabs", type.ToString())) as GameObject; // Assets/Resources/Ships/Prefabs/[Ship Name]
            if (shipObject != null)
                dev_fleet.Add(shipObject.GetComponent<Ship>());
            else
                continue;
        }
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
        else if(command.ToLower() == "unfixed bugs") //spawn the dev fleet
        {
            if (cheats) {
                Debug.Log("unfixed bugs");
                if (Console.cheats == false) return;
                Debug.Log("Konami Achieved");
                
                foreach(Ship ship in dev_fleet) {
                    for (int i = 0; i < 10; i++) {
                        GameObject shipObj = GameObject.Instantiate(ship.gameObject, ControlledPlayer.Instance.playerBase.transform.position, ControlledPlayer.Instance.playerBase.transform.rotation, ControlledPlayer.Instance.transform);
                        shipObj.GetComponent<Ship>().SetOwner(ControlledPlayer.Instance);
                        shipObj.layer = 8; // 8 is the player layer
                    }
                }
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
