using UnityEngine;
using System.Collections;

public static class GravityAttractor
{

	public static float gravityScale = -10;//global scalar for gravity;

	public static void Attract ( GameObject attractor, GameObject pulled, bool keepUpright )
	{

		pulled.rigidbody2D.AddForce( AttractForce(attractor, pulled) * Time.fixedDeltaTime, ForceMode2D.Force );
		if (keepUpright)
		{
			KeepUpright(attractor, pulled);
		}
	}
	

	public static Vector2 AttractForce(GameObject attractor, GameObject pulled)
	{
		Debug.DrawLine(pulled.transform.position, attractor.transform.position, Color.blue );
		Debug.DrawRay(pulled.transform.position, -(pulled.transform.position - attractor.transform.position).normalized, Color.red );

		return 
			(pulled.transform.position - attractor.transform.position).normalized 
				* attractor.rigidbody2D.mass 
				* pulled.rigidbody2D.mass 
				* gravityScale;
	}

	public static void KeepUpright(GameObject relativeTo, GameObject uprightObj)
	{
		uprightObj.transform.rotation = 
			Quaternion.FromToRotation(
				uprightObj.transform.up, 
				(uprightObj.transform.position - relativeTo.transform.position).normalized) * uprightObj.transform.rotation ;
	}

	static float Cross(Vector2 a, Vector2 b)
	{
		return a.x*b.y - a.y-b.x;
	}
}
