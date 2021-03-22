using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ControlledPlayer : IPlayer
{
    public static ControlledPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }
}
