using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	[SerializeField] protected string entityName;
    [SerializeField] protected int maxHealth;
    public int health;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected GameObject loot;
	private void Start()
	{
		health = maxHealth;
	}
	private void GetHurt(int damage)
	{
        health -= damage;
        if (health <= 0f)
		{
            Die();
		}
	}
	protected void Attack()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].tag != this.tag)
				colliders[i].SendMessage("GetHurt", 5);
		}
	}
	protected void Interact()
	{

	}
    private void Die()
	{
		print(name + " has died");
		if (loot != null) Instantiate(loot, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
}
