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
    //public Slider slider;         // Health UI
    public float maxSpeed;
    public int armorStrength;       // Max health
    public int price;
    public int health;              // Current health

    // Start is called before the first frame update
    void Start()
    {
        // Test 
        maxSpeed = 3;
        armorStrength = 4;
        health = armorStrength;
        price = 1;
    }

    // Update is called once per frame
    void Update()
    {
        flyTo(target.transform.position);
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
        health -= attack;
        //slider.value = health;
        if (health <= 0)
        {
            destory();
            return false;
        }
        return true;
    }

    // The ship is destoryed. Change animation==>delete gameobject in dictionary==>delete gameobject
    void destory() {
        Destroy(m_Obj);
    }
}
