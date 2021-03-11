using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ControlledPlayer : IPlayer
{
    public static ControlledPlayer Instance;

    public override  void Awake()
    {
        base.Awake();
        Instance = this;
    }
}
