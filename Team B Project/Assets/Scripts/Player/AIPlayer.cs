using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class AIPlayer : IPlayer
{
    public float timeBetweenActions = 2.0f;
    public float currentTime = 0.0f;
    public static AIPlayer Instance;
    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    public override List<Planet> OwnedPlanets()
    {
        return _planets.Where(x => x.control == Planet.controlEnum.player2).ToList();
    }

    public override void Update()
    {
        base.Update();

        currentTime += Time.deltaTime;
        if (currentTime >= timeBetweenActions)
        {
            currentTime = 0f;
            //timeBetweenActions++;
            makeDecision();
        }
    }

    void makeDecision()
    {
        int myAttackShips = Fleet.Ships.Where(x => x is AttackShip).Count();
        int myTransportShips = Fleet.Ships.Where(x => x is TransportShip).Count();
        int enemyAttackShips = Fleet.EnemyShips.Where(x => x is AttackShip).Count();
        int enemyTransportShips = Fleet.EnemyShips.Where(x => x is TransportShip).Count();
        uint attackShipPrice = StarShipUtilities.Instance.ShipDictionary[Ship.shipType.BasicStarfighter].price.metal;
        uint transportShipPrice = StarShipUtilities.Instance.ShipDictionary[Ship.shipType.Freighter].price.metal;
        bool canBuyAttack = Resources[Resource.ResourceKind.metal].amount >= attackShipPrice;
        bool canBuyTransport = Resources[Resource.ResourceKind.metal].amount >= transportShipPrice;

        if (myTransportShips > 0 && myAttackShips <= enemyAttackShips && canBuyAttack)
        {
            SpawnUnit(Ship.shipType.BasicStarfighter);
        }
        else if(enemyAttackShips <= myAttackShips && myTransportShips <= enemyTransportShips && canBuyTransport)
        {
            SpawnUnit(Ship.shipType.Freighter);
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, 5);
            if (rand == 0 && canBuyAttack)
                SpawnUnit(Ship.shipType.BasicStarfighter);
            else if(canBuyTransport)
            {
                SpawnUnit(Ship.shipType.Freighter);
            }
        }
        // Debug Ship Spawning
        //  SpawnUnit(Ship.shipType.Freighter);
        //  SpawnUnit(Ship.shipType.BasicStarfighter);
        //  AddResources(new Resource(1000, Resource.ResourceKind.metal));
        //  AddResources(new Resource(1000, Resource.ResourceKind.fuel));
    }

}

