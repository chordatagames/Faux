using UnityEngine;
using System.Collections;

public class ThornyKatana : Weapon 
{
	Animator anim;

	protected override void Spawned() 
	{
		anim = GetComponent<Animator>();
	}

	protected override void WeaponFireBehaviour (GameObject product)
	{
		anim.SetTrigger("attack");
	}
}
