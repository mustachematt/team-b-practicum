using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class StarShipUtilities : MonoBehaviour
{
    public enum shipType { test1, test2, test3 }
    public static StarShipUtilities Instance;
    public Dictionary<shipType, GameObject> ShipDictionary = new Dictionary<shipType, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        BuildShipDictionary();
    }

    private void BuildShipDictionary()
    {
        foreach(shipType type in Enum.GetValues(typeof(shipType)))
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
