﻿using System.Collections;
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


    // 3 is too small for attackRange, use attackRange(3) to calculate points of balance and use (attackRange * sale) to set actrally attack range 
    private float attackScale = 3;
    private int nextTarget;

    public override void Start()
    {
        base.Start();

        kind = shipType.Attack;

        // Test
        attackRange = 3.0f;
        attackStrength = 3;
        attackSpeed = 3;
        attackTimer = 0;
        if (transform.parent.GetComponent<Fleet>().EnemyShips.Contains(target.GetComponent<Ship>()))
            return;
        transform.parent.GetComponent<Fleet>().EnemyShips.Add(target.GetComponent<Ship>());
    }

    public override void Update()
    {
        base.Update();
        // Check if all enemies clear
        if (transform.parent.GetComponent<Fleet>().EnemyShips.Count == 0 && !target)
            return;
        nextTarget = transform.parent.GetComponent<Fleet>().EnemyShips.Count - 1;
        // Check if target is destory
        if (!target)
        {
            if (transform.parent.GetComponent<Fleet>().EnemyShips.Count != 0)
            {
                nextTarget = transform.parent.GetComponent<Fleet>().EnemyShips.Count - 1;
              //  target = transform.parent.GetComponent<Fleet>().EnemyShips[nextTarget];
            }
        }
        if (!target)
            return;
        // Check if target is in range
        float distance = (target.transform.position - gameObject.transform.position).magnitude;
        if (distance <= attackRange)
            attack();
        
    }


    //Detect enemy
    public void OnTriggerEnter(Collider collider)
    {
        Ship ship;
        if (collider.gameObject.TryGetComponent(out ship) == true)
            if (collider.transform.parent != transform.parent)     // If collider is enemy
            {
                // Add collider to enemyList
                if (transform.parent.GetComponent<Fleet>().EnemyShips.Contains(collider.gameObject.GetComponent<Ship>()))
                    return;
                transform.parent.GetComponent<Fleet>().EnemyShips.Add(collider.gameObject.GetComponent<Ship>());

                // Chose the closest enemy
                if (!isFiring)
                {
                    if ((transform.position - target.transform.position).magnitude > (transform.position - collider.transform.position).magnitude)
                        target = collider.gameObject;
                }
            }

        if (collider.gameObject.TryGetComponent(out ship) == false)
            if (collider.transform.parent != transform.parent)     // If collider is enemy
            {
                // Add collider to enemyList
                if (transform.parent.GetComponent<Fleet>().EnemyShips.Contains(collider.gameObject.GetComponent<Ship>()))
                    return;
                transform.parent.GetComponent<Fleet>().EnemyShips.Add(collider.gameObject.GetComponent<Ship>());

                // Chose the closest enemy
                if (!isFiring)
                {
                    if ((transform.position - target.transform.position).magnitude > (transform.position - collider.transform.position).magnitude)
                        target = collider.gameObject;
                }
            }
    }


    void attack()
    {
        isFiring = true;
        attackTimer += Time.deltaTime;
        if (attackTimer >=  attackSpeed)
        {
            if (!target.GetComponent<Ship>().takeDamage(attackStrength))               // Target destoryed, remove destoryed targer from enemyList
                transform.parent.GetComponent<Fleet>().EnemyShips.Remove(target.GetComponent<Ship>());
            attackTimer = 0;
            isFiring = false;
        }
    }
}
