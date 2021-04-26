using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullZoomOut : MonoBehaviour
{
    public GameObject camControl;
    Vector3 outPos = new Vector3(0, 1, 0);
    Vector3 inPos;

    float inFOV;
    float outFOV = 120f;

    private Vector3 moveVel = Vector3.zero;
    private float zoomVel = 0;
    private float camSpeed = 0.4f;

    public void zoomOutFunc()
    {
        StartCoroutine(zoomOut());
    }

    public IEnumerator zoomOut()
    {
        camControl.GetComponent<CameraControl>().enabled = false;
        inPos = transform.position;
        inFOV = GetComponent<Camera>().orthographicSize;
        while (transform.position != outPos)
        {
            transform.position = Vector3.SmoothDamp(transform.position, outPos, ref moveVel, camSpeed);
            GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, outFOV, ref zoomVel, camSpeed);
            yield return null;
        }
    }

    public void zoomInFunc()
    {
        StartCoroutine(zoomIn());
    }

    public IEnumerator zoomIn()
    {
        while (transform.position != inPos)
        {
            transform.position = Vector3.SmoothDamp(transform.position, inPos, ref moveVel, camSpeed);
            GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, inFOV, ref zoomVel, camSpeed);
            yield return null;
        }
        camControl.GetComponent<CameraControl>().enabled = true;
    }
}
