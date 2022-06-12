using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    [Header("Villagers")]
    [SerializeField] private List<Villager> Adults;
    [SerializeField] private List<Villager> Children;
    [SerializeField] private int maxVillagers;
    [SerializeField] private GameObject villagerPrefab;
    [SerializeField] private GameObject childPrefab;
    
    [Header("Food")]
    [SerializeField] private int FoodAvailable;
    [SerializeField] private int MaxFood;
    [SerializeField] private int FoodConsumption;
    [SerializeField] private int VillagerCostSpawn;

    [Header("Buildings")]
    [SerializeField] private float Spawn_radius;
    [SerializeField] private float Total_radius;
    [SerializeField] private Transform Center_position;

    [Header("Interface")]
    [SerializeField] private GameObject VillageInterface;

    public static Village village;

    private void Awake()
    {
        village = this;
    }
    private void Start()
	{
        GameEvents.current.OnNewDay += NewDay;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            SpawnVillager();
        }
    }
    private void LateUpdate()
    {
        ReCheckVillagers();
    }
    public void SpawnVillager() 
    {
        if (VillagerCostSpawn < FoodAvailable) 
        {
            FoodAvailable -= VillagerCostSpawn;
            CreateVillager(childPrefab);
            FoodConsumption = ConsumedFood();
        }
    }
	private void CreateVillager(GameObject vlObj, string his_name = "child") 
    {
        float newX = Random.Range(-Spawn_radius, Spawn_radius);
        float maxY = Mathf.Sqrt(Mathf.Pow(Spawn_radius, 2) - Mathf.Pow(newX, 2));
        float newY = Random.Range(-maxY, maxY);
        Vector3 sp = Center_position.position + new Vector3(newX, newY, -1f); // cos bonfire is -4 and we need -5
        Villager v = Instantiate(vlObj, sp, Quaternion.identity).GetComponent<Villager>();
        v.NAME = his_name;
        Children.Add(v); 
    }
    public void RegisterVillager(Villager v, string his_name = "random") 
    {
        Adults.Add(v);
        if (his_name == "random") v.NAME = Names_vil.NewName();
        else v.NAME = his_name;

    }
    public float getRadius() 
    {
        return Total_radius;
    }
    public Vector3 getPosition()
    {
        return Center_position.position;
    }
    private void NewDay()
    {
        FoodConsumption = ConsumedFood();
        FoodAvailable -= FoodConsumption;
        if (FoodAvailable < 0)
		{
            if (Adults.Count != 0)
            {
                int toDie = (FoodAvailable / (ConsumedFood() / Adults.Count)) * -1;
                FoodAvailable = 0;
                print($"day: {WorldAge.GetWorldAge()} : to die of starvation: {toDie}");
                toDie -= Random.Range(0, toDie);
                print($"day: {WorldAge.GetWorldAge()} : to die after divine intervention: {toDie}");
                for (int i = 0; i < toDie; i++)
                {
                    Adults[0].SendMessage("Die", true);
                    Adults.RemoveAt(0);
                }
            }
        }
            
    }
    private void ReCheckVillagers() 
    {
        Adults.RemoveAll(x => !x);
        Children.RemoveAll(x => !x);
    }
    private int ConsumedFood() 
    {
        return (3 + (Adults.Count * 3 + Children.Count * 1));
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.name == "holdingPoint") VillageInterface.SetActive(true);
    }
	private void OnTriggerExit(Collider other)
	{
        if (other.name == "holdingPoint") VillageInterface.SetActive(false);
    }
	public void RecieveFood(int foodValue)
	{
        FoodAvailable += foodValue;
	}



}

