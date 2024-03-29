using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutSolTools;
using System.Linq;

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

	[Header("Loot")]
	[SerializeField] protected LootItem[] DeathLoot;
	[SerializeField] protected LootItem[] HitLoot;


	[Header("Sprites")]
	[SerializeField] protected States state;
	[SerializeField] protected List<Sprite_set> Sprite_sets;  // index must correspond to state index
	[SerializeField] protected bool StayAfterDeath;


	[Header("Holding")]
	[SerializeField] protected bool canHold;
	[SerializeField] private LayerMask hold_what;
	[SerializeField] private float holdDistance;
	[SerializeField] protected bool holding;
	[SerializeField] protected Transform holdingPoint;
	[SerializeField] protected GameObject Item;

	[Header("Interactions")]
	[SerializeField] protected LayerMask interacts_with;  /// �������� �� ��������
	
	





	private Sprite_animator anim;

	#region drops
	[SerializeField] private float total_weight_death;
	private float rand_weight_death;
	private bool drops_init_death;

	[SerializeField] private float total_weight_hit;
	private float rand_weight_hit;
	private bool drops_init_hit;


	#endregion
	private void Start()
	{
		health = maxHealth;
		anim = GetComponent<Sprite_animator>();
		InitDrop();
		if (anim != null)
		{
			anim.enabled = true;
			anim.init(Sprite_sets[((int)state)].SPRITES, Sprite_sets[((int)state)].repeat_times);
		}
	}
	private void Rebirth() 
	{
		Start();
	}
	private void GetHurt(int damage)
	{
		if (state != States.dead)
		{
			LootItem loot = HitDrop();
			if (loot != null) loot.Drop(transform.position);
			health -= damage;
			if (health <= 0f)
			{
				Die(true);
				if (TryGetComponent<Age>(out Age a))
				{
					SendMessage("Killed", this.gameObject);
				}
			}
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
				Collider[] colliders = Physics.OverlapSphere(transform.position, holdDistance, interacts_with);
				if (colliders.Length != 0)
				{
					if (colliders[0].tag == "villager")
					{
						drop();
						colliders[0].SendMessage("pickup", Item);
						colliders[0].SendMessage("SwitchState", States.carry_move);
						Item = null;
						return;
					}
				}
				drop();
				Item = null;
			}
			else
			{
				////���������
				Collider[] colliders = Physics.OverlapSphere(holdingPoint.position, holdDistance, hold_what); 
				if (colliders.Length != 0) 
				{
					pickup(colliders[0].gameObject);
				}
				else 
				{
					OtherInteractions();

				}
			}
		}
		else 
		{
			OtherInteractions();
		}
	}
	private void OtherInteractions()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, attack_reach, interacts_with);
		if (colliders.Length != 0)
		{
			if (colliders[0].tag == "villager")
			{
				colliders[0].SendMessage("followPlayer");
			}
		}
	}
	private void pickup(GameObject item) 
	{
		if (holdingPoint == null) print("no holding point on this object");
		holding = true;
		item.transform.position = holdingPoint.position;
		item.transform.SetParent(holdingPoint);
		Item = item;
		Item.GetComponent<BoxCollider>().enabled = false;
	}
	protected void drop()
	{
		holding = false;
		Item.GetComponent<BoxCollider>().enabled = true;
		Item.transform.SetParent(null);
	}
    private void Die(bool drop)
	{
		print(name + " just died");
		if (!StayAfterDeath)
		{
			Destroy(this.gameObject);
		}
		SwitchState(States.dead);

		if (drop)
		{
			LootItem loot = DeathDrop();
			if (loot != null) loot.Drop(transform.position);
		}
	}
	protected void SwitchState(States s) 
	{
		if (s != state)
		{
			state = s;
			if (anim != null)
			{
				anim.enabled = true;
				if (Sprite_sets[((int)state)] != null)
				{
					anim.init(Sprite_sets[((int)state)].SPRITES, Sprite_sets[((int)state)].repeat_times);
				}
				else 
				{
					Debug.LogWarning($"no sprites on state {s}: object: {name}");
				}
			}
		}
	}

	private void InitDrop() 
	{
		drops_init_death = false;
		drops_init_hit = false;
		if (DeathLoot.Length != 0)
		{
			total_weight_death = DeathLoot.Sum(dl => dl.GetWeight());
			drops_init_death = true;
		}
		if (HitLoot.Length != 0) 
		{
			total_weight_hit = DeathLoot.Sum(hl => hl.GetWeight());
			drops_init_hit = true;
		}	
	}
	private LootItem DeathDrop() 
	{
		float roll = Random.Range(0f, total_weight_death);

		for (int i = 0; i < DeathLoot.Length; i++) 
		{
			if (DeathLoot[i].GetWeight() >= roll) 
			{
				return DeathLoot[i];
			}
			roll -= DeathLoot[i].GetWeight();
		}
		return null;
	}
	private LootItem HitDrop()
	{
		float roll = Random.Range(0f, total_weight_hit);
		for (int i = 0; i < HitLoot.Length; i++)
		{
			if (HitLoot[i].GetWeight() >= roll)
			{
				return HitLoot[i];
			}
			roll -= HitLoot[i].GetWeight();
		}
		return null;
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
