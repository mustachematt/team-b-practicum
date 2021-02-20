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
    }

    // when trigger resource point, resource point call this function every 1s. ps: Set isTransportation to false;
    void collectResource()
    {
        amount++;
        if(amount >= capacity)
        {
            isTransportation = true;
            target = depositPoint;
        }
    }


    // when trigger deposit point, resource point call this function every 1s. ps: Set isTransportation to false;
    void depositResource()
    {
        amount--;
        if (amount == 0)
        {
            isTransportation = true;
            target = resourcePoint;
        }
    }
}
