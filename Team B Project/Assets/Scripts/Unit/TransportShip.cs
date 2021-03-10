//using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TransportShip : Ship
{
    //public enum ResourceType { M, F };              // Temporary resource type
    public Resource resource;
    public int capacity = 5;
    protected bool returning = false;
    protected GameObject destination;
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
        if(!goHome)
        {
            var Playerplanets = Resources.FindObjectsOfTypeAll<Planet>().Where(x => owner is ControlledPlayer ?
                   x.control == Planet.controlEnum.player1 : x.control == Planet.controlEnum.player2);
            var location = Playerplanets.ElementAt(1);
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

        if(Vector3.Distance(navAgent.destination, gameObject.transform.position) < 1)
        {
            //Destination Reached
            if(!returning)
            {
                Debug.Log("Reached Planet");
                Planet planet = destination.GetComponent<Planet>();
                var acquiredResources = planet.removeResources(new Resource(capacity, resource.kind));
                resource.amount += acquiredResources.amount;
                SetDestination(true);
            }
            else
            {
                Debug.Log("Reached Player");
                owner.AddResources(resource);
                resource.amount = 0;
                SetDestination(false);

            }
        }
    }
    
}
