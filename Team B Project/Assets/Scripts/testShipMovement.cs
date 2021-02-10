﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testShipMovement : MonoBehaviour
{
    GameObject[] targetBag;//bag of potiential targets
    GameObject currentTarget;
    GameObject nullTarget;//empty gameObject to assert target hasn't been destroyed

    public Vector3 currentTargetCordinates;

    string myTargetTag;

    float pursuitRange = 40f;
    float firingRange = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("testShipEnemy"))//assert what this' tag is to find enemy ships
            myTargetTag = "testShip";
        else
            myTargetTag = "testShipEnemy";

        targetBag = GameObject.FindGameObjectsWithTag(myTargetTag);//gather objects of requisite tag

        int index = 0;      //loop index
        while (currentTarget == nullTarget && index != targetBag.Length)//find a new target if need be/ make sure array bounds are honored
        {

            if (targetBag[index] != gameObject && Mathf.Abs(targetBag[index].transform.position.x - transform.position.x) < pursuitRange && Mathf.Abs(targetBag[index].transform.position.z - transform.position.z) < pursuitRange)//if possible target is relatively close assign as new target
                currentTarget = targetBag[index];
            else
                ++index;//else keep searching

        }
        
    }

    public Vector3 targeted() {//method to find targets position
        return gameObject.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        targetBag = GameObject.FindGameObjectsWithTag(myTargetTag);//gather objects of requisite tag
        int index = 0;//loop index

        while (currentTarget == nullTarget && index != targetBag.Length)//find a new target if need be/ make sure array bounds are honored
        {

            if (targetBag[index] != gameObject && Mathf.Abs(targetBag[index].transform.position.x - transform.position.x) < pursuitRange && Mathf.Abs(targetBag[index].transform.position.z - transform.position.z) < pursuitRange)//if possible target is relatively close assign as new target
                currentTarget = targetBag[index];
            else
                ++index;//else keep searching

        }

        currentTargetCordinates = currentTarget.GetComponent<testShipMovement>().targeted();//obtain targets current position

        if (Mathf.Abs(currentTargetCordinates.x - transform.position.x) > firingRange && Mathf.Abs(currentTargetCordinates.z - transform.position.z) > firingRange)//if target is out of firing range move twords it
        {
            GetComponent<NavMeshAgent>().SetDestination(new Vector3(currentTarget.transform.position.x, .25f, currentTarget.transform.position.z));//move tranform
            transform.rotation = Quaternion.LookRotation(gameObject.GetComponent<NavMeshAgent>().velocity.normalized);//face target
        }
        else
            GetComponent<NavMeshAgent>().SetDestination(new Vector3(transform.position.x, .25f, transform.position.z));//if close enough to fire at hold position

    }
}
