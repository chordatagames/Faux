using UnityEngine;
using System.Collections;

public class ThornyKatana : Weapon {
	protected override void Spawned() {
		// doesn't really need to do anything, i guess?
	}

	protected override void WeaponFireBehaviour (GameObject product)
	{
		GetComponent<Animation>().Play();
	}
}
