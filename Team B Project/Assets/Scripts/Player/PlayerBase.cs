using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public IPlayer Owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        var ship = other.gameObject.GetComponent<Ship>();
        if(ship is AttackShip attacker && Vector3.Distance(other.gameObject.transform.position, transform.position) <= 3)
        {
            if(attacker.owner != Owner)
                if(Owner is ControlledPlayer)
                {
                    ControlledPlayer.Instance.GameEnd(false);
                }
                else
                {
                    ControlledPlayer.Instance.GameEnd(true);
                }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
