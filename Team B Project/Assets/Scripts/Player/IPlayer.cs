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

    
    public void AddResources(Resource resourceToAdd)
    {
        Resources[Resource.ResourceKind.metal].amount += resourceToAdd.amount;
    }

    void Start()
    {
        Resources = new Dictionary<Resource.ResourceKind, Resource>();
        Resources[Resource.ResourceKind.metal] = new Resource(0, Resource.ResourceKind.metal);
        Resources[Resource.ResourceKind.fuel] = new Resource(0, Resource.ResourceKind.fuel);
    }

    public virtual void Update()
    {

    }

    //Player Actions
    public void SpawnUnit(Ship.shipType unitType/*, GameObject waypoint*/)

    {
        //Instantiate Ship Prefab, subtract resources
        GameObject shipPrefab = StarShipUtilities.Instance.ShipDictionary[unitType].gameObject;
        Resources[Resource.ResourceKind.metal].amount -= shipPrefab.GetComponent<Ship>().price;
        GameObject ship = GameObject.Instantiate(shipPrefab, playerBase.transform.position, playerBase.transform.rotation);
        //ship.Ship.target = waypoint;
        //ship.GetComponent<StartShipScript>().target = waypoint;
    }

}
