using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 12.5f;  //the larger, the longer the camera will spend locking onto our target
    public float zOffset;


    float horizontal;
    float vertical;
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition;
        if(Math.Abs(horizontal) > 0 && Math.Abs(vertical) > 0)
        {
            desiredPosition = target.position + new Vector3(horizontal, vertical, zOffset);
        }
        else if(Math.Abs(horizontal) > 0)
        {
            desiredPosition = target.position + new Vector3(horizontal, 0, zOffset);
        } else if (Math.Abs(vertical) > 0)
        {
            desiredPosition = target.position + new Vector3(0, vertical, zOffset);
        }
        else
        {
            desiredPosition = target.position + new Vector3(0, 0, zOffset);
        }
                
        transform.position = desiredPosition;
    }

}
