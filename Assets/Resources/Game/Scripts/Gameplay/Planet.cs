using UnityEngine;
using System.Collections;

public class Planet : GameComponent
{
	public bool gasPlanet = false; //Gasplanets 'surface' will be inside the planet. Upon touch, objects will 'die'.
	//Perhaps add another shader for gasplanets to use?

	void Start()
	{
		if (gasPlanet)
		{ collider2D.isTrigger = true; }
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if ( (col.transform.position-transform.position).magnitude+1 < ( (CircleCollider2D)collider2D ).radius )
		{ Destroy( col.gameObject ); } //A bit harsh perhaps?
	}
}
