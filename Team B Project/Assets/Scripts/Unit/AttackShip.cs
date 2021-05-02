using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : Ship
{
    public ShipPropertyValue attackRange;
    public ShipPropertyValue attackStrength;
    public ShipPropertyValue attackSpeed;
    float setDestinationRange = 8f;

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
            if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1MainPlanet").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1MainPlanet").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player1Planet1").transform.position);
                GameObject.Find("Player1MainPlanet").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1Planet1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1Planet1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player1Planet3").transform.position);
                GameObject.Find("Player1Planet1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1Planet3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1Planet3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player1Planet2").transform.position);
                GameObject.Find("Player1Planet3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1Planet2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1Planet2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottom2").transform.position);
                GameObject.Find("Player1Planet2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottom2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottom2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottom3").transform.position);
                GameObject.Find("NeutralBottom2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottom3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottom3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottomMain").transform.position);
                GameObject.Find("NeutralBottom3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottomMain").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottomMain").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottom1").transform.position);
                GameObject.Find("NeutralBottomMain").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottom1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottom1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet5").transform.position);
                GameObject.Find("NeutralBottom1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet5").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet5").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet4").transform.position);
                GameObject.Find("NeutralPlanet5").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet4").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet4").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet6").transform.position);
                GameObject.Find("NeutralPlanet4").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet6").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet6").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralCenterMain").transform.position);
                GameObject.Find("NeutralPlanet6").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralCenterMain").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralCenterMain").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet2").transform.position);
                GameObject.Find("NeutralCenterMain").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet3").transform.position);
                GameObject.Find("NeutralPlanet2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralCenter1").transform.position);
                GameObject.Find("NeutralPlanet3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralCenter1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralCenter1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTop1").transform.position);
                GameObject.Find("NeutralCenter1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTop1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTop1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTop3").transform.position);
                GameObject.Find("NeutralTop1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTop3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTop3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTopMain").transform.position);
                GameObject.Find("NeutralTop3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTopMain").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTopMain").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTop2").transform.position);
                GameObject.Find("NeutralTopMain").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTop2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTop2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player2Planet2").transform.position);
                GameObject.Find("NeutralTop2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2Planet2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2Planet2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player2Planet3").transform.position);
                GameObject.Find("Player2Planet2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2Planet3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2Planet3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player2Planet1").transform.position);
                GameObject.Find("Player2Planet3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2Planet1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2Planet1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player2MainPlanet").transform.position);
                GameObject.Find("Player2Planet1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2MainPlanet").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2MainPlanet").transform.position.z) < setDestinationRange)
            {
                GameObject.Find("Player2MainPlanet").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player1);
                //call win game?
            }
        }
        if (!this.isPlayer)
        {
            if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2MainPlanet").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2MainPlanet").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player2Planet1").transform.position);
                GameObject.Find("Player2MainPlanet").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2Planet1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2Planet1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player2Planet3").transform.position);
                GameObject.Find("Player2Planet1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2Planet3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2Planet3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player2Planet2").transform.position);
                GameObject.Find("Player2Planet3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player2Planet2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player2Planet2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTop2").transform.position);
                GameObject.Find("Player2Planet2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTop2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTop2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTopMain").transform.position);
                GameObject.Find("NeutralTop2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTopMain").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTopMain").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTop3").transform.position);
                GameObject.Find("NeutralTopMain").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTop3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTop3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralTop1").transform.position);
                GameObject.Find("NeutralTop3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralTop1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralTop1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralCenter1").transform.position);
                GameObject.Find("NeutralTop1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralCenter1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralCenter1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet3").transform.position);
                GameObject.Find("NeutralCenter1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet2").transform.position);
                GameObject.Find("NeutralPlanet3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralCenterMain").transform.position);
                GameObject.Find("NeutralPlanet2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralCenterMain").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralCenterMain").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet6").transform.position);
                GameObject.Find("NeutralCenterMain").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet6").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet6").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet4").transform.position);
                GameObject.Find("NeutralPlanet6").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet4").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet4").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralPlanet5").transform.position);
                GameObject.Find("NeutralPlanet4").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralPlanet5").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralPlanet5").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottom1").transform.position);
                GameObject.Find("NeutralPlanet5").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottom1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottom1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottomMain").transform.position);
                GameObject.Find("NeutralBottom1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottomMain").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottomMain").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottom3").transform.position);
                GameObject.Find("NeutralBottomMain").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottom3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottom3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("NeutralBottom2").transform.position);
                GameObject.Find("NeutralBottom3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("NeutralBottom2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("NeutralBottom2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player1Planet2").transform.position);
                GameObject.Find("NeutralBottom2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1Planet2").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1Planet2").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player1Planet3").transform.position);
                GameObject.Find("Player1Planet2").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1Planet3").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1Planet3").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player1Planet1").transform.position);
                GameObject.Find("Player1Planet3").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1Planet1").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1Planet1").transform.position.z) < setDestinationRange)
            {
                navAgent.SetDestination(GameObject.Find("Player1MainPlanet").transform.position);
                GameObject.Find("Player1Planet1").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
            }
            else if (Mathf.Abs(this.transform.position.x - GameObject.Find("Player1MainPlanet").transform.position.x) < setDestinationRange && Mathf.Abs(this.transform.position.z - GameObject.Find("Player1MainPlanet").transform.position.z) < setDestinationRange)
            {
                GameObject.Find("Player1MainPlanet").GetComponent<Planet>().SwitchControl(Planet.controlEnum.player2);
                //call lose game?
            }
        }
    }
    public void SetDestinationToTargetShip()
    {
        navAgent.SetDestination(target.transform.position);
    }
    public override void Update()
    {
        base.Update();
        if (!target)
        {
            SetDestinationToEnemyBase();
            return;
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
            shoot();
            GetComponent<AudioSource>().Play(); // shooting sound effect
            target.GetComponent<Ship>().takeDamage(attackStrength.Value);
            attackTimer = 0;
            isFiring = false;
        }
    }


    void shoot()
    {
        string color;
        if (owner is ControlledPlayer) color = "blue";
        else color = "red";

        GameObject laser = (GameObject)Instantiate(
            Resources.Load($"Lasers/{color}Laser"), 
            transform.position, 
            Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0)
        );

        laser.GetComponent<LaserController>().range = GetComponent<SphereCollider>().radius;
        laser.GetComponent<LaserController>().owner = owner;
    }
}
