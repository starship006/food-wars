using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;


    public float spawnOffset;
    [Range(0, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)] public float driveFactor = 10f;  //flock agents start out pretty slow, so we multiply this to make it faster. 1f would be original speed
    [Range(1f, 100f)] public float maxSpeed = 5f;

    [Range(1f, 10f)] public float neighborRadius = 1.5f;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier = 0.9f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;

    int spawnNumberWhenPressed = 4;
    public float SquareAvoidanceRadius  { get { return squareAvoidanceRadius; }  }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab, //what to instantiate
                Random.insideUnitCircle * startingCount * AgentDensity,   //where its located at 
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),   //random rotation
                transform //transform as parent
                );

            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }

        //suscribe to events
        GameEvents.instance.onSpacebarPressed += OnSpacebarPressed;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
           
            //For Demo to see this work
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red,context.Count / 6f);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }

    public void OnSpacebarPressed(Transform position)
    {
        for (int i = 0; i < spawnNumberWhenPressed; i++)
        {
            FlockAgent newAgent = Instantiate(
               agentPrefab, //what to instantiate
               position.position + new Vector3(Random.Range(-spawnOffset, spawnOffset),Random.Range(-spawnOffset, spawnOffset)),   //where its located at         --CHANGE
               Quaternion.identity,
               transform //transform as parent
               );

            newAgent.name = "Agent";
            newAgent.Initialize(this);
            agents.Add(newAgent);
            Debug.Log("Added Agents");
        }        
        
    }
}
