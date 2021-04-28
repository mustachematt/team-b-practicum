using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AttackShip : Ship
{
    public ShipPropertyValue attackRange;
    public ShipPropertyValue attackStrength;
    public ShipPropertyValue attackSpeed;
    float setDestinationRange = 8f;//distance from a planet this ship must be to gain control of it and move to next target
    string[] planetList = { "Player2MainPlanet", "Player2Planet1", "Player2Planet3", "Player2Planet2", "NeutralTop2", "NeutralTopMain", "NeutralTop3", "NeutralTop1", "NeutralCenter1", "NeutralPlanet3", "NeutralPlanet2", "NeutralCenterMain", "NeutralPlanet6", "NeutralPlanet4", "NeutralPlanet5", "NeutralBottom1", "NeutralBottomMain", "NeutralBottom3", "NeutralBottom2", "Player1Planet2", "Player1Planet3", "Player1Planet1", "Player1MainPlanet" };
    //the string array above lists the order planets are to be visited, for player ships -> start at Length-1 and move towords 0, for AI ships -> start at 0 and move towords Length-1
    int planetTargetIndex;//used to track which planet this ship is targeting

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
        UpdateDestinationPlanetIndex();

    }

    void UpdateDestinationPlanetIndex()
    {
        if (this.isPlayer)
        {
            var lastNotOwnedPlanet = planetList.FirstOrDefault(x => GameObject.Find(x).GetComponent<Planet>().control == Planet.controlEnum.player1);
            if (lastNotOwnedPlanet == null)
                planetTargetIndex = planetList.Length - 1;
            else
                planetTargetIndex = Mathf.Clamp(planetList.ToList().IndexOf(lastNotOwnedPlanet) - 1, 0, planetList.Length);
        }
        else if (!this.isPlayer)
        {
            var lastNotOwnedPlanet = planetList.LastOrDefault(x => GameObject.Find(x).GetComponent<Planet>().control == Planet.controlEnum.player2);
            if (lastNotOwnedPlanet == null)
                planetTargetIndex = planetList.Length - 1;
            else
                planetTargetIndex = Mathf.Clamp(planetList.ToList().IndexOf(lastNotOwnedPlanet) + 1, 0, planetList.Length);
        }
    }
    public void SetDestinationToEnemyBase()
    {
        UpdateDestinationPlanetIndex();
        if (this.isPlayer)
        {
            if (Mathf.Abs(this.transform.position.x - GameObject.Find(planetList[planetTargetIndex]).transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find(planetList[planetTargetIndex]).transform.position.z) < setDestinationRange)//checks to see if this ship is close enough to its target planet
            {
                GameObject.Find(planetList[planetTargetIndex]).GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);//gains control of the previous planet
                if (planetTargetIndex > 0)
                {
                    navAgent.SetDestination(GameObject.Find(planetList[planetTargetIndex - 1]).transform.position);//sets destination to next planet
                    --planetTargetIndex;
                }
            }
            else
                navAgent.SetDestination(GameObject.Find(planetList[planetTargetIndex]).transform.position);//sets destination to current planet
        }
        if (!this.isPlayer)
        {
            if (Mathf.Abs(this.transform.position.x - GameObject.Find(planetList[planetTargetIndex]).transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find(planetList[planetTargetIndex]).transform.position.z) < setDestinationRange)//checks to see if this ship is close enough to its target planet
            {
                GameObject.Find(planetList[planetTargetIndex]).GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);//gains control of the previous planet
                if (planetTargetIndex < planetList.Length-1)
                {
                    navAgent.SetDestination(GameObject.Find(planetList[planetTargetIndex + 1]).transform.position);//sets destination to next planet
                    ++planetTargetIndex;
                }
            }
            else
                navAgent.SetDestination(GameObject.Find(planetList[planetTargetIndex]).transform.position);//sets destination to current planet
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
