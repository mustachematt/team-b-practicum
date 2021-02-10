using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitSpawnTest : MonoBehaviour
{
    GameObject[] waypoints;
    [SerializeField] GameObject[] prefab;   //made this an array to try opposing ships
    
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("spawn");
        Debug.Log("Hello" + waypoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnShip()
    {
        GameObject.Instantiate(prefab[Random.Range(0, prefab.Length)], waypoints[Random.Range(0,waypoints.Length)].transform);
    }
}
