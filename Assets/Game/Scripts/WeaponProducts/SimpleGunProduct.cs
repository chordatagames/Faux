using UnityEngine;
using System.Collections;

public class SimpleGunProduct : WeaponProduct 
{
	public float force;

	private Rigidbody2D rigidbody2d;

	protected override void Spawned()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		//this does a lot of weird things and isn't really wanted:
		//rigidbody2d.velocity = ShotBy.GetComponent<Rigidbody2D>().velocity;
		transform.rotation = ShotBy.transform.rotation;
		rigidbody2d.AddForce((ShotBy.facingRight ? transform.right : -transform.right) * force);
	}

	protected override void OnHit(Living living)
	{
		Destroy(gameObject);
	}

	protected override void OnHit(GameObject obj) 
	{
		if (obj.tag == "GravityAttractor") Destroy(gameObject);
	}
}