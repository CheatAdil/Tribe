using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Age : MonoBehaviour
{
    [SerializeField] private int bday;
    private int age;

    [SerializeField] private bool NaturalDeath;
    [SerializeField] private int deathAge;
    [SerializeField] private int rebirthAge;
    private bool dead;

    private WorldAge wa;
    private void Start()
    {
        wa = GameObject.Find("WorldAge").GetComponent<WorldAge>();
        GameEvents.current.OnNewDay += NewDay;
        if (!NaturalDeath) deathAge = -1;
    }
    private void NewDay() 
    {
        age = wa.GetWorldAge() - bday;
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
                    bday = wa.GetWorldAge();
                    GetComponent<Entity>().SendMessage("Die", false);
                }
            }
        }
        else 
        {
            if (age >= rebirthAge)
            {
                bday = wa.GetWorldAge();
                dead = false;
                GetComponent<Entity>().SendMessage("SwitchState", States.idle);
                SendMessage("Rebirth");
            }
        }
    }

}
