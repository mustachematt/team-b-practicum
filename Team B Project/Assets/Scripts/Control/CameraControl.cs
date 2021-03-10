using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraControl : MonoBehaviour
{
    public float edgeClearance = 2.5f;
    [SerializeField] float speed = 15f;
    
    [Header("Zoom Options")]
    [Space]
    [SerializeField] float minCamSize = 10f;
    [SerializeField] float maxCamSize = 30f;
    [SerializeField] float zoomSpeed = 0.5f;
    //This is used to control the camera panning.
    
    [Header("Screen  Size")]
    [Space]
    [SerializeField] int xLimit = 32;
    [SerializeField] int zLimit = 29;
    
    private bool keyMoving;
    private Vector3 edgeMove;
    private float directionZ = 0;
    private float directionX = 0;
    private void Update()
    {
        {
            keyMoving = false;
            //Vector3 mPOS = Input.mousePosition;

            
            var pos = Camera.main.transform.position;
            var newPos = Camera.main.transform.position + (new Vector3(directionX, 0, directionZ) * speed * Time.deltaTime);
            
            if(newPos.x < xLimit && newPos.x > -xLimit)
            {
                keyMoving = true;
                Camera.main.transform.position = new Vector3(newPos.x, pos.y, pos.z);
            }
            pos = Camera.main.transform.position;
            if (newPos.z < zLimit && newPos.z > -zLimit)
            {
                keyMoving = true;
                Camera.main.transform.position = new Vector3(pos.x, pos.y, newPos.z);
            }
            /*if(!keyMoving)
            {
                if(mPOS.x >= Screen.width - edgeClearance)
                {
                      edgeMove += Vector3.forward * speed * Time.deltaTime; 
                }

                Camera.main.transform.position = edgeMove;
                
            }
            */
        }
        
    }

    public void OnZoom(InputValue value)
    {
        var zoomDir = value.Get<Vector2>();
        if(zoomDir.y == 1f)
        {
            Camera.main.orthographicSize -= zoomSpeed;
            if (Camera.main.orthographicSize < minCamSize)
                Camera.main.orthographicSize = minCamSize;
        }
        if(zoomDir.y == -1f)
        {
            Camera.main.orthographicSize += zoomSpeed;
            if (Camera.main.orthographicSize > maxCamSize)
                Camera.main.orthographicSize = maxCamSize;
        }
    }
    public void OnPanHorizontal(InputValue value)
    {
        directionX = value.Get<float>();
    }
    public void OnPanVertical(InputValue value)
    {
        directionZ = value.Get<float>();
    }
    public void OnCameraSpeed(InputValue value)
    {
        if(value.Get<float>() > 0f) speed *=  2;
        
        else if(value.Get<float>() == 0.0f) speed /= 2;
    }

}
