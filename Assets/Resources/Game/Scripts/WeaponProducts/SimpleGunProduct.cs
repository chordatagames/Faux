using UnityEngine;
using System.Collections;

public class SimpleGunProduct : WeaponProduct 
{
	public float force;

	private Rigidbody2D rigidbody2d;

	protected override void Spawned()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		rigidbody2d.AddForce(new Vector2(force,0));
	}

	protected override void OnHit(Living living)
	{

	}

	protected override void OnHit(GameObject obj) 
	{
		if (obj.tag == "GravityAttractor") Destroy(gameObject);
	}
}