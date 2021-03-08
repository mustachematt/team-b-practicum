using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ship : MonoBehaviour
{
    public enum shipType { Attack, Transport };
    public shipType kind;
    public GameObject target;       //Initally hold enemy's base(Attack)/resource point(Transport).
    public Slider healthSlider;         // Health UI
    public float maxSpeed;
    public int armorStrength;           // Max health
    public int price;
    public int health;                  // Current health
    public GameObject owner;           // Who does the ship belong to
    protected bool isPlayer;
    public bool isFire = false;
    public bool isCollecting = false;

    public virtual void Start()
    {
        // Test 
        maxSpeed = 3;
        armorStrength = 4;
        health = armorStrength;
        healthSlider.value = health / armorStrength;
        price = 1;
        isPlayer = (owner.tag == "SpawnPlayer1");
        // Place holder for setting inital target
    }

    public virtual void Update()
    {
    }

    // Move to target
    public void flyTo(Vector3 pos) {
        //Vector3 movePos = (target.transform.position - gameObject.transform.position).normalized * maxSpeed;
        //gameObject.GetComponent<Rigidbody>().MovePosition(movePos); 
    }

    // Change UI according to the taken damage, return false if the ship is destoryed
    // The bool value returned signals to the attacking ship that it has been destroyed
    // will make it to where the attacking ship does not try to continue attacking a destroyed ship
    public bool takeDamage(int attack) {
        int currentHealth = health - attack;
        if (currentHealth <= 0)
        {
            health = 0;
            destory();
            return false;
        }
        health = currentHealth;
        healthSlider.value = health / armorStrength;
        return true;
    }

    // The ship is destoryed. Change animation==>delete gameobject in dictionary==>delete gameobject
    public void destory() {
        Destroy(this);
    }
}
