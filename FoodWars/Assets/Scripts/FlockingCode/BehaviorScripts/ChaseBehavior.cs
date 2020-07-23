using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Chase")]
public class ChaseBehavior : DistanceBehavior
{

    GameObject inGameTarget;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 chaseMove = Vector2.zero;
        //were just gonna ignore the context for this one, but instead use the transform stuff that is in the other abstract definiton
        if(inGameTarget == null)
        {
            inGameTarget = GameObject.FindGameObjectWithTag("Enemy");
            return chaseMove;
        }
        else
        {
            chaseMove = (Vector2)inGameTarget.transform.position - (Vector2) agent.transform.position;
            return chaseMove;
        }
    }
}
