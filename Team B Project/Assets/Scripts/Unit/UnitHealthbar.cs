using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthbar : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.down);
    }
}
