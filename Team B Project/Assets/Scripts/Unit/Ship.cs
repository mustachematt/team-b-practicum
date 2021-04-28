using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using System.IO;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Ship : MonoBehaviour
{
    public enum shipType { BasicStarfighter, SpartanStarfighter, Freighter, Bomber, FuelFreighter };

    // unsigned to prevent negative prices
    [Serializable]
    public struct ShipPrice
    {
        public uint fuel;
        public uint metal;
    }

    // to restrict the property values between _minVal and _maxVal
    [Serializable]
    public class ShipPropertyValue
    {
        private int _maxVal = 5, _minVal = 1;
        [SerializeField] private int _value;

        public int Value
        {
            get => _value;
            set => _value = correctValue(value);
        }

        private int correctValue(int x)
        {
            if (x < _minVal) return _minVal;
            if (x > _maxVal) return _maxVal;
            else return x;
        }
    }

    [Header("General Debug")]
    public GameObject target;       // Initally hold enemy's base(Attack)/resource point(Transport).
    public Slider healthSlider;     // Health UI
    [SerializeField] private ShipPropertyValue health;  // Current health 

    [Header("Ship Properties")]
    public shipType kind;
    public ShipPrice price; 
    public ShipPropertyValue maxSpeed;
    public ShipPropertyValue armorStrength;     // Max health 

    private Vector3 previousVelocity;
    private Vector3 previousAcceleration;
    private Vector3 currentVelocity;
    private Vector3 currentAcceleration;

    protected IPlayer owner = null;
    protected NavMeshAgent navAgent;
    protected bool isPlayer;


    public virtual void Start() { isPlayer = owner is ControlledPlayer; }
    public virtual void Update() { moveAnimHandler(); }
    public virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        currentVelocity = Vector3.zero;
        currentAcceleration = Vector3.zero;

        SetMaxSpeed(); SetMaxHealth();
    }


    public void SetOwner(IPlayer owner) { this.owner = owner; }
    public void DestroyShip()
    {
        Instantiate(Resources.Load("Explosions/Prefabs/TempExplosion"), transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
    private void SetMaxSpeed() { navAgent.speed = maxSpeed.Value * 2; }
    private void SetMaxHealth() { health.Value = armorStrength.Value; }
    
    
    public bool takeDamage(int attack)
    {
        Debug.Log($"Attacked. Health: {health.Value}");
        int currentHealth = health.Value - attack;
        Debug.Log($"Took {attack} damage. Remaining Health {currentHealth}");
        if (currentHealth <= 0)
        {
            health.Value = 0;
            DestroyShip();
            return false;
        }
        health.Value = currentHealth;
        healthSlider.value = health.Value / armorStrength.Value;
        return true;
    }


    private void moveAnimHandler()
    {
        // update values and get acceleration
        currentVelocity = navAgent.velocity;
        currentAcceleration = (currentVelocity - previousVelocity) / Time.deltaTime;

        // booster animation if positive acceleration, still animation otherwise
        if (currentAcceleration.magnitude > previousAcceleration.magnitude) GetComponentInChildren<Animator>()?.SetBool("hasPosAcceleration", true);
        else GetComponentInChildren<Animator>()?.SetBool("hasPosAcceleration", false);

        // for the next call
        previousVelocity = currentVelocity;
        previousAcceleration = currentAcceleration;
    }
}
