using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControl : MonoBehaviour
{
    [SerializeField] float minCamSize = 10f;
    [SerializeField] float maxCamSize = 30f;
    [SerializeField] float zoomSpeed = 0.5f;
    //This is used to control the camera panning.
    [SerializeField] float speed = 15f;
    float directionX = 0;
    float directionZ = 0;
    [SerializeField] int xLimit = 32;
    [SerializeField] int zLimit = 29;

    private void Update()
    {
        {
            var pos = Camera.main.transform.position;
            var newPos = Camera.main.transform.position + (new Vector3(directionX, 0, directionZ) * speed * Time.deltaTime);
            
            if(newPos.x < xLimit && newPos.x > -xLimit)
            {
                Camera.main.transform.position = new Vector3(newPos.x, pos.y, pos.z);
            }
            pos = Camera.main.transform.position;
            if (newPos.z < zLimit && newPos.z > -zLimit)
            {
                Camera.main.transform.position = new Vector3(pos.x, pos.y, newPos.z);
            }
        }   
    }

    private void OnZoom(InputValue value)
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
    private void OnPanHorizontal(InputValue value)
    {
        directionX = value.Get<float>();
    }
    private void OnPanVertical(InputValue value)
    {
        directionZ = value.Get<float>();
    }
}
