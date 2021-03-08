using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayer : MonoBehaviour
{
    public int Resources { get; private set; } = 0;
    private List<object> _ownedPlanets = new List<object>();
    public GameObject playerBase;

    public IReadOnlyList<object> OwnedPlanets
    {
        get => _ownedPlanets.AsReadOnly();
    }
    public void AddResources(int amount)
    {
        Resources += amount;
    }
    public virtual void Update()
    {

    }
    //Player Actions
    public void SpawnUnit(Ship.shipType unitType/*, GameObject waypoint*/)
    {
        //Instantiate Ship Prefab, subtract resources
        //resources -= shipPrefab.cost;
        GameObject shipPrefab = StarShipUtilities.Instance.ShipDictionary[unitType];
        GameObject ship = GameObject.Instantiate(shipPrefab, playerBase.transform.position, playerBase.transform.rotation);
        //ship.Ship.target = waypoint;
    }

}
