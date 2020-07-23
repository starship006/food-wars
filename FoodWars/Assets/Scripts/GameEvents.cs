using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour { 
   
    public static GameEvents instance;


    

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public event Action<Transform> onSpacebarPressed;
    public void SpacebarPressed(Transform position)
    {
        if(onSpacebarPressed != null)
        {
            onSpacebarPressed(position);
        }
    }

}
