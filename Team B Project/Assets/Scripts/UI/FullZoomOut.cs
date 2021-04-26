using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullZoomOut : MonoBehaviour
{
    public CameraControl camControl;
    public Vector3 outPos = new Vector3(0, 1, 0);
    public Vector3 inPos;

    public float inFOV;
    public float outFOV = 120f;

    private Vector3 moveVel = Vector3.zero;
    private float zoomVel = 0;
    private float camSpeed = 0.4f;

    public void zoomOutFunc()
    {
        StartCoroutine(zoomOut());
        StartCoroutine(moveCam(outPos));
    }

    public IEnumerator zoomOut()
    {
        camControl.isZoomedOut = true;

        // saving these for zooming back in
        inPos = transform.position;
        inFOV = GetComponent<Camera>().orthographicSize;

        while (GetComponent<Camera>().orthographicSize < outFOV)
        {
            Debug.Log(GetComponent<Camera>().orthographicSize + " / " + outFOV);
            GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, outFOV, ref zoomVel, camSpeed);
            yield return null;
        }
        yield break;
    }

    public void zoomInFunc()
    {
        StartCoroutine(zoomIn(inPos));
        //StartCoroutine(moveCam(inPos));
    }

    public IEnumerator zoomIn(Vector3 dest)
    {
        while (GetComponent<Camera>().orthographicSize > inFOV || transform.position != dest)
        {
            GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, inFOV, ref zoomVel, camSpeed);
            transform.position = Vector3.SmoothDamp(transform.position, dest, ref moveVel, camSpeed);
            yield return null;
        }
        camControl.isZoomedOut = false;
    }

    public IEnumerator moveCam(Vector3 dest)
    {
        while (transform.position != dest)
        {
            transform.position = Vector3.SmoothDamp(transform.position, dest, ref moveVel, camSpeed);
            yield return null;
        }
    }
}
