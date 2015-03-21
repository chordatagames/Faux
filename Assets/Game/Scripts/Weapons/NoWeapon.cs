using UnityEngine;
using System.Collections;

/// <summary>
/// No weapon. (monodevelop generated this summary but frankly its perfect so whatever.)
/// </summary>
public class NoWeapon : Weapon {
	protected override void Spawned() {
		// do nothing, there is nothing here 
		// ¯\_(ツ)_/¯ go away
	}

	protected override void WeaponFireBehaviour(GameObject product) {
		Destroy(product); // it's just gone, man. it's just gone.
	}
}
