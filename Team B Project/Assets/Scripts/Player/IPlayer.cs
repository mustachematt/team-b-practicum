using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayer : MonoBehaviour
{
    public int Resources { get; private set; } = 0;
    private List<object> _ownedPlanets = new List<object>();
    public IReadOnlyList<object> OwnedPlanets
    {
        get => _ownedPlanets.AsReadOnly();
    }
    public void AddResources(int amount)
    {
        Resources += amount;
    }
    public virtual void Update()
    {

    }
    //Player Actions
    public void SpawnUnit(int unitType)
    {
        //Instantiate Ship Prefab, subtract resources
    }
    public void BuildStructure(object planet, int structure)
    {
        // Add Structure to planet, add planet as owned by player
    }

}
