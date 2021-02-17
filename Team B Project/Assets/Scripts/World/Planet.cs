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
    public planetTypeEnum buildingType;

    // Player control
    public enum controlEnum { neutral, player1, player2 };
    public controlEnum control;

    // Resources
    public Resource[] resources = new Resource[2];
    [Serializable]
    public struct Resource
    {
        public int amount;
        public enum ResourceKind { metal, fuel };
        public ResourceKind kind;

        public Resource(int amount, ResourceKind kind)
        {
            this.amount = amount;
            this.kind = kind;
        }
    }

    // order of resources in both arrays have to be the same with current implementation
    private int[] curResources;
    private int[] maxResources;

    // Start is called before the first frame update
    void Start()
    {
        curResources = new int[] {};
        maxResources = new int[] {};
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxResources.Length; ++i)
            if (curResources[i] < maxResources[i])
            {

            }
    }

    private void switchControl(controlEnum c)
    {
        control = c;
    }
}