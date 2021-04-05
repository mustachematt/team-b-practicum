using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
public class ConsoleController : MonoBehaviour
{
    public GameObject console;
    internal static Key[] konami = new Key[] { Key.UpArrow, Key.UpArrow, Key.DownArrow, Key.DownArrow, Key.LeftArrow, Key.RightArrow, Key.LeftArrow, Key.RightArrow, Key.B, Key.A };
    private int konamiIndex = 0;
    bool keyboard = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    void CheckInput()
    {
       // Debug.Log(key);
        if (Keyboard.current[konami[konamiIndex]].wasPressedThisFrame)
            konamiIndex++;
        else
            konamiIndex = 0;
        if (konamiIndex >= konami.Length)
        {
            konamiIndex = 0;
            Konami();
        }
        if (Keyboard.current.backquoteKey.wasPressedThisFrame)
        {
            ActivateConsole();
        }

    }
    public void ActivateConsole()
    {
        console.gameObject.SetActive(true);
        console.GetComponentInChildren<UnityEngine.UI.InputField>().ActivateInputField();
    }
    public void Konami()
    {
        if (Console.cheats == false) return;
        Debug.Log("Konami Achieved");
        var transportCost = (int)StarShipUtilities.Instance.ShipDictionary[Ship.shipType.Freighter].price.metal;
        ControlledPlayer.Instance.AddResources(new Resource(transportCost * 100, Resource.ResourceKind.metal));
        for (int i = 0; i < 100; i++)
            ControlledPlayer.Instance.SpawnUnit(Ship.shipType.Freighter);

    }
    // Update is called once per frame
    void Update()
    {
     //   foreach(Key key in Enum.GetValues(typeof(Key)))
     //   {
     //       if (key == Key.None || key == Key.IMESelected || key == Key.PrintScreen) continue;
     //       if (Keyboard.current[key].wasPressedThisFrame)
     //           CheckInput();
     //   }
        if (Keyboard.current.anyKey.wasPressedThisFrame)
            if(!Keyboard.current.printScreenKey.wasPressedThisFrame)
                CheckInput();

    }
}
