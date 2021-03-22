using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitPurchaseButtonTest : MonoBehaviour
{
    public Ship.shipType ship;
    ControlledPlayer player;
    Image shipImage;
    Text shipText;
    // Start is called before the first frame update
    void Start()
    {
        player = ControlledPlayer.Instance;
        shipImage = transform.Find("Image").GetComponent<Image>();
        shipText = transform.Find("Text").GetComponent<Text>();
        var prefab = StarShipUtilities.Instance.ShipDictionary[ship];
        var prefabSprite = prefab.gameObject.GetComponentInChildren<SpriteRenderer>()?.sprite;
        shipImage.sprite = prefabSprite ?? shipImage.sprite;
        shipText.text = prefab.gameObject.name;
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
