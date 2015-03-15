using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameComponent),true)]
public class PlanetAlignEditor : Editor
{
	//TODO add layermask filtering
	void OnSceneGUI()
	{
		GameObject[] selection = Selection.gameObjects;
		if (selection.Length >= 1)
		{
			Event e = Event.current;
			foreach (GameObject g in selection)
			{

				Component allign = g.GetComponent( typeof(IAlignable) );
				if ( allign != null )
				{
					if(e.type == EventType.MouseUp)
					{
						RaycastHit2D[] hits = Physics2D.CircleCastAll(g.transform.position, ((IAlignable)allign).alignRadius, Vector3.forward, 10 );
						if ( hits.Length > 0 )
						{
							RaycastHit2D closestHit;
							float closestDist = float.MaxValue;
							foreach (RaycastHit2D hit in hits)
							{
								if ( hit.distance < closestDist)
								{
									closestDist = hit.distance;
									closestHit = hit;
								}
							}
							g.transform.parent = closestHit.collider.transform;
							( (IAlignable)allign ).AlignTo(g.transform.parent.GetComponent<Collider2D>());
						}
						else
						{ g.transform.parent = null; }
					}
				}
			}
		}
	}
}
