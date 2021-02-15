using System.Collections;
using System.Collections.Generic;
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
    public enum planetTypeEnum { neutral, gas, mountain, random };
    public controlEnum control;

    // Player control
    public enum controlEnum { neutral, player1, player2 };
    public controlEnum control;

    // Resources
    public enum ResourceKind { metal, fuel };
    public int[] resources;
    public ResourceKind[] resourceKinds;

    private int curMetal;
    public int maxMetal;
    private int curFuel;
    public int maxFuel;


    struct Resource
    {
        public int amount;
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
        curResources = new int[] { curMetal };
        maxResources = new int[] { maxMetal };
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxResources.Length; ++i)
            if (curResources[i] < maxResources[i])
            {

            }
    }

    void switchControl(controlEnum c)
    {
        control = c;
    }
}