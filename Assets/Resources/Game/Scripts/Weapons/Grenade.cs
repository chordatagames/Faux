using UnityEngine;
using System.Collections;

public class Grenade : TimerBased 
{
	public float explotionSize = 4, explotionForce = 12;
	
	// Called when the object is spawned.
	public override void Spawned ()
	{
		base.Spawned();
		GetComponent<Renderer>().material.color = Color.yellow;
	}
	// Called every frame this object is alive.
	public override void Wait ()
	{
		base.Wait ();

	}
	// Called when the objects timer runs out.
	public override void Activate ()
	{
		base.Activate ();
		foreach (Collider2D c in Physics2D.OverlapCircleAll (transform.position, explotionSize))
		{
			if (c.GetComponent<Rigidbody2D>() != null)
			{
				c.GetComponent<Rigidbody2D>().AddForce((c.transform.position-this.transform.position) * explotionForce);
				if(c.GetComponent<Living>() != null)
				{
					c.GetComponent<Living>().Damage(((c.transform.position - transform.position).magnitude * (c.transform.position - transform.position).magnitude + 1) * explotionForce);
				}
			}
		}
	}
}