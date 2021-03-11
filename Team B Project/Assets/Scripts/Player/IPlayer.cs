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

    
    public virtual void Awake()
    {
        Resources = new Dictionary<Resource.ResourceKind, Resource>();
        Resources[Resource.ResourceKind.metal] = new Resource(0, Resource.ResourceKind.metal);
        Resources[Resource.ResourceKind.fuel] = new Resource(0, Resource.ResourceKind.fuel);
    }
    public void AddResources(Resource resourceToAdd)
    {
        if(resourceToAdd.kind == Resource.ResourceKind.metal) Resources[Resource.ResourceKind.metal].amount += resourceToAdd.amount;
        if(resourceToAdd.kind == Resource.ResourceKind.fuel) Resources[Resource.ResourceKind.fuel].amount += resourceToAdd.amount;
    }


    public virtual void Update()
    {

    }

    //Player Actions
    public void SpawnUnit(Ship.shipType unitType/*, GameObject waypoint*/)

    {
        GameObject shipPrefab = StarShipUtilities.Instance.ShipDictionary[unitType].gameObject;
        //Instantiate Ship Prefab, subtract resources
        if(Resources[Resource.ResourceKind.metal].amount >=shipPrefab.GetComponent<Ship>().price) //this needs to be changed to reflect
        {
            Resources[Resource.ResourceKind.metal].amount -= shipPrefab.GetComponent<Ship>().price;
            GameObject ship = GameObject.Instantiate(shipPrefab, playerBase.transform.position, playerBase.transform.rotation);
        }
        else Debug.Log("Not enough resources");
        //ship.Ship.target = waypoint;
        //ship.GetComponent<Ship>().target = waypoint;
    }

}
