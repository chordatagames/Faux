using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalBehaviour : MonoBehaviour
{
	public GameObject otherPortal;
	
	List<GameObject> inside = new List<GameObject>();

	void Update ()
	{

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.name != "portal_clone" && !inside.Contains(col.gameObject))
		{
			GameObject entering = col.transform.gameObject;
			Debug.Log (entering.ToString());
			inside.Add (entering);

			MonoBehaviour[] cloneScripts = GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour script in cloneScripts)
			{
				Destroy (script);
			}

			Object temp = Instantiate (entering, otherPortal.transform.position, Quaternion.identity);
			temp.name = "portal_clone";
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		inside.Remove (col.gameObject);
	}
}