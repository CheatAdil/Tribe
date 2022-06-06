using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutSolTools;

public class Entity : MonoBehaviour
{
	[Header("Basic")]
	[SerializeField] protected string entityName;
    [SerializeField] protected int maxHealth;
    [SerializeField] public int health;
    [SerializeField] protected float movementSpeed;

	[Header("Attack")]
	[SerializeField] protected float damage;
	[SerializeField] protected float attack_reach;
	[SerializeField] private LayerMask attack_what;

	[Header("Holding")]
	[SerializeField] protected bool canHold;
	[SerializeField] private LayerMask hold_what;
	[SerializeField] protected bool holding;
	[SerializeField] protected Transform holdingPoint;
	[SerializeField] protected GameObject Item;

	[Header("Loot")]
	[SerializeField] protected GameObject[] loot; ///to change

	[Header("Sprites")]
	[SerializeField] protected States state;
	[SerializeField] protected List<Sprite_set> Sprite_sets;  // index must correspond to state index

	private Sprite_animator anim;
	private void Start()
	{
		health = maxHealth;
		anim = GetComponent<Sprite_animator>();
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
		Collider[] colliders = Physics.OverlapSphere(transform.position, attack_reach, attack_what);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].tag != this.tag)
				colliders[i].SendMessage("GetHurt", damage);
		}
	}
	protected void Interact()
	{
		if (canHold)
		{
			if (holding)
			{
				drop();
			}
			else
			{
				//// ןונגûל הוכמל ןמהבטנאול
				Collider[] colliders = Physics.OverlapSphere(transform.position, attack_reach, hold_what); 
				if (colliders.Length != 0) 
				{
					pickup(colliders[0].gameObject);
				}
				else 
				{
					// check for other interactions
				}
			}
		}
	}

	private void pickup(GameObject item) 
	{
		if (holdingPoint == null) print("no holding point on this object");
		holding = true;
		item.transform.position = holdingPoint.position;
		item.transform.SetParent(transform);
		Item = item;
	}
	private void drop()
	{
		holding = false;
		Item.transform.SetParent(null);
	}
    private void Die()
	{
		print(name + " has died");
	//	if (loot != null) Instantiate(loot, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
	protected void SwitchState(States s) 
	{
		if (s != state)
		{
			state = s;
			if (anim != null)
			{
				anim.init(Sprite_sets[((int)state)].SPRITES);
			}
		}
	}
}
public enum States
{
	idle,         // 0
	move,         // 1
	attack,       // 2
	carry_idle,   // 3
	carry_move,   // 4
	dead,         // 5
}
