using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class GameEvents : MonoBehaviour { 
   
    public static GameEvents instance;
    private float oneSec = 1;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(oneSec >= 0)
        {
            oneSec -= Time.deltaTime;
        }
        else
        {
            SecondPassed();
            oneSec = 1;
        }

    }
    public event Action<Transform> onSpacebarPressed;
    public void SpacebarPressed(Transform position)
    {
        if(onSpacebarPressed != null)
        {
            onSpacebarPressed(position);
        }
    }

    public event Action onSecondPassed;
    public void SecondPassed()
    {
        if(onSecondPassed != null)
        {
            onSecondPassed();
        }
    }
}
