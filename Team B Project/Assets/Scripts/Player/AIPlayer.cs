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
        if (currentTime >= timeBetweenActions) {
            currentTime = 0f;
            timeBetweenActions ++;
            makeDecision();
        }
    }

    void makeDecision() {
        SpawnUnit(Ship.shipType.Freighter);
        SpawnUnit(Ship.shipType.BasicStarfighter);
        AddResources(new Resource(1000, Resource.ResourceKind.metal));
        AddResources(new Resource(1000, Resource.ResourceKind.fuel));
    }
    
}

