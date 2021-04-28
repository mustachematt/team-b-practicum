using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuButton : MonoBehaviour
{
    public Ship.shipType ship;

    public string shipName;
    public string shipDesc;
    public Sprite shipImage;
    public int attackPts;
    public int defensePts;
    public int moveSpeedPts;
    public int AtkSpeedPts;
    public int rangePts;
    public int fuelCost;
    public int metalCost;

    public void Awake()
    {
        var ship = StarShipUtilities.Instance.ShipDictionary[this.ship];
        var spriteRenderer = ship.gameObject.GetComponentInChildren<SpriteRenderer>();
        shipImage = spriteRenderer.sprite;
        attackPts = ship is AttackShip ? (ship as AttackShip).attackStrength.Value : 0;
        defensePts = ship.armorStrength.Value;
        moveSpeedPts = ship.maxSpeed.Value;
        AtkSpeedPts = ship is AttackShip ? (ship as AttackShip).attackSpeed.Value : 0;
        rangePts = ship is AttackShip ? (ship as AttackShip).attackRange.Value : 0;
        fuelCost = (int)ship.price.fuel;
        metalCost = (int)ship.price.metal;
    }
}
