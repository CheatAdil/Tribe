using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
	[Header("Sprites")]
	[SerializeField] protected FireStates state;
	[SerializeField] protected List<Sprite_set> Sprite_sets;

	private Sprite_animator anim;
	private void Start()
	{
		anim = GetComponent<Sprite_animator>();
		if (anim != null)
		{
			anim.enabled = true;
			anim.init(Sprite_sets[((int)state)].SPRITES, Sprite_sets[((int)state)].repeat_times);
		}
		
	}
	private void Update()
	{
		SwitchState(FireStates.idle);
	}
	private void SwitchState(FireStates s)
	{
		if (s != state)
		{
			state = s;
			if (anim != null)
			{
				anim.enabled = true;
				anim.init(Sprite_sets[((int)state)].SPRITES, Sprite_sets[((int)state)].repeat_times);
			}
		}
	}
}


public enum FireStates
{
	idle,
	excited,
	off,
}
