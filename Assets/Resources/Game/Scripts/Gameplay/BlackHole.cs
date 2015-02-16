using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour
{
	public GameObject whiteHole;

	void Start ()
	{
		rigidbody2D.AddTorque (5.0f);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		col.transform.position = whiteHole.transform.position;
		RotateTransform (col.transform);
	}

	void RotateTransform (Transform a)
	{
		Vector2 _velocity;


		a.rigidbody2D.velocity = _velocity;
	}
}
