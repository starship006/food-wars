using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCook : MonoBehaviour
{
    public GameObject foodObject;
    public GameObject flockObject;
    public List<GameObject> flocks;

 


    // Start is called before the first frame update
    void Start()
    {
        CreateFlocks();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacebarPressed();            
        }
    }
    

    void CreateFlocks()             // still needs code for if there isn't flock code avaialble 
    {
        FlockAgent flockPiece = foodObject.GetComponent<FlockAgent>();
        if (flockPiece != null)
        {
            GameObject newFlockObject = Instantiate(flockObject);
            newFlockObject.name = "NewFlockObject";
            Flock flock = newFlockObject.GetComponent<Flock>();
            flock.agentPrefab = foodObject.GetComponent<FlockAgent>();
            flocks.Add(newFlockObject);
        }
        
    }
    void OnSpacebarPressed()
    {
        Transform target = transform.GetChild(0).transform;
        GameEvents.instance.SpacebarPressed(target);
    }
}
