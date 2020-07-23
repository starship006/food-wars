﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/StayInRadius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2) agent.transform.position;
        float t = centerOffset.magnitude / radius; //could be optimized
        if(Math.Abs(t) < 0.9f)
        {
            return Vector2.zero;
        }
        else
        {
            return centerOffset * t * t;
        }

    }
}
