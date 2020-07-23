using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistanceBehavior : FlockBehavior
{
    public GameObject target;
    public ContextFilter filter;
    public float neighborDistanceMultiplier;
}
