//using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportShip : Ship
{
    //public enum ResourceType { M, F };              // Temporary resource type
    public Resource resource;
    public int capacity = 5;
    public bool isTransportation = true;            // Transport state call flyTo, or wait  for collect/deposit
    public GameObject depositPoint;
    public GameObject resourcePoint;
    float resourceRange = 1f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        kind = shipType.Transport;
        resource = new Resource(0, Resource.ResourceKind.metal);
        health = armorStrength;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isTransportation)
            flyTo(target.transform.position);

        if (Mathf.Abs(target.transform.position.x - transform.position.x) < resourceRange && Mathf.Abs(target.transform.position.z - transform.position.z) < resourceRange) //if unit is close enough to target, stop moving and take the corresponding action
        {
            isTransportation = false;
            if (target == resourcePoint)//if seeking resources
                collectResource();
            else if (target == depositPoint)//if returning with resources
                depositResource();
        }
    }

    // when trigger resource point, resource point call this function every 1s. ps: Set isTransportation to false;
    void collectResource()
    {
        int amountToWithdraw = resource.amount;//save old amount
        resource.amount++;
        amountToWithdraw = resource.amount- amountToWithdraw;//find difference between amounts and remove corresponding amount from planet
        Resource resourceToWithdraw = new Resource(amountToWithdraw, Resource.ResourceKind.metal);//create resource to remove from planet
        target.GetComponent<Planet>().removeResources(resourceToWithdraw);
        if (resource.amount >= capacity)
        {
            isTransportation = true;
            target = depositPoint;
        }
    }


    // when trigger deposit point, resource point call this function every 1s. ps: Set isTransportation to false;
    void depositResource()
    {
        int amountToDeposit = resource.amount;//save old amount
        resource.amount--;
        amountToDeposit = amountToDeposit - resource.amount;//find difference between amounts and add corresponding amount to player
        Resource resourceToAdd = new Resource(amountToDeposit, Resource.ResourceKind.metal);//create resource to add to player
        if (gameObject.CompareTag("testShip"))//find unit's owner
            GameObject.FindGameObjectWithTag("Player").GetComponent<ControlledPlayer>().AddResources(resourceToAdd);
        else if (gameObject.CompareTag("testShipEnemy"))
            GameObject.FindGameObjectWithTag("AIPlayer").GetComponent<AIPlayer>().AddResources(resourceToAdd);
        if (resource.amount == 0)
        {
            isTransportation = true;
            target = resourcePoint;
        }
    }
}
