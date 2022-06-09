using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutSolTools;


public class Villager : Entity
{
    [SerializeField] private string NAME;
    [SerializeField] private VillagerJob JOB;

    private Village village;


    #region Movement
    private Vector3 target;
    [SerializeField] private float allowedError;
    const float pi = Mathf.PI;
    private float IdleToMove;
    private float timer;
    #endregion
    private void Awake() // для тестов
    {
        village = GameObject.Find("UTILITY OBJECT").GetComponent<Village>();
    }
    private void StartVillager(Village vil) 
    {
        village = vil;
        IdleToMove = 8f;
        timer = 0f;
    }

    private void Update()
    {
        if (village != null)
        {
            switch (state)
            {
                case States.move:
                    target = UpdateTarget(target);
                    Vector3 direction = target - transform.position;
                    transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);
                    break;
                case States.idle:
                    timer += Time.deltaTime;
                    if (timer >= IdleToMove) 
                    {
                        timer = 0;
                        SwitchState(States.move);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private Vector3 UpdateTarget(Vector3 _target) 
    {
        if (Vector3.Distance(transform.position, _target) > allowedError) 
        {
            return _target;
        }
        else 
        {
            if (OutSolTools.CoreFunctions.CheckChance(0.3f))
            {
                float newX = Random.Range(-village.getRadius(), village.getRadius());
                float maxY = Mathf.Sqrt(Mathf.Pow(village.getRadius(), 2) - Mathf.Pow(newX, 2));
                float newY = Random.Range(-maxY, maxY);
                return (village.getPosition() + new Vector3(newX, newY, 0f));
            }
            else 
            {
                SwitchState(States.idle);
                IdleToMove = Random.Range(3f, 16f);
                return Vector3.zero;
            }
        }
    }






}

public enum VillagerJob 
{
    collector,
    warrior,
    child,
}
