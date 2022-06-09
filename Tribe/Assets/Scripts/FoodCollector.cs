using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    [SerializeField] private int foodValue;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "food")
		{
			foodValue += other.GetComponent<FoodItem>().foodValue;
			Destroy(other.gameObject);


			//Play sound
		}
	}

}
