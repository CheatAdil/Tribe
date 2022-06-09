using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    [Header("Villagers")]
    [SerializeField] private List<Villager> allVillagers;
    [SerializeField] private List<Villager> children;

    [Header("Food")]
    [SerializeField] private float FoodAvailable;
    [SerializeField] private float MaxFood;
    [SerializeField] private float FoodConsumption;

    [Header("Buildings")]
    [SerializeField] private float radius;
    [SerializeField] private Vector3 position;

    private void CreateVillager(GameObject vlObj, Villager vl) 
    {
        Villager v = Instantiate(vlObj).GetComponent<Villager>();
        v = vl;
        v.gameObject.SendMessage("StartVillager", GetComponent<Village>());
    }
    public float getRadius() 
    {
        return radius;
    }
    public Vector3 getPosition() 
    {
        return position;
    }
}

