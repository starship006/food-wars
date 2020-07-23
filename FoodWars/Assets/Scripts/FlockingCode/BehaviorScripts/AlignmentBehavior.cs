using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //if no neighbors, maintian current alignment
        if (filteredContext.Count == 0)
        {
            return agent.transform.up;
        }

        //Add all points together and average
        Vector2 alignmentMove = Vector2.zero;

        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }

        alignmentMove /= context.Count;

        
        return alignmentMove;
    }
}
