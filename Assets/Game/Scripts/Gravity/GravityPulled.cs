using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class GravityPulled : MonoBehaviour 
{
	public bool keepUpright = false;

	GameObject[] attractors;
	GameObject closest;

	float closestDist = float.MaxValue;

	void Awake ()
	{
		rigidbody2D.fixedAngle = false;
	}
	void Start () 
	{
		attractors = GameObject.FindGameObjectsWithTag("GravityAttractor");
	}

	void FixedUpdate () 
	{
		foreach(GameObject attractor in attractors)
		{
			float dist = (transform.position - attractor.transform.position).magnitude;
			if( dist < closestDist)
			{
				closestDist = dist;
				closest = attractor;
			}
		}
		closestDist = float.MaxValue;
		GravityAttractor.Attract(closest, gameObject, keepUpright);
	}
}
