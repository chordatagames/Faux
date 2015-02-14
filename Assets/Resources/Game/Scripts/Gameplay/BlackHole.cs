using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour
{
	public GameObject whiteHole;

	void OnTriggerEnter2D (Collider2D col)
	{
		col.transform.position = whiteHole.transform.position;
	}
}
