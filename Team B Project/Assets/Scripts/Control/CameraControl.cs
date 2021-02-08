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
    
    private void Start()
    {

    }
    private void Update()
    {
        Camera.main.transform.position += new Vector3(directionX, 0, directionZ) * speed * Time.deltaTime; 
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
