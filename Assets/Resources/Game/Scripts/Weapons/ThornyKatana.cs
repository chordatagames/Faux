using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class ThornyKatana : Weapon {
	Animator anim;

	protected override void Spawned() 
	{
		anim = GetComponent<Animator>();
	}

	protected override void WeaponFireBehaviour (GameObject product)
	{
		anim.SetBool("shooting",true);
	}
}
