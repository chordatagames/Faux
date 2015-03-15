using UnityEngine;
using System.Collections;

/// <summary>
/// No weapon. (monodevelop generated this summary but frankly its perfect so whatever.)
/// </summary>
public class NoWeapon : Weapon {
	public override void Spawned() {
		ScriptableObject.CreateInstance<NoWeapon>();
	}
}
