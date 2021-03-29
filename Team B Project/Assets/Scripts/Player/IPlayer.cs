﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//This is a player class
[RequireComponent(typeof(Fleet))]
public abstract class IPlayer : MonoBehaviour
{
    public Dictionary<Resource.ResourceKind, Resource> Resources;
    private List<object> _ownedPlanets = new List<object>();
    public GameObject playerBase;
    [System.NonSerialized]
    public Fleet Fleet;
    protected static Planet[] _planets;
    public abstract List<Planet> OwnedPlanets();

    protected virtual void Start()
    {
        SpawnUnit(Ship.shipType.Transport);
        if (_planets == null)
            _planets = UnityEngine.Resources.FindObjectsOfTypeAll<Planet>();
    }
    public virtual void Awake()
    {
        Resources = new Dictionary<Resource.ResourceKind, Resource>();
        Resources[Resource.ResourceKind.metal] = new Resource(100, Resource.ResourceKind.metal);
        Resources[Resource.ResourceKind.fuel] = new Resource(0, Resource.ResourceKind.fuel);
        Fleet = GetComponent<Fleet>();
    }
    public void AddResources(Resource resourceToAdd)
    {
        Resources[resourceToAdd.kind].amount += resourceToAdd.amount;
    }


    public virtual void Update()
    {

    }

    //Player Actions
    public void SpawnUnit(Ship.shipType unitType/*, GameObject waypoint*/)

    {
        GameObject shipPrefab = StarShipUtilities.Instance.ShipDictionary[unitType].gameObject;
        //Instantiate Ship Prefab, subtract resources
        if(Resources[Resource.ResourceKind.metal].amount >=shipPrefab.GetComponent<Ship>().price.metal) //this needs to be changed to reflect
        {
            Resources[Resource.ResourceKind.metal].amount -= (int)shipPrefab.GetComponent<Ship>().price.metal;
            GameObject ship = GameObject.Instantiate(shipPrefab, playerBase.transform.position, playerBase.transform.rotation, this.transform);
            ship.GetComponent<Ship>().SetOwner(this);
            if (this is ControlledPlayer) {
                ship.layer = 8; // 8 is the player layer
            }
            else {
                ship.layer = 9; // 9 is the AI layer
            }
        }
        else Debug.Log("Not enough resources");
     //   Debug.Log("Enemy Ships: " + Fleet.EnemyShips.Count);
        //ship.Ship.target = waypoint;
        //ship.GetComponent<Ship>().target = waypoint;
    }

}
