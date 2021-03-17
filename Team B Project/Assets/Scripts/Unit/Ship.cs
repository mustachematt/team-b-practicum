using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Reflection;
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Ship : MonoBehaviour
{
    public enum shipType { Attack, Transport };
    protected IPlayer owner = null;
    public shipType kind;
    public GameObject target;       //Initally hold enemy's base(Attack)/resource point(Transport).
    public Slider healthSlider;         // Health UI
    public float maxSpeed;
    public int armorStrength;           // Max health
    public int price; //this should be an array of 2 ints
    public int health;                  // Current health
    protected bool isPlayer;
    protected bool isFire = false;
    protected bool isCollecting = false;
    protected NavMeshAgent navAgent;
    public virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    public virtual void Start()
    {
        // Test 
        //All of these fields should be set in the editor
    //    maxSpeed = 3;
     //   armorStrength = 4;
     //   health = armorStrength;
    //    healthSlider.value = health / armorStrength;
      //  price = 1;
        isPlayer = owner is ControlledPlayer;
     //   Debug.Log(owner.GetType());
    }

    public virtual void Update()
    {
    }
    public void SetOwner(IPlayer owner)
    {
        this.owner = owner;
    }
    // Change UI according to the taken damage, return false if the ship is destoryed
    // The bool value returned signals to the attacking ship that it has been destroyed
    // will make it to where the attacking ship does not try to continue attacking a destroyed ship
    public bool takeDamage(int attack) {
        int currentHealth = health - attack;
        if (currentHealth <= 0)
        {
            health = 0;
            DestroyShip();
            return false;
        }
        health = currentHealth;
        healthSlider.value = health / armorStrength;
        return true;
    }

    // The ship is destoryed. Change animation==>delete gameobject in dictionary==>delete gameobject
    public void DestroyShip() {
        Destroy(gameObject);
    }

}
