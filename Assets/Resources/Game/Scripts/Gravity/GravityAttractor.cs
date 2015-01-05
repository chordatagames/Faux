using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class GravityAttractor : MonoBehaviour
{
	
	public static float gravityScale = 1;//global scalar for gravity;
	
	public void Attract ( GameObject pulled, bool keepUpright )
	{
		pulled.rigidbody2D.AddForce( AttractForce( pulled ) );
		if (keepUpright)
		{
			KeepUpright(pulled);
		}
	}
	
	
	public Vector2 AttractForce( GameObject pulled)
	{
		Debug.DrawLine(pulled.transform.position, transform.position, Color.blue );
		Debug.DrawRay(pulled.transform.position, -(pulled.transform.position - transform.position).normalized, Color.red );
		
		return 
			( transform.position - pulled.transform.position).normalized 
				* rigidbody2D.mass 
				* pulled.rigidbody2D.mass 
				* gravityScale;
	}
	
	public void KeepUpright( GameObject pulled)
	{
		Vector3 dir = (transform.position - pulled.transform.position).normalized;
		float angle = 90+Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg ;
		pulled.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) ;
	}

}