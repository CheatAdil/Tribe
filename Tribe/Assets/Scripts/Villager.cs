using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutSolTools;
using TMPro;


public class Villager : Entity
{
    public string NAME;
    [SerializeField] private VillagerJob JOB;
    private TextMeshProUGUI text;

    #region Movement
    private Vector3 target;
    [SerializeField] private float allowedError;
    const float pi = Mathf.PI;
    private float IdleToMove;
    private float timer;
    #endregion
    private void Awake() // для тестов
    {
        IdleToMove = 3f;
        timer = 0f;
        if (JOB != VillagerJob.child)
        {
            Village.village.RegisterVillager(this);
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = NAME;
            ToggleNameVisibility(false);
        }
    }
    private void Update()
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

    private void ToggleNameVisibility(bool on) 
    {
        if (text != null) 
        {
            text.enabled = on;
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
                float newX = Random.Range(-Village.village.getRadius(), Village.village.getRadius());
                float maxY = Mathf.Sqrt(Mathf.Pow(Village.village.getRadius(), 2) - Mathf.Pow(newX, 2));
                float newY = Random.Range(-maxY, maxY);
                return (Village.village.getPosition() + new Vector3(newX, newY, -1f));
            }
            else 
            {
                SwitchState(States.idle);
                IdleToMove = Random.Range(1f, 8f);
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
