using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollector : MonoBehaviour
{
	[SerializeField] private Village village;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "food")
		{
			village.RecieveFood(other.GetComponent<FoodItem>().foodValue);
			Destroy(other.gameObject);


			//Play sound
		}
	}


}
