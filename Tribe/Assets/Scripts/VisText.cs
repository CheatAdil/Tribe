using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisText : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "villager")
		{
			other.SendMessage("ToggleNameVisibility", true);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "villager")
		{
			other.SendMessage("ToggleNameVisibility", false);
		}
	}
}
