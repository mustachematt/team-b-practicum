using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPurchaseButtonTest : MonoBehaviour
{
    public Ship.shipType ship;
    ControlledPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = ControlledPlayer.Instance;
    }

    public void PurchaseShip() 
    {
        player.SpawnUnit(ship);
        Debug.Log(player.Resources[Resource.ResourceKind.metal].amount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void closeBuyMenu()
        //could put an animator here to make it look pretty
    {
        gameObject.SetActive(false);
    }
}
