using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControl : MonoBehaviour
{
    //This is used to control the camera panning.

    [SerializeField]float speed = 2.0f;
    float directionX = 0;
    float directionZ = 0;
    [SerializeField] int xLimit = 32;
    [SerializeField] int zLimit = 29;
    
    private void Start()
    {

    }
    private void Update()
    {
        {
            var newPos = Camera.main.transform.position + (new Vector3(directionX, 0, directionZ) * speed * Time.deltaTime);
            
            if(newPos.x < xLimit && newPos.x > -xLimit 
                && newPos.z < zLimit && newPos.z > -zLimit)
            {
                Camera.main.transform.position = newPos;
            }
        }   
    }
    private void OnPanHorizontal(InputValue value)
    {
        directionX = value.Get<float>();
    }
        private void OnPanVertical(InputValue value)
    {
        directionZ = value.Get<float>();
    }
}
