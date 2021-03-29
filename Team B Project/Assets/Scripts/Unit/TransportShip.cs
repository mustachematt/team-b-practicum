//using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TransportShip : Ship
{
    public ShipPropertyValue capacity;

    [Header("Transport Debug")]
    public Resource resource;

    protected GameObject destination;
    protected bool returning = false;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        kind = shipType.Transport;
        resource = new Resource(0, Resource.ResourceKind.metal);
        SetDestination();
    }

    private void SetDestination(bool goHome = false)
    {
        returning = goHome;
        if (!goHome)
        {
            var Playerplanets = owner.OwnedPlanets();
            var viablePlanets = Playerplanets.Where(x => x.resources.Any(y => y.kind == resource.kind)).ToList();
            viablePlanets.Sort(delegate (Planet x, Planet y)
            {
                return y.PlanetResourcesAsDictionary[resource.kind].amount.CompareTo(x.PlanetResourcesAsDictionary[resource.kind].amount);
                  
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
    // Update is called once per frame
    public void LateUpdate()
    {

        if (Vector3.Distance(navAgent.destination, gameObject.transform.position) < 3)
        {
            //Destination Reached
            if (!returning)
            {
              //  Debug.Log("Reached Planet");
                Planet planet = destination.GetComponent<Planet>();
                var acquiredResources = planet.removeResources(new Resource(capacity.Value, resource.kind));
                resource.amount += acquiredResources.amount;
                SetDestination(true);
            }
            else
            {
             //   Debug.Log("Reached Player");
                owner.AddResources(resource);
                resource.amount = 0;
                SetDestination(false);

            }
        }
    }

}
