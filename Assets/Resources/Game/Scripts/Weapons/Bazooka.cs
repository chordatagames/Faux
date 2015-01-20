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
		base.Spawned ();
	}

	public override void Wait ()
	{
		base.Wait ();
	}

	public override void Activate ()
	{
		base.Activate ();

		Debug.DrawLine(transform.position - new Vector3(explotionSize, 0, 0), transform.position + new Vector3(explotionSize, 0, 0), Color.yellow, 50f);

		foreach (Collider2D c in Physics2D.OverlapCircleAll (transform.position, explotionSize))
		{
			if (c.rigidbody2D != null)
			{
				c.rigidbody2D.AddForce((c.transform.position - transform.position) * explotionForce);
				if( c.GetComponent<Living>() != null )
				{
					c.GetComponent<Living>().Damage((-(c.transform.position - transform.position).magnitude * (c.transform.position - transform.position).magnitude + 1) * explotionForce);
				}
			}
		}
	}
}
