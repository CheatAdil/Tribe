using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Age : WorldAge
{
    [SerializeField] private int bday;
    private int age;

    [SerializeField] private bool NaturalDeath;
    [SerializeField] private int deathAge;
    [SerializeField] private int rebirthAge;
    private bool dead;


    private void Start()
    {
        if (!NaturalDeath) deathAge = -1;
        OnNewDay += NewDay;
    }
    private void NewDay() 
    {
        age = GetWorldAge() - bday;
        CheckDates();
    }
    private void Killed() /// eto kogda on umiraet ranshe vremeni
    {
        dead = true;
        NewDay();
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
                    bday = GetWorldAge();
                    GetComponent<Entity>().SendMessage("Die", false);
                }
            }
        }
        else 
        {
            if (age >= rebirthAge)
            {
                bday = GetWorldAge();
                dead = false;
                GetComponent<Entity>().SendMessage("SwitchState", States.idle);
                SendMessage("Rebirth");
            }
        }
    }

}
