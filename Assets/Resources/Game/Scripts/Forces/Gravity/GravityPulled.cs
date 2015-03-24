using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class GravityPulled : MonoBehaviour 
{
	public bool keepUpright = true;
	public float uprightRange = 2.0f;

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
		Attract(closest);
	}

	void Update()
	{
		if (closest == null) 
		{
			Debug.Log("closest is null"); // this was an issue
			return;
		}
		if (keepUpright)
		{
			KeepUpright (closest);
		}
	}

	private void FindClosest()
	{
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
	
	public void Attract(GameObject attractor)
	{
		GetComponent<Rigidbody2D> ().AddForce (AttractForce (attractor));
	}
	
	public Vector2 AttractForce(GameObject attractor)
	{
	//	Debug.DrawLine(transform.position, attractor.transform.position, Color.blue);
	//	Debug.DrawRay(transform.position, -(transform.position - attractor.transform.position).normalized, Color.red);
		
		return 
			(attractor.transform.position - transform.position).normalized
				* GetComponent<Rigidbody2D>().mass
				* attractor.GetComponent<Rigidbody2D>().mass
				* WorldOptions.GravityScale;
	}
	
	public void KeepUpright(GameObject attractor) //Keep all calls of this function to Update, not FixedUpdate, or chracter will appear wrongly rotated when moving
	{
		Vector2 dir = new Vector2(attractor.transform.position.x - transform.position.x, attractor.transform.position.y - transform.position.y).normalized;
		if (Physics2D.OverlapCircle(transform.position, uprightRange, 1 << 9) != null)
		{
			RaycastHit2D hit = Physics2D.Raycast (transform.position, -transform.up, uprightRange, 1 << 9);
			if (hit != null)
			{
				if (hit.normal != -Vector2.up)
				{
					dir = -hit.normal;
				}
			}
		}

		float angle = Vector2.Angle(Vector2.right, dir) * Mathf.Deg2Rad * Mathf.Sign(dir.y) + Mathf.PI/2;
		transform.rotation = new Quaternion(0, 0, Mathf.Sin (angle/2), Mathf.Cos (angle/2));
	}
}
