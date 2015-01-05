using UnityEngine;
using System.Collections;

public class Grenade : Throwable 
{
	public float explotionSize = 4, explotionForce = 12;
	
	// Called when the object is spawned.
	public override void Spawned ()
	{
		base.Spawned();
		renderer.material.color = Color.yellow;
	}
	// Called every frame this object is alive.
	public override void Living ()
	{
		//base.Living (); - Not neccesary.
		
		if ( ( (int)base.timer )%2 == 0 )
		{
			renderer.material.color = Color.yellow;
		}
		else
		{
			renderer.material.color = Color.red;
		}
	}
	// Called when the objects timer runs out.
	public override void Activate ()
	{
		base.Activate ();
		Debug.DrawLine(transform.position - new Vector3(explotionSize, 0, 0), transform.position + new Vector3(explotionSize, 0, 0), Color.green, 100f);
		foreach (Collider2D c in Physics2D.OverlapCircleAll (transform.position, explotionSize))
		{
			if (c.rigidbody2D != null)
			{
				c.rigidbody2D.AddForce((c.transform.position-this.transform.position) * explotionForce);
			}
		}
	}
}