using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class GravityPulled : MonoBehaviour 
{
	public bool keepUpright = false;
	
	GameObject[] attractors;
	GameObject closest;

	void Start ()
	{
		GetComponent<Rigidbody2D>().fixedAngle = false;
		attractors = World.GravityAttractors;
		FindClosest();
	}

	void FixedUpdate ()
	{
		FindClosest();
	}

	void Update()
	{
		if (closest == null) 
		{
			Debug.Log("closest is null"); // this was an issue
			return;
		}
		closest.GetComponent<GravityAttractor>().Attract(gameObject, keepUpright);
	}

	private void FindClosest() {
		float biggestAtt = 0;
		foreach(GameObject attractor in attractors)
		{
			float dist = (transform.position - attractor.transform.position).magnitude;
			float att = attractor.GetComponent<Rigidbody2D>().mass / Mathf.Pow(dist,2);
			if (att > biggestAtt)
			{
				biggestAtt = att;
				closest = attractor;
			}
		}
	}
}
