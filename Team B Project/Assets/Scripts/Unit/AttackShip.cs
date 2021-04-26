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
        attackTimer = attackSpeed.Value;
    }

    public void SetDestinationToEnemyBase()
    {
        if (this.isPlayer)
        {
            navAgent.SetDestination(AIPlayer.Instance.GetHomeLocation());
        }
        if (!this.isPlayer)
        {
            navAgent.SetDestination(ControlledPlayer.Instance.GetHomeLocation());
        }
    }
    public void SetDestinationToTargetShip()
    {
        navAgent.SetDestination(target.transform.position);
    }
    public override void Update()
    {
        base.Update();
        // Check if all enemies clear
     //   if (transform.parent.GetComponent<Fleet>().EnemyShips.Count == 0 && !target)
     //       return;
     //   nextTarget = transform.parent.GetComponent<Fleet>().EnemyShips.Count - 1;
        // Check if target is destroyed
        if (!target)
        {
            SetDestinationToEnemyBase();
            return;
     //       if (transform.parent.GetComponent<Fleet>().EnemyShips.Count != 0)
     //       {
     //           nextTarget = transform.parent.GetComponent<Fleet>().EnemyShips.Count - 1;
     //           //  target = transform.parent.GetComponent<Fleet>().EnemyShips[nextTarget];
      //      }
        }
        attack();
    }


    //Detect enemy
    public void OnTriggerEnter(Collider collider)
    {
        Ship ship;
        if (collider.gameObject.TryGetComponent(out ship) == true)
            if (target == null && owner.Fleet.EnemyShips.Contains(ship))
            {
                target = ship.gameObject;
                SetDestinationToTargetShip();
            }
    }

    
    void attack()
    {
        isFiring = true;
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed.Value)
        {
            Debug.Log("Attacking");
            GetComponent<AudioSource>().Play(); // shooting sound effect
            target.GetComponent<Ship>().takeDamage(attackStrength.Value);
         //   if (!target.GetComponent<Ship>().takeDamage(attackStrength.Value))               // Target destoryed, remove destoryed targer from enemyList
         //       transform.parent.GetComponent<Fleet>().EnemyShips.Remove(target.GetComponent<Ship>());
            attackTimer = 0;
            isFiring = false;
        }
    }
}
