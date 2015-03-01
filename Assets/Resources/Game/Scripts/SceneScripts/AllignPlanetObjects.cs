using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode()]
public class AllignPlanetObjects : MonoBehaviour 
{
	void OnRenderObject ()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		foreach(Transform child in transform)//access children
		{
			Vector2 fromCenterDir = (child.position - transform.position).normalized;

			if (child.name != "OrangePortal" && child.name != "BluePortal")	//TODO: Make better system for this
				child.position = (Vector2)transform.position + fromCenterDir*(transform.localScale.x/2);

			float angle = Vector2.Angle(Vector2.right, -fromCenterDir)*Mathf.Deg2Rad*Mathf.Sign(-fromCenterDir.y) + Mathf.PI/2;
			child.transform.rotation = new Quaternion(0, 0, 1 * Mathf.Sin (angle/2), Mathf.Cos (angle/2));
		}
	}
}
