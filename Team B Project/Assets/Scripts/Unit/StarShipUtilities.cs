using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StarShipUtilities : MonoBehaviour
{
    public static StarShipUtilities Instance;
    public Dictionary<Ship.shipType, GameObject> ShipDictionary = new Dictionary<Ship.shipType, GameObject>();

    void Start()
    {
        Instance = this;
        BuildShipDictionary();
    }

    private void BuildShipDictionary()
    {
        foreach(Ship.shipType type in Enum.GetValues(typeof(Ship.shipType)))
        {
            var shipObject = Resources.Load(Path.Combine("Ships", "Prefabs", type.ToString())) as GameObject;
            if (shipObject != null)
                ShipDictionary[type] = shipObject;
            else
                continue;
            Debug.Log($"Enum Type: {type}");
            var prefabObj = ShipDictionary[type];
            foreach(Transform child in prefabObj.transform)
            {
                Debug.Log($"Child Name: {child.gameObject.name}");
            }
        }
    }
}
