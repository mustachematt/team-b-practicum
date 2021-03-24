using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class ControlledPlayer : IPlayer
{
    public static ControlledPlayer Instance;
    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    public override List<Planet> OwnedPlanets()
    {
        return _planets.Where(x => x.control == Planet.controlEnum.player1).ToList();
    }
    protected override void Start()
    {
        base.Start();
    }



}
