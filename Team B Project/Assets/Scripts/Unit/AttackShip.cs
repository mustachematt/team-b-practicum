using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : Ship
{
    public ShipPropertyValue attackRange;
    public ShipPropertyValue attackStrength;
    public ShipPropertyValue attackSpeed;

    [Header("Attack Debug")]
    public bool isFiring = false;
    public List<GameObject> targetList;

    // 3 is too small for attackRange, use attackRange(3) to calculate points of balance and use (attackRange * sale) to set actual attack range 
    private float attackScale = 3;
    private float attackTimer;
    private int nextTarget;


    private void SetAttackRange() { GetComponent<SphereCollider>().radius = attackRange.Value * attackScale; }
    private void SetAttackStrength() { }
    private void SetAttackSpeed() { }


    public override void Start()
    {
        base.Start();

        SetAttackRange();
        attackTimer = 0;
    }


    public override void Update()
    {
        base.Update();
        // Check if all enemies clear
        if (transform.parent.GetComponent<Fleet>().EnemyShips.Count == 0 && !target)
            return;
        nextTarget = transform.parent.GetComponent<Fleet>().EnemyShips.Count - 1;
        // Check if target is destroyed
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
        if (distance <= attackRange.Value)
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
                if (transform.parent.GetComponent<Fleet>().EnemyShips.Contains(ship))
                    return;
                transform.parent.GetComponent<Fleet>().EnemyShips.Add(ship);

                // Chose the closest enemy
                if (!isFiring)
                {
                    if ((transform.position - target.transform.position).magnitude > (transform.position - collider.transform.position).magnitude)
                        target = collider.gameObject;
                }
            }

        else
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
        if (attackTimer >=  attackSpeed.Value)
        {
            if (!target.GetComponent<Ship>().takeDamage(attackStrength.Value))               // Target destoryed, remove destoryed targer from enemyList
                transform.parent.GetComponent<Fleet>().EnemyShips.Remove(target.GetComponent<Ship>());
            attackTimer = 0;
            isFiring = false;
        }
    }
    

    private void SetAttackRange()
    {
        GetComponent<SphereCollider>().radius = attackRange.Value * attackScale;
    }
}
