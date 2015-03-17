using UnityEngine;
using System.Collections;

public class SimpleGunProduct : WeaponProduct {
	protected override void Spawned()
	{

	}

	protected override void OnHit(Living living)
	{

	}

	protected override void OnHit(GameObject obj) 
	{
		if (obj.tag == "GravityAttractor") Destroy(gameObject);
	}
}
