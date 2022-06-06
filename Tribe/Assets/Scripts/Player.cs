using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
	private void Update()
	{
		Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
		transform.Translate(movementDirection.normalized * movementSpeed * Time.deltaTime, Space.World);


		if (Input.GetKeyDown(KeyCode.Space)) Attack();
	}
}
