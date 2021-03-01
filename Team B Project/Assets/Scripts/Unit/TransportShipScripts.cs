//using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : StartShipScript
{
    public enum ResourceType { M, F };              // Temporary resource type
    public ResourceType resource = ResourceType.M;
    public int capacity = 5;
    int amount;
    public bool isTransportation = true;            // Transport state call flyTo, or wait  for collect/deposit
    public GameObject depositPoint;
    public GameObject resourcePoint;
    float resourceRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        shipKind = shipType.Transport;
        health = armorStrength;
    }

    // Update is called once per frame
    void Update()
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
        int toWithdraw = amount;//save old amount
        amount++;
        toWithdraw =amount- toWithdraw;//find difference between amounts and remove corresponding amount from planet
        target.GetComponent<Planet>().removeResources(toWithdraw);
        if (amount >= capacity)
        {
            isTransportation = true;
            target = depositPoint;
        }
    }


    // when trigger deposit point, resource point call this function every 1s. ps: Set isTransportation to false;
    void depositResource()
    {
        int toDeposit = amount;//save old amount
        amount--;
        toDeposit = toDeposit - amount;//find difference between amounts and add corresponding amount to player
     //   if (gameObject.CompareTag("testShip"))//find unit's owner
     //       GameObject.FindGameObjectWithTag("Player").GetComponent<ControlledPlayer>().AddResources(toDeposit);
     //   else if (gameObject.CompareTag("testShipEnemy"))
     //       GameObject.FindGameObjectWithTag("AIPlayer").GetComponent<AIPlayer>().AddResources(toDeposit);
        if (amount == 0)
        {
            isTransportation = true;
            target = resourcePoint;
        }
    }
}
