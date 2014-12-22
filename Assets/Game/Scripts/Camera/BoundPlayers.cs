using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class BoundPlayers : MonoBehaviour 
{
	GameObject[] tracking;



	public float margin= 0.5f;

	void Awake()
	{
		if(camera.orthographic)
		{
			camera.orthographic = true;
		}
	}

	void Start () 
	{
		tracking = GameObject.FindGameObjectsWithTag("Player");
	}
	
	void LateUpdate () 
	{
		CenterCamera();
		Encapsulate();
	}

	void CenterCamera()
	{
		Vector2 camPos = Vector2.zero;
		foreach (GameObject p in tracking)
		{
			camPos += (Vector2)p.transform.position;
		}
		transform.position = camPos/2;
	}

	void Encapsulate()
	{
		float longestDist = 0;
		Vector2 furthestAway = Vector2.zero;
		foreach (GameObject p in tracking)
		{
			float dist = (p.transform.position - transform.position).magnitude;
			if ( dist > longestDist)
			{
				longestDist = dist;
				furthestAway = p.transform.position;
			}
		}
		camera.orthographicSize = (furthestAway-(Vector2)transform.position).magnitude;

	}
}
