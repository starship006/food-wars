using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/AStarChase")]
public class AStarChaseBehavior : FlockBehavior
{
    GameObject inGameTarget;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(inGameTarget == null)
        {
            inGameTarget = GameObject.FindGameObjectWithTag("Enemy");
        }
        else
        {
            List<Node> path = Pathfinder.instance.FindPath(agent.transform.position, inGameTarget.transform.position);
            if(path.Count > 1)
            {
                 Vector3 direction = path[1].worldPosition - path[0].worldPosition;
                 return direction.normalized;
            }         
            
        }

        return Vector2.zero;
    }
}
