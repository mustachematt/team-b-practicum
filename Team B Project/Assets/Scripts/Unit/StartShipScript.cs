using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartShipScript : MonoBehaviour
{
    public enum shipType { Attack, Transport }; // Temporary ship type
    public GameObject target;       //Initally hold enemy's base(Attack)/resource point(Transport).
    public shipType shipKind;
    public float aTime = 0;         // Attack timer
    public GameObject m_Obj;        // Itself
    public Slider healthSlider;         // Health UI
    public float maxSpeed;
    public int armorStrength;           // Max health
    public int price;
    public int health;                  // Current health
    public GameObject owner;           // Who does the ship belong to
    protected bool isPlayer;
    public bool isFire = false;
    public bool isCollecting = false;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
    }

    // Move to target
    public void flyTo(Vector3 pos) {
        Vector3 movePos = (target.transform.position - m_Obj.transform.position).normalized * maxSpeed;
        m_Obj.GetComponent<Rigidbody>().MovePosition(movePos); 
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
    void destory() {
        Destroy(m_Obj);
    }
}
