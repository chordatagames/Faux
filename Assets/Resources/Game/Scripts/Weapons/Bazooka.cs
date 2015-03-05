using UnityEngine;
using System.Collections;

public class Bazooka : TimerBased
{
	public float explotionSize = 4, explotionForce = 12;

	void OnCollisionEnter2D( Collision2D col )
	{
		Activate ();
	}

	public override void Spawned ()
	{
	}

	public override void Wait ()
	{
	}

	public override void Activate ()
	{
		foreach (Collider2D c in Physics2D.OverlapCircleAll (transform.position, explotionSize))
		{
			if (c.GetComponent<Rigidbody2D>() != null)
			{
				c.GetComponent<Rigidbody2D>().AddForce((c.transform.position - transform.position).normalized * explotionForce);
				if( c.GetComponent<Living>() != null )
				{
					c.GetComponent<Living>().Damage(explotionForce / ((c.transform.position - transform.position).magnitude * (c.transform.position - transform.position).magnitude + 1));
				}
			}
		}
		Destroy (gameObject);
	}
}
