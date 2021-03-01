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
    	Mountain = -metal +gas
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
    public Resource[] resources = new Resource[2];
    [Serializable]
    public class Resource
    {
        public int currAmt; // the current amount of the resource on the planet
        public int maxAmt; // the max amount of the resource on the planet
        public enum ResourceKind { metal, fuel };
        public ResourceKind kind;

        public Resource(int currAmt, ResourceKind kind)
        {
            this.currAmt = currAmt;
            this.maxAmt = currAmt;
            this.kind = kind;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Resource r in resources)
        {
            if (r.currAmt < r.maxAmt)
            {
                // wait certain amount of seconds...
                ++r.currAmt;
            }
        }

    }

    private void switchControl(controlEnum c)
    {
        control = c;
    }

    public void removeResources(int amount)//removes resources from planet equally
    {
        for(int i =0; i<resources.Length; ++i)//iterate through each index in resources
            resources[i].currAmt -= amount/resources.Length;//remove amount/length for each index
    }
}