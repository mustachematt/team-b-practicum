using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public enum controlEnum { neutral, player1, player2 };
    public controlEnum control;

    private int curMetal;
    public int maxMetal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void switchControl(controlEnum c)
    {
        control = c;
    }
}