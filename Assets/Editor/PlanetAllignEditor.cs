using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameComponent))]
public class PlanetAllignEditor : Editor
{
	void OnSceneGUI() 
	{
		if (Selection.gameObjects.Length >= 1)
		{
//			if (Event.current.type == EventType.MouseUp)
//			{
//				Debug.Log("TEST");
//			}
			if (Event.current.type == EventType.MouseDrag) 
			{
//				if()//in area
//					PlaceOnSurface ();
			}
		}
	}


	public void PlaceOnSurface()
	{
		foreach (GameObject g in Selection.gameObjects)
		{
			g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0);

			Vector2 fromCenterDir = (g.transform.position - g.transform.parent.position).normalized;
			g.transform.position = (Vector2)g.transform.parent.position + fromCenterDir*(g.transform.parent.localScale.x/2);
				
			float angle = Vector2.Angle(Vector2.right, -fromCenterDir)*Mathf.Deg2Rad*Mathf.Sign(-fromCenterDir.y) + Mathf.PI/2;
			g.transform.rotation = new Quaternion(0, 0, 1 * Mathf.Sin (angle/2), Mathf.Cos (angle/2));
		}
	}

}


//[ExecuteInEditMode()]
//public class AllignPlanetObjects : MonoBehaviour 
//{
//	void Update()
//	{
//		if (Selection)
//		if (transform.hasChanged)
//		{PlaceOnSurface();}
//	}
//

//

//	void OnValidate () //Only ran in editmode.
//	{
//		PlaceOnSurface ();
//	}

//	void Start()
//	{
//		PlaceOnSurface ();
//	}
//
//	public void PlaceOnSurface()
//	{
//		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
//		foreach(Transform child in transform)//access children
//		{
//			Vector2 fromCenterDir = (child.position - transform.position).normalized;
//			child.position = (Vector2)transform.position + fromCenterDir*(transform.localScale.x/2);
//			
//			float angle = Vector2.Angle(Vector2.right, -fromCenterDir)*Mathf.Deg2Rad*Mathf.Sign(-fromCenterDir.y) + Mathf.PI/2;
//			child.transform.rotation = new Quaternion(0, 0, 1 * Mathf.Sin (angle/2), Mathf.Cos (angle/2));
//		}
//	}
//}
