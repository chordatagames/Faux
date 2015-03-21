using UnityEngine;
using System.Collections;

/// <summary>
/// This is the product of NoWeapon. I realise that this looks strange af.
/// </summary>
public class NoProduct : WeaponProduct {
	protected override void Spawned() 
	{
		// do nothing here either. idk
	}
	protected override void OnHit(Living living)
	{

	}
	protected override void OnHit (GameObject gc)
	{

	}
}
