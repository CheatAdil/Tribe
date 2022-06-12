using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] private GameObject[] loot;
    [SerializeField] private Vector3[] targets;
    [SerializeField] private Vector3[] orig;
    [SerializeField] private float[] vars;
    private float f;
    [SerializeField] float z = 0;
    private float timer = 0;
    private float tm;

    public void DropLoot(GameObject[] l, float force, float t, float v) 
    {
        tm = t;
        loot = l;
        f = force;
        timer = 0;

        targets = new Vector3[l.Length];
        orig = new Vector3[l.Length];
        vars = new float[l.Length];

        for (int i = 0; i < l.Length; i++) 
        {
            vars[i] = Random.Range(1f, 1f + v);
            orig[i] = l[i].transform.position;

            targets[i] = (new Vector3(Mathf.Cos(Random.Range(0, 360f) * Mathf.Deg2Rad), Mathf.Sin(Random.Range(0, 360f)), z)) - orig[i];
        }
    }
    private void Update()
    {
        if (timer < tm)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < loot.Length; i++)
            {
                loot[i].transform.position += targets[i] * vars[i] * f * Time.deltaTime;
            }
        }
        else if (timer >= tm)
        {
            Destroy(this.gameObject);
        }
    }
}
