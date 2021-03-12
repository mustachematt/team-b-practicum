using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
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
    private Dictionary<Resource.ResourceKind, Resource> _planetResourcesAsDictionary = new Dictionary<Resource.ResourceKind, Resource>();
    public IReadOnlyDictionary<Resource.ResourceKind, Resource> PlanetResourcesAsDictionary
    {
        get => _planetResourcesAsDictionary;
    }
    [Serializable]
    public class PlanetResource : Resource
    {
        public int maxAmt; // Max amount of the resource the planet can have
        public PlanetResource(ResourceKind kind, int startingAmount, int maxAmount) : base(startingAmount, kind)
        {
            this.maxAmt = maxAmount;
        }
    }
    private void Awake()
    {
        foreach (Resource planetResource in resources)
            if(!_planetResourcesAsDictionary.ContainsKey(planetResource.kind))
            _planetResourcesAsDictionary.Add(planetResource.kind, planetResource);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlanetResource r in resources)
        {
            if (r.amount < r.maxAmt)
            {
                // wait certain amount of seconds...
                ++r.amount;
            }
        }
    }

    private void SwitchControl(controlEnum c)
    {
        control = c;
    }

    public Resource removeResources(Resource resourceToWithdraw)//removes resources from planet equally
    {
        Resource removed = new Resource(0, resourceToWithdraw.kind);
        int amountToWithdraw = resourceToWithdraw.amount;
        Resource resourceToExtract = resources.FirstOrDefault(x => x.kind == resourceToWithdraw.kind);
        if(resourceToExtract != null)
        {
            while (resourceToExtract.amount > 0 && amountToWithdraw > 0)
            {
                removed.amount += 1;
                amountToWithdraw -= 1;
            }
        }
        return removed;
    }
}