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
<<<<<<< HEAD
    public void AddResources(Resource resourceToAdd)
=======
    public void AddResources(Resource amount)
>>>>>>> 0730abfb9a5009e581ab2bf72881e8c8acba4be6
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
    public void SpawnUnit(StarShipUtilities.shipType unitType)
    {
        //Instantiate Ship Prefab, subtract resources
        GameObject shipPrefab = StarShipUtilities.Instance.ShipDictionary[unitType];
        Resources[Resource.ResourceKind.metal].amount -= shipPrefab.GetComponent<StartShipScript>().price;
        GameObject ship = GameObject.Instantiate(shipPrefab, playerBase.transform.position, playerBase.transform.rotation);
        ship.GetComponent<StartShipScript>().target = waypoint;
    }

}
