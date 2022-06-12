using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private static World main;
    [SerializeField] private float radius;
    [SerializeField] private float minDist;
    [SerializeField] private GameObject[] objects;
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        Generate(4000);
    }
    private void Generate(int count) 
    {
        if (objects.Length != 0)
        {
            float radial;
            float dist;
            int id;
            Vector3 orig = Village.village.getPosition();
            Vector3 spawnpos;
            for (int i = 0; i < count; i++)
            {
                id = Random.Range(0, objects.Length);
                if (objects[id] != null)
                {
                    radial = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                    dist = Random.Range(minDist, radius);
                    spawnpos = orig + new Vector3(Mathf.Cos(radial) * dist , Mathf.Sin(radial) * dist, -1f);
                    Instantiate(objects[id], spawnpos, Quaternion.identity);
                }
            }
        }
    }
}
