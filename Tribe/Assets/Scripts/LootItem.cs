using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutSolTools;
[CreateAssetMenu] 

public class LootItem : ScriptableObject
{
    [SerializeField] private string Name;
    [SerializeField] private GameObject obj;

    [Header("Drop settings")]
    [SerializeField] private float weight;
    [SerializeField] private int amountMin;
    [SerializeField] private int amountMax;
    [SerializeField] private float dropForce;
    [SerializeField] private float duration;
    [SerializeField] private float variance;

    
    public float GetWeight() 
    {
        return weight;
    }
    public void Drop(Vector3 pos) 
    {
        int amount = Random.Range(amountMin, amountMax + 1);
        GameObject[] loots = new GameObject[amount];
        if (obj != null)
        {
            for (int i = 0; i < amount; i++)
            {
                loots[i] = Instantiate(obj, pos, Quaternion.identity);
            }
            GameObject n = new GameObject();
            n.name = "lootDropper";
            n.transform.parent = GameObject.Find("UTILITY OBJECT").transform;
            n.AddComponent<LootDropper>();
            n.GetComponent<LootDropper>().DropLoot(loots, dropForce, duration, variance);
        }
    }
}

