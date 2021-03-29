using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Planet : MonoBehaviour
{
    /* 
     * HOW PLANETS WORK:
     * Each planet holds a state of which player has control of it,
     * stored in an enum
     * 
     * A planet's resources aren't finite! If a planet's current (cur) value
     * of a given resource is LESS than its maximum (max) value, after a few
     * seconds, the planet will gain 1 more of that resource
     * 
     * Multiple ships can pull the same resource from the same planet at the
     * same time
     */

    //Planet Types
    /*
    	Neutral = even gas/metal
    	Gas = +gas -metal
    	Mountain = +metal -gas
    */

    // Planet type
    public enum planetTypeEnum { neutral, gas, mountain, random };
    public planetTypeEnum planetType;

    // Building type
    public enum buildingTypeEnum { neutral, mine, factory };
    public buildingTypeEnum buildingType;

    // Player control
    public enum controlEnum { neutral, player1, player2 };
    public controlEnum control;

    // Resources
    public PlanetResource[] resources = new PlanetResource[2];

    bool replenishing = false;

    [Serializable]
    public class PlanetResource : Resource
    {
        public int maxAmt; // Max amount of the resource the planet can have
        public PlanetResource(ResourceKind kind, int startingAmount, int maxAmount) : base(startingAmount, kind)
        {
            this.maxAmt = maxAmount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!replenishing)
            StartCoroutine(ReplenishResources());
    }

    private void SwitchControl(controlEnum c)
    {
        control = c;
    }

    public void removeResources(Resource resourceToWithdraw)//removes resources from planet equally
    {
        for(int i =0; i<resources.Length; ++i)//iterate through each index in resources
            resources[i].amount -= resourceToWithdraw.amount / resources.Length;//remove amount/length for each index
    }

    IEnumerator ReplenishResources()
    {
        replenishing = true;
        foreach (PlanetResource r in resources)
            if (r.amount < r.maxAmt)
                r.amount++;

        yield return new WaitForSeconds(15);
        replenishing = false;
    }
}