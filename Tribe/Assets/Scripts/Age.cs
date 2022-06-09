using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Age : MonoBehaviour
{
    [SerializeField] private int age;

    [SerializeField] private bool NaturalDeath;
    [SerializeField] private int deathAge;
    [SerializeField] private int rebirthAge;
    [SerializeField] private bool dead;

    private void Start()
    {
        GameEvents.current.OnNewDay += NewDay;
        if (!NaturalDeath) deathAge = -1;
    }
    private void NewDay() 
    {
        age++;
        print(1);
        CheckDates();
    }
    private void Killed() /// eto kogda on umiraet ranshe vremeni
    {
        dead = true;
        age = 0;
    }
    private void CheckDates() 
    {
        if (!dead) 
        {
            if (NaturalDeath)
            {
                if (age >= deathAge)
                {
                    dead = true;
                    print(2);
                    age = 0;
                    print(3);
                    GetComponent<Entity>().SendMessage("Die", false);
                    print(4);
                }
            }
        }
        else 
        {
            if (age >= rebirthAge)
            {
                age = 0;
                dead = false;
                GetComponent<Entity>().SendMessage("SwitchState", States.idle);
                SendMessage("Rebirth");
            }
        }
    }

}
