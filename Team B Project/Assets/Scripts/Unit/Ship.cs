using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Ship : MonoBehaviour
{
    public enum shipType { Attack, Transport };

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

    [Header("Ship Properties")]
    public shipType kind;
    public ShipPrice price; 
    public ShipPropertyValue maxSpeed;
    public ShipPropertyValue armorStrength;     // Max health

    private ShipPropertyValue health;            // Current health    
    protected IPlayer owner = null;
    protected NavMeshAgent navAgent;
    protected bool isPlayer;


    public virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void Start()
    {
        isPlayer = owner is ControlledPlayer;

        /*
        // initialize ship properties
        maxSpeed.Value = _maxSpeed;
        armorStrength.Value = _armorStrength;
        health.Value = _health;
        */
    }

    public virtual void Update() {}

    public void SetOwner(IPlayer owner)
    {
        this.owner = owner;
    }

    // Change UI according to the taken damage, return false if the ship is destoryed
    // The bool value returned signals to the attacking ship that it has been destroyed
    // will make it to where the attacking ship does not try to continue attacking a destroyed ship
    public bool takeDamage(int attack) {
        int currentHealth = health.Value - attack;
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

    // The ship is destoryed. Change animation==>delete gameobject in dictionary==>delete gameobject
    public void DestroyShip() {
        Destroy(gameObject);
    }
}
