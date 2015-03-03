using UnityEngine;
using System.Collections;

public class JumpPad : GameComponent
{
	public LayerMask affectingLayers;
	public float pushForce = 1200;

	void OnTriggerEnter2D(Collider2D col)
	{
		if( !onlyOwnerUse || col.GetComponent<GameComponent>().OwnedBy == OwnedBy) 
		{
			if (((1<<col.gameObject.layer) & affectingLayers) != 0)
			{
				// Will push all objects just as hard regardless of mass
				col.GetComponent<Rigidbody2D>().AddForce(pushForce * col.GetComponent<Rigidbody2D>().mass * transform.up); 
			}
		}
	}
}
