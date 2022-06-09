using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldAge : MonoBehaviour
{
    public static WorldAge current;
    private static int WrldAge;

    private void Awake()
    {
        current = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GlobalNewDay();
        }
    }
    protected int GetWorldAge() 
    {
        return WrldAge;
    }
    public void GlobalNewDay() 
    {
        if (OnNewDay != null) 
        {
            OnNewDay();
            WrldAge += 1;
        }
    }
    public event Action OnNewDay;
    
}
