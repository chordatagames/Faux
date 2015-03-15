using UnityEngine;
using System.Collections;

/// <summary>
/// Destroys gameobject when it's created. Used for NoProduct.
/// </summary>
public class TerminateOnCreation : MonoBehaviour {
	void Start() {
		Destroy(gameObject);
	}
}
