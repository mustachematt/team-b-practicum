using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ControlledPlayer : IPlayer
{
    public static ControlledPlayer Instance;

    protected override void Start()
    {
        base.Start();
        SpawnUnit(Ship.shipType.Transport);
    }

    public override  void Awake()
    {
        base.Awake();
        Instance = this;

    }
}
