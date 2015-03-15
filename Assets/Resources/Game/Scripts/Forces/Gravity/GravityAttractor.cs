using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class GravityAttractor : MonoBehaviour
{

	public void Attract(GameObject pulled, bool keepUpright )
	{
		pulled.GetComponent<Rigidbody2D>().AddForce(AttractForce( pulled ));
		if (keepUpright)
		{
			KeepUpright(pulled);
		}
	}
	
	
	public Vector2 AttractForce(GameObject pulled)
	{
		Debug.DrawLine(pulled.transform.position, transform.position, Color.blue);
		Debug.DrawRay(pulled.transform.position, -(pulled.transform.position - transform.position).normalized, Color.red);
		
		return 
			(transform.position - pulled.transform.position).normalized 
				* GetComponent<Rigidbody2D>().mass
				* pulled.GetComponent<Rigidbody2D>().mass
				* WorldOptions.GravityScale;
	}
	
	public void KeepUpright(GameObject pulled) //Keep all calls of this function to Update, not FixedUpdate, or character will appear wrongly rotated when moving
	{
		Vector2 dir = new Vector2(transform.position.x - pulled.transform.position.x, transform.position.y - pulled.transform.position.y).normalized;
		float angle = Vector2.Angle(Vector2.right, dir)*Mathf.Deg2Rad*Mathf.Sign(dir.y) + Mathf.PI/2;
		pulled.transform.rotation = new Quaternion(0, 0, 1 * Mathf.Sin (angle/2), Mathf.Cos (angle/2));
	}

}