using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/AStarChase")]
public class AStarChaseBehavior : FlockBehavior
{
        
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {


        return Vector2.zero;
    }
}
