using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    [Header("Villagers")]
    [SerializeField] private List<Villager> allVillagers;
    [SerializeField] private List<Villager> children;
    [SerializeField] private int maxVillagers;
    [SerializeField] private GameObject villagerPrefab;
    


    [Header("Food")]
    [SerializeField] private float FoodAvailable;
    [SerializeField] private float MaxFood;
    [SerializeField] private float FoodConsumption;

    [Header("Buildings")]
    [SerializeField] private float radius;
    [SerializeField] private Vector3 position;


	private void Start()
	{
        GameEvents.current.OnNewDay += NewDay;
    }
	private void CreateVillager(GameObject vlObj, Villager vl = null) 
    {
        Villager v = Instantiate(vlObj, new Vector3(4.38f, 0.59f, -5f), Quaternion.identity).GetComponent<Villager>();
        allVillagers.Add(v);
        //v = vl;
        //v.gameObject.SendMessage("StartVillager", GetComponent<Village>());
    }
    public float getRadius() 
    {
        return radius;
    }
    public Vector3 getPosition()
    {
        return position;
    }
    private void NewDay()
    {
        FoodAvailable -= (allVillagers.Count * 3 + children.Count * 1);
        while (allVillagers.Count + children.Count < maxVillagers && FoodAvailable > 5)
		{
            CreateVillager(villagerPrefab);
            FoodAvailable -= 5;
		}
        if (FoodAvailable < 0)
		{
            FoodAvailable = 0;
            allVillagers[0].SendMessage("Die", true);
            allVillagers.RemoveAt(0);
        }
            
    }

    #region Food
    public void RecieveFood(int foodValue)
	{
        FoodAvailable += foodValue;
	}
    #endregion

}

