using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a player class
public abstract class IPlayer : MonoBehaviour
{
    public Dictionary<Resource.ResourceKind, Resource> Resources;
    private List<object> _ownedPlanets = new List<object>();
    public GameObject playerBase;
    public GameObject waypoint;

    public IReadOnlyList<object> OwnedPlanets
    {
        get => _ownedPlanets.AsReadOnly();
    }
    public void AddResources(Resource amount)
    {
        Resources[amount.kind].currAmt += amount.currAmt;
    }

    void Start()
    {
        Resources = new Dictionary<Planet.Resource.ResourceKind, Planet.Resource>();
        Resources[Planet.Resource.ResourceKind.metal] = new Planet.Resource(0, Planet.Resource.ResourceKind.metal);
        Resources[Planet.Resource.ResourceKind.fuel] = new Planet.Resource(0, Planet.Resource.ResourceKind.fuel);
    }

    public virtual void Update()
    {

    }

    //Player Actions
    public void SpawnUnit(StarShipUtilities.shipType unitType)
    {
        //Instantiate Ship Prefab, subtract resources
        GameObject shipPrefab = StarShipUtilities.Instance.ShipDictionary[unitType];
        Resources[Planet.Resource.ResourceKind.metal].currAmt -= shipPrefab.GetComponent<StartShipScript>().price;
        GameObject ship = GameObject.Instantiate(shipPrefab, playerBase.transform.position, playerBase.transform.rotation);
        ship.GetComponent<StartShipScript>().target = waypoint;
    }

}
