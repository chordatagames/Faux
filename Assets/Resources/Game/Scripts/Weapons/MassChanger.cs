using UnityEngine;
using System.Collections;

public class MassChanger : Shooting
{
	public float radius, change;
	public LayerMask affecting;
	int timesUsed;
	GameObject forceField;

	public override void Spawned ()
	{
		Activate ();
	}
	public override void Wait ()
	{
		base.Wait();
	}
	public override void Activate ()
	{
		base.Activate();
	}
	public override void Active ()
	{
		Active();
		Collider2D[] inField = Physics2D.OverlapCircleAll(transform.position, radius, affecting);
		foreach (Collider2D c in inField)
		{
			if (c.GetComponent<Rigidbody2D>() != null)
			{
				c.GetComponent<Rigidbody2D>().mass *= change;
			}
		}
	}
}
