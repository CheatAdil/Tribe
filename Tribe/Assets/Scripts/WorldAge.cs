using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldAge : MonoBehaviour
{
    public static WorldAge current;
    private static int worldAge;
    [SerializeField] private float dayLength = 20f;
    public float dayTime;
    

    private void Awake()
    {
        current = this;
    }


    private void Update()
    {
        if (dayTime < dayLength) dayTime += Time.deltaTime;
        else
        {
            GlobalNewDay();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GlobalNewDay();
        }
    }
    static public int GetWorldAge() 
    {
        return worldAge;
    }
    public void GlobalNewDay() 
    {
        dayTime = 0;
        GameEvents.current.NewDay();
        worldAge += 1;
    }
    public float DayProgress()
	{
        return dayTime / dayLength;
	}


}
