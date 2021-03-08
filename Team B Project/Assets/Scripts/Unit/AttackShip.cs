using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : Ship
{
    public bool isFiring = false;
    public List<GameObject> targetList;
    public float attackRange = 3;
    public int attackStrength = 3;
    public int attackSpeed = 3;

    private float attackTimer;

    public override void Start()
    {
        base.Start();

        kind = shipType.Attack;

        // Test
        attackRange = 3.0f;
        attackStrength = 3;
        attackSpeed = 3;
        attackTimer = 0;
    }

    public override void Update()
    {
        // Check if target is in range
        float distance = (target.transform.position - m_Obj.transform.position).magnitude;
        if (distance <= attackRange)
            isFiring = true;
        else
            isFiring = false;

        if (isFiring)
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
        if (!isFiring)
        {
            if ((target.transform.position - m_Obj.transform.position).magnitude
                    < (collider.gameObject.transform.position - m_Obj.transform.position).magnitude)
                return;
            target = collider.gameObject;
            isFiring = true;
        }
    }


    void attack() {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed)
        {
            if (!target.GetComponent<Ship>().takeDamage(attackStrength))// Target destoryed
            {/*
                // Remove destoryed targer from enemyList and set new target
                if (isPlayer)
                    owener.GetComponent<IPlayer>().enemyList.Remove(target);
                    if ( owener.GetComponent<IPlayer>().enemyList.Count == 0)
                       isFiring = false;                                                              // All target clear
                    else
                       target = targetList[owener.GetComponent<IPlayer>().enemyList.Count - 1];     // Set target from enemyList
                else
                    owener.GetComponent<AIPlayer>().enemyList.Remove(target);
                    if ( owener.GetComponent<AIPlayer>().enemyList.Count == 0)
                       isFiring = false;                                                              // All target clear
                   else
                       target = targetList[owener.GetComponent<AIPlayer>().enemyList.Count - 1];     // Set target from enemyList
             */
            }
            attackTimer = 0;
        }
    }
}
