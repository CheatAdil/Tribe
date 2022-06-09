using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    private WorldAge wa;
	private void Awake()
	{
		wa = WorldAge.current;
	}
	private void Update()
	{
		if (wa != null)
		{
			transform.eulerAngles = Vector3.forward * wa.DayProgress() * -360;
		}
	}
}
