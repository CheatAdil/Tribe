using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutSolTools;
using TMPro;


public class Villager : Entity
{
    public string NAME;
    [SerializeField] private VillagerJob JOB;
    public bool FollowPlayer;
    private Transform pl;


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
        pl = Player.main.transform;
        IdleToMove = 3f;
        timer = 0f;
        if (JOB != VillagerJob.child)
        {
            Village.village.RegisterVillager(this);
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = NAME;
            text.color = Color.yellow;
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
            case States.carry_move:
                target = Village.village.getPosition() + Vector3.back;
                Vector3 directio = target - transform.position;
                transform.Translate(directio.normalized * movementSpeed * Time.deltaTime, Space.World);
                break;
            case States.idle:
            case States.carry_idle:
                timer += Time.deltaTime;
                if (timer >= IdleToMove)
                {
                    timer = 0;
                    if (!holding) SwitchState(States.move);
                    else SwitchState(States.carry_move);
                }
                break;
            default:
                break;
        }
        
    }
    private void followPlayer() 
    {
        if (JOB != VillagerJob.child)
        {
            if (!FollowPlayer)
            {
                FollowPlayer = true;
                text.color = Color.green;
                timer = 0;
                if (!holding) SwitchState(States.move);
                else SwitchState(States.carry_move);
            }
            else
            {
                FollowPlayer = false;
                text.color = Color.yellow;
                if (!holding) SwitchState(States.idle);
                else SwitchState(States.carry_idle); ;
                IdleToMove = 1f;
                timer = 0;
            }
        }
    }
    private void ToggleNameVisibility(bool on) 
    {
        if (text != null) 
        {
            text.enabled = on;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "FollowStop" && FollowPlayer)
        {
            if (!holding) SwitchState(States.idle);
            else SwitchState(States.carry_idle);
            IdleToMove = 1f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonfire") 
        {
            if (holding)
            {
                drop();
                Item = null;
                SwitchState(States.idle);
                IdleToMove = 1f;
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
            if (FollowPlayer && pl != null)
            {
                float newX = Random.Range(-0.15f, -0.15f);
                float maxY = Mathf.Sqrt(Mathf.Pow(0.15f, 2) - Mathf.Pow(newX, 2));
                float newY = Random.Range(-maxY, maxY);
                return pl.position + new Vector3(newX, newY, -1f);
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
                    if (!holding) SwitchState(States.idle);
                    else SwitchState(States.carry_idle);
                    IdleToMove = Random.Range(1f, 8f);
                    return Vector3.zero;
                }
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
