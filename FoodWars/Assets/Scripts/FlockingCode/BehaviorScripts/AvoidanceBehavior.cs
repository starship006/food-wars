using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //if no neighbors, return no adjustment
        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        //Add all points together and average
        Vector2 advoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform item in filteredContext)
        {

            if(Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                advoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
            //advoidanceMove += (Vector2)item.position;
        }

        if(nAvoid > 0)
        {
            advoidanceMove /= nAvoid;
        }        

        return advoidanceMove;
    }
}
