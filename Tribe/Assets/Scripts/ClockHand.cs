using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    private WorldAge wa;
	private void Awake()
	{
		wa = GameObject.Find("WorldAge").GetComponent<WorldAge>();
	}
	private void Update()
	{
		transform.eulerAngles = Vector3.forward * wa.DayProgress() * -360;
	}
}
