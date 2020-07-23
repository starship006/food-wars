﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{

    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;



    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {


        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //if no neighbors, return no adjustment
        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        //Add all points together and average
        Vector2 cohesionMove = Vector2.zero;

        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }

        cohesionMove /= context.Count;

        //Create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
