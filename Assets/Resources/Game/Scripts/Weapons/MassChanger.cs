using UnityEngine;
using System.Collections;

public class MassChanger : Throwable , IWeaponThrowable
{
	public float radius, change;
	public LayerMask affecting;
	int timesUsed;
	GameObject forceField;

	public override void Spawned ()
	{
		base.Spawned ();
		Activate ();

	}
	public override void Wait ()
	{
		base.Wait ();

	}
	public override void Activate ()
	{
		base.Activate ();
	}
	public override void Active ()
	{
		base.Active ();

		Collider2D[] inField = Physics2D.OverlapCircleAll(transform.position, radius, affecting);
		foreach (Collider2D c in inField)
		{
			if (c.rigidbody2D != null)
			{
				c.rigidbody2D.mass *= change;
			}
		}
	

	}
}
