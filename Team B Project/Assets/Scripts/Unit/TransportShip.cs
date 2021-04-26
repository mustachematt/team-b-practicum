//using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TransportShip : Ship
{
    [Header("Transport Debug")]
    public Resource resource;
    public ShipPropertyValue capacity;

    public GameObject destination { get; private set; }
    protected bool returning = false;

    private int cap;


    public override void Start()
    {
        base.Start();
        resource = new Resource(0, Resource.ResourceKind.metal);
        SetDestination(); SetCap();
    }
    private float planetDistance(Planet planet)
    {
        return Vector3.Distance(planet.gameObject.transform.position, owner.playerBase.gameObject.transform.position);
    }

    public void LateUpdate()
    {
        if (Vector3.Distance(navAgent.destination, gameObject.transform.position) < 3)
        {
            //Destination Reached
            if (!returning)
            {
                Planet planet = destination.GetComponent<Planet>();
                var acquiredResources = planet.removeResources(new Resource(cap, resource.kind));
                resource.amount += acquiredResources.amount;
                SetDestination(true);
            }
            else
            {
                owner.AddResources(resource);
                resource.amount = 0;
                SetDestination(false);

            }
        }
    }


    private void SetCap() { cap = capacity.Value * 100; }


    public void SetDestination(bool goHome = false)
    {
        returning = goHome;
        if (!goHome)
        {
            var Playerplanets = owner.OwnedPlanets();
            var viablePlanets = Playerplanets.Where(x => x.resources.Any(y => y.kind == resource.kind)).ToList();
         //   var idealPlanets = viablePlanets.Where(x => x.resources.Any(y => y.kind == resource.kind && y.amount >= cap));
            viablePlanets.Sort(delegate (Planet x, Planet y)
            {
                var attractCompare = planetAttractiveness(y).CompareTo(planetAttractiveness(x));
                if (attractCompare == 0)
                    return planetDistance(x).CompareTo(planetDistance(y));
                return attractCompare;
            });

            var location = viablePlanets.First();
            var pos = location.gameObject.transform.position;
            navAgent.SetDestination(new Vector3(pos.x, gameObject.transform.position.y, pos.z));
            destination = location.gameObject;
        }
        else
        {
            var pos = owner.playerBase.transform.position;
            navAgent.SetDestination(new Vector3(pos.x, gameObject.transform.position.y, pos.z));
            destination = owner.gameObject;

        }
    }


    private float planetAttractiveness(Planet planet)
    {
        var transports = owner.Fleet.Ships.Where(x => x is TransportShip);
        var travellingShips = transports.Where(x => (x as TransportShip).destination == planet.gameObject);
        return planet.PlanetResourcesAsDictionary[resource.kind].amount / ((float)cap * travellingShips.Count() + 1);
    }
}
