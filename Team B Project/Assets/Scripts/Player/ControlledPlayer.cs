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
        Instance = this;
        SpawnUnit(Ship.shipType.Transport);
    }
}
