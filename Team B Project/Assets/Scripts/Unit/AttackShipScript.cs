using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShipScript : StartShipScript
{
    public float attackRange = 3;
    public int attackStrength = 3;
    public int attackSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        shipKind = shipType.Attack;
        health = armorStrength;

        // Test
        attackRange = 3.0f;
        attackStrength = 3;
        attackSpeed = 3;
}

    // Update is called once per frame
    void Update()
    {
        // Check if target is in range
        float distance = (target.transform.position - m_Obj.transform.position).magnitude;
        if (distance <= attackRange)
            isFire = true;
        else
            isFire = false;

        if (isFire)
            attack();
        else
            flyTo(target.transform.position);
    }


    //Detect enemy
    public void OnTriggerEnter(Collider collider)
    {
        if (isPlayer)
            if (collider.gameObject.tag == "SpawnPlayer2")     // If collider is enemy
            {
                // Add collider to enemyList
                //owener.GetComponent<IPlayer>().enemyList.Add(collider.gameObject);
            }

        if (!isPlayer)
            if (collider.gameObject.tag == "SpawnPlayer1")     // If collider is enemy
            {
                // Add collider to enemyList
                //owener.GetComponent<AIPlayer>().enemyList.Add(collider.gameObject);
            }

        // Chane target to the closer one
        if (!isFire)
        {
            if ((target.transform.position - m_Obj.transform.position).magnitude
                    < (collider.gameObject.transform.position - m_Obj.transform.position).magnitude)
                return;
            target = collider.gameObject;
            isFire = true;
        }
    }


    void attack() {
        aTime += Time.deltaTime;
        if (aTime >= attackSpeed)
        {
            if (!target.GetComponent<StartShipScript>().takeDamage(attackStrength))// Target destoryed
            {/*
                // Remove destoryed targer from enemyList and set new target
                if (isPlayer)
                    owener.GetComponent<IPlayer>().enemyList.Remove(target);
                    if ( owener.GetComponent<IPlayer>().enemyList.Count == 0)
                       isFire = false;                                                              // All target clear
                   else
                       target = targetList[owener.GetComponent<IPlayer>().enemyList.Count - 1];     // Set target from enemyList
                else
                    owener.GetComponent<AIPlayer>().enemyList.Remove(target);
                    if ( owener.GetComponent<AIPlayer>().enemyList.Count == 0)
                       isFire = false;                                                              // All target clear
                   else
                       target = targetList[owener.GetComponent<AIPlayer>().enemyList.Count - 1];     // Set target from enemyList
             */
            }
            aTime = 0;
        }
    }
}
