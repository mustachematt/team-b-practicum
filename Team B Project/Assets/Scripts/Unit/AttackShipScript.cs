using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShipScript : StartShipScript
{

    public bool isFire = false;
    public List<GameObject> targetList;
    public float attackRange = 3;
    public int attackStrength = 3;
    public int attackSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        shipKind = shipType.Attack;
        health = armorStrength;

        // Test
        attackRange = 3.0f;
        attackStrength = 3;
        attackSpeed = 3;
}

    // Update is called once per frame
    void Update()
    {
        if (isFire)
            attack();
        else
            flyTo(target.transform.position);
    }

    void attack() {
        aTime += Time.deltaTime;
        if (aTime >= attackSpeed)
        {
            if (!target.GetComponent<StartShipScript>().takeDamage(attackStrength))// Target destoryed
            {
                targetList.Remove(target);
                if (targetList.Count == 0)
                    isFire = false;
                else
                    target = targetList[0];
            }
            aTime = 0;
        }
    }
}
