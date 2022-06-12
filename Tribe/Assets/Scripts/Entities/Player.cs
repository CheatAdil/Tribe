using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
	public static Player main;

    private void Awake()
    {
		main = this;
    }

    private void Update()
	{
		Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
		transform.Translate(movementDirection.normalized * movementSpeed * Time.deltaTime, Space.World);
		if (movementDirection == Vector3.zero) 
		{
			if (!holding) SwitchState(States.idle);
			else SwitchState(States.carry_idle);
		}
		else 
		{
			if (!holding) SwitchState(States.move);
			else SwitchState(States.carry_move);
		}


		if (Input.GetKeyDown(KeyCode.Space)) Attack();
		if (Input.GetKeyDown(KeyCode.E)) Interact();
	}
    
}

