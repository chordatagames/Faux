﻿using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour
{
	public LayerMask affectingLayers;
	public float pushForce = 1200;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (((1<<col.gameObject.layer) & affectingLayers) != 0)
		{
			// Will push all objects just as hard regardless of mass
			col.rigidbody2D.AddForce(pushForce * col.rigidbody2D.mass * transform.up); 
		}
	}
}
