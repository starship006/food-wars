using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    float distanceScale = 2;
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = PlayerMovement.globalMovementVector * distanceScale;
    }
}
